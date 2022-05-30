using System.Collections;
using UnityEngine;

public class OffensiveState_Enemy : CombatState_Enemy
{
    bool startOpener;

    public override void EnterState(EnemyBehaviour enemy)
    {
        Debug.Log("OffensiveState");

        if (enemy.animatorHandler_AI.animator.GetBool("Impact"))
        {
            enemy.animatorHandler_AI.animator.SetBool("Impact", false);
        }

        startOpener = false;
    }

    public override void UpdateState(EnemyBehaviour enemy)
    {
        StateCondition(enemy);

        if (!startOpener)
        {
            Attack(enemy);
        }
    }

    void StateCondition(EnemyBehaviour enemy)
    {
        if (enemy.animatorHandler_AI.animator.GetBool("Brace"))
        {
            enemy.animatorHandler_AI.animator.SetBool("Brace", false);
        }
        if (enemy.animatorHandler_AI.animator.GetBool("Parry"))
        {
            enemy.animatorHandler_AI.animator.SetBool("Parry", false);
        }

        // to Defensive State
        if (enemy.statsManager.stats.stamina <= enemy.statsManager.stats.maxStamina * 0.15f)
        {
            enemy.SwitchCombatState(enemy.defensiveState);
        }
    }

    #region Attacking

    void Attack(EnemyBehaviour enemy)
    {
        // Attack Opener
        string opener = enemy.actionManager_Enemy.jobType.AttackOpener;
        enemy.animatorCombo_Enemy.ReadComboOpener(opener);
        startOpener = true;
    }

    public void ResetAttack()
    {
        startOpener = false;
    }

    #endregion
}
