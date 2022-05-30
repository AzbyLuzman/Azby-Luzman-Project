using System.Collections;
using UnityEngine;

public class DefensiveState_Enemy : CombatState_Enemy
{
    public override void EnterState(EnemyBehaviour enemy)
    {
        Debug.Log("DefensiveState");

        if (enemy.animatorHandler_AI.animator != null)
        {
            enemy.animatorHandler_AI.animator.SetBool("Brace", true);
        }

        // Counter instead of guarding
        if (enemy.statsManager.stats.stamina >= enemy.statsManager.stats.maxStamina * 0.5f)
        {
            enemy.SwitchCombatState(enemy.counterState);
        }

        Guarding(enemy);
    }

    public override void UpdateState(EnemyBehaviour enemy)
    {
        StateCondition(enemy);

        if (!enemy.animatorHandler_AI.animator.GetBool("Brace"))
        {
            enemy.animatorHandler_AI.animator.SetBool("Brace", true);
        }

        
    }

    void StateCondition(EnemyBehaviour enemy)
    {
        // to Counter State
        if (!guarding && enemy.statsManager.stats.stamina >= enemy.statsManager.stats.maxStamina * 0.5f)
        {
            enemy.animatorHandler_AI.animator.SetBool("Brace", false);
            enemy.SwitchCombatState(enemy.counterState);
        }
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
}
