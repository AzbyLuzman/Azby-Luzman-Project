using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCombo_Enemy : MonoBehaviour
{
    public EnemyBehaviour enemyBehaviour;
    public ActionManager_Enemy actionManager_Enemy;
    public Animator animator;

    string clipName;
    AnimatorClipInfo[] currentClipInfo;

    void Start()
    {
        enemyBehaviour = transform.parent.GetComponent<EnemyBehaviour>();
        actionManager_Enemy = transform.parent.GetComponent<ActionManager_Enemy>();
        animator = GetComponent<Animator>();
    }

    void SendClipInfo()
    {
        currentClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
        clipName = currentClipInfo[0].clip.name;
        actionManager_Enemy.ReadClipInfo(clipName);
    }

    public void ReadComboOpener(string opener)
    {
        switch (opener)
        {
            case "L":
                Combo_L();
                break;
            case "H":
                Combo_H();
                break;
            case "S":
                Combo_S();
                break;
            case "LH":
                Combo_LH();
                break;
            case "LS":
                Combo_LS();
                break;
            case "HS":
                Combo_HS();
                break;
            case "LHS":
                Combo_LHS();
                break;
        }
    }

    #region Combo Transition

    // into Light
    void Combo_L()
    {
        if (enemyBehaviour.statsManager.stats.stamina > 0f)
        {
            animator.SetBool("LightAttack", true);
        }
    }

    // Into Heavy
    void Combo_H()
    {
        if (enemyBehaviour.statsManager.stats.stamina > 0f)
        {
            animator.SetBool("HeavyAttack", true);
        }
    }

    // Into Special
    void Combo_S()
    {
        if (enemyBehaviour.statsManager.stats.stamina > 0f)
        {
            animator.SetBool("SpecialAttack", true);
        }
    }

    // Into Light / as Finisher (no Combo)
    void Combo_LF()
    {
        if (enemyBehaviour.statsManager.stats.stamina > 0f)
        {
            int range = Random.Range(0, 2);

            if (range == 0)
            {
                animator.SetBool("LightAttack", true);
            }
            
            // or EMPTY
        }
    }

    // Into Heavy / as Finisher (no Combo)
    void Combo_HF()
    {
        if (enemyBehaviour.statsManager.stats.stamina > 0f)
        {
            int range = Random.Range(0, 2);

            if (range == 0)
            {
                animator.SetBool("HeavyAttack", true);
            }
        }
    }

    // Into Special / as Finisher (no Combo)
    void Combo_SF()
    {
        if (enemyBehaviour.statsManager.stats.stamina > 0f)
        {
            int range = Random.Range(0, 2);

            if (range == 0)
            {
                animator.SetBool("SpecialAttack", true);
            }
        }
    }

    // Into Light / Heavy
    void Combo_LH()
    {
        if (enemyBehaviour.statsManager.stats.stamina > 0f)
        {
            int range = Random.Range(0, 2);

            if (range == 0)
            {
                animator.SetBool("LightAttack", true);
            }
            else if (range == 1)
            {
                animator.SetBool("HeavyAttack", true);
            }
        }
    }

    // Into Light / Special
    void Combo_LS()
    {
        if (enemyBehaviour.statsManager.stats.stamina > 0f)
        {
            int range = Random.Range(0, 2);

            if (range == 0)
            {
                animator.SetBool("LightAttack", true);
            }
            else if (range == 1)
            {
                animator.SetBool("SpecialAttack", true);
            }
        }
    }

    // Into Heavy / Special
    void Combo_HS()
    {
        if (enemyBehaviour.statsManager.stats.stamina > 0f)
        {
            int range = Random.Range(0, 2);

            if (range == 0)
            {
                animator.SetBool("HeavyAttack", true);
            }
            else if (range == 1)
            {
                animator.SetBool("SpecialAttack", true);
            }
        }
    }

    // Into Light / Heavy / Special
    void Combo_LHS()
    {
        if (enemyBehaviour.statsManager.stats.stamina > 0f)
        {
            int range = Random.Range(0, 3);

            if (range == 0)
            {
                animator.SetBool("LightAttack", true);
            }
            else if (range == 1)
            {
                animator.SetBool("HeavyAttack", true);
            }
            else if (range == 2)
            {
                animator.SetBool("SpecialAttack", true);
            }
        }            
    }

    public void Combo_Finished()
    {
        enemyBehaviour.offensiveState.ResetAttack();
    }

    #endregion
}
