using System.Collections;
using UnityEngine;

public class CounterState_Enemy : CombatState_Enemy
{
    public override void EnterState(EnemyBehaviour enemy)
    {
        Debug.Log("CounterState");

        if (enemy.animatorHandler_AI.animator != null)
        {
            enemy.animatorHandler_AI.animator.SetBool("Brace", true);
        }

        // SET COUNTER, GUARD OR PARRY
        if (!countering && !guarding)
        {
            CounterOption(enemy);
        }
    }

    public override void UpdateState(EnemyBehaviour enemy)
    {
        StateCondition(enemy);

        if (!guarding && enemy.animatorHandler_AI.animator.GetBool("Brace"))
        {
            enemy.animatorHandler_AI.animator.SetBool("Brace", false);
        }
    }

    void StateCondition(EnemyBehaviour enemy)
    {
        // to Defensive State
        if (enemy.statsManager.stats.stamina <= enemy.statsManager.stats.maxStamina * 0.15f)
        {
            enemy.SwitchCombatState(enemy.defensiveState);
        }

        // to Offensive State
        if (!guarding && !countering && enemy.statsManager.stats.stamina >= enemy.statsManager.stats.maxStamina * 0.8f)
        {
            enemy.SwitchCombatState(enemy.offensiveState);
        }
    }

    void CounterOption(EnemyBehaviour enemy)
    {
        float choice = Random.Range(0, 2);

        if (choice == 0)
        {
            Guarding(enemy);
        }
        else if (choice == 1)
        {
            Counter(enemy);
        }
        //else if (choice == 2)
        //{
        //    // to Offensive State
        //    enemy.SwitchCombatState(enemy.offensiveState);
        //}
    }

    #region Guarding

    bool guarding;
    Coroutine StartGuarding;
    void Guarding(EnemyBehaviour enemy)
    {
        if (StartGuarding != null)
        {
            enemy.StopCoroutine(StartGuarding);
        }
        StartGuarding = enemy.StartCoroutine(OnGuarding());
    }
    IEnumerator OnGuarding()
    {
        //Debug.Log("Guarding");
        guarding = true;
        yield return new WaitForSeconds(2f);
        guarding = false;
    }

    #endregion

    // TEMPORARY COUNTER LOGIC
    #region Countering

    bool countering;

    void Counter(EnemyBehaviour enemy)
    {
        if (enemy.animatorHandler_AI.animator != null)
        {
            enemy.animatorHandler_AI.animator.SetBool("Brace", false);
        }

        // todo READ PLAYER'S MOVE

        // PARAMETER "COUNTER" to ON for now
        if (enemy.animatorHandler_AI.animator != null)
        {
            if (!enemy.animatorHandler_AI.animator.GetBool("Parry"))
            {
                enemy.StartCoroutine(CounterTimer(enemy));
            }
        }
        
    }

    IEnumerator CounterTimer(EnemyBehaviour enemy)
    {
        countering = true;
        yield return new WaitForSeconds(0.25f);
        
        enemy.animatorHandler_AI.animator.SetBool(("Parry"), true);

        yield return new WaitForSeconds(2f);
        
        enemy.animatorHandler_AI.animator.SetBool(("Parry"), false);
        countering = false;
    }

    #endregion

}
