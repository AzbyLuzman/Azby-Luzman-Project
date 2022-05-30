using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardCombo : MonoBehaviour
{
    public PlayerMove playerMove;
    public AnimatorHandler AnimHandler;
    public CameraChange cameraChange;

    public JobType jobType;
    public StatsManager statsManager;
 
    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        AnimHandler = transform.GetChild(1).GetComponent<AnimatorHandler>();
        cameraChange = GameObject.Find("Cameras").GetComponent<CameraChange>();
        statsManager = GetComponent<StatsManager>();
    }

    public string currentAnimation;
    void AnimationCheck(string moveName)
    {
        currentAnimation = moveName;
        //Debug.Log("Current Animation = " + currentAnimation);
    }

    public void LightAttackCombo()
    {
        if (!AnimHandler.animator.GetBool("Action") && !AnimHandler.animator.GetBool("RTHold"))
        {
            // Light Attack Opener - Vanguard_Light1
            if (!AnimHandler.animator.GetBool("Interact") && !AnimHandler.animator.GetBool("Parry"))
            {
                AnimationCheck(jobType.LightAttack[0]);
                AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Interact);
                AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Action);
                AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.LightAttack);
                playerMove.CombatMovement(jobType.LightStartTime[0],jobType.LightDuration[0], jobType.LightSpeedType[0]);
                statsManager.DecreaseStat(StatsManager.StatsType.stamina, jobType.LightStats[0].y);
            }

            // Light Attack Combo
            else if (AnimHandler.animator.GetBool("Combo"))
            {
                // Vanguard_Light2
                if (currentAnimation == jobType.LightAttack[0] && !AnimHandler.animator.GetBool("Action"))
                {
                    AnimationCheck(jobType.LightAttack[1]);
                    AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Action);
                    AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.LightAttack);
                    playerMove.CombatMovement(jobType.LightStartTime[1], jobType.LightDuration[1], jobType.LightSpeedType[1]);
                    statsManager.DecreaseStat(StatsManager.StatsType.stamina, jobType.LightStats[1].y);
                }

                // Vanguard_Light3
                if (currentAnimation == jobType.LightAttack[1] && !AnimHandler.animator.GetBool("Action"))
                {
                    AnimationCheck(jobType.LightAttack[2]);
                    AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Action);
                    AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.LightAttack);
                    playerMove.CombatMovement(jobType.LightStartTime[2], jobType.LightDuration[2], jobType.LightSpeedType[2]);
                    statsManager.DecreaseStat(StatsManager.StatsType.stamina, jobType.LightStats[2].y);
                }

                // Vanguard_Light4
                if (currentAnimation == jobType.LightAttack[2] && !AnimHandler.animator.GetBool("Action"))
                {
                    AnimationCheck(jobType.LightAttack[3]);
                    AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Action);
                    AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.LightAttack);
                    playerMove.CombatMovement(jobType.LightStartTime[3], jobType.LightDuration[3], jobType.LightSpeedType[3]);
                    statsManager.DecreaseStat(StatsManager.StatsType.stamina, jobType.LightStats[3].y);
                }
            }
        }
    }

    public void HeavyAttackCombo()
    {
        if (!AnimHandler.animator.GetBool("Action") && !AnimHandler.animator.GetBool("RTHold"))
        {
            // Heavy Attack Opener - Vanguard_Heavy1
            if ((!AnimHandler.animator.GetBool("Interact") && !AnimHandler.animator.GetBool("Parry")) || (AnimHandler.animator.GetBool("Combo") && currentAnimation == jobType.LightAttack[2] && !AnimHandler.animator.GetBool("Action")))
            {
                AnimationCheck(jobType.HeavyAttack[0]);
                AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Interact);
                AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Action);
                AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.HeavyAttack);
                playerMove.CombatMovement(jobType.HeavyStartTime[0], jobType.HeavyDuration[0], jobType.HeavySpeedType[0]);
                statsManager.DecreaseStat(StatsManager.StatsType.stamina, jobType.HeavyStats[0].y);
            }

            // Heavy Attack Combo
            else if (AnimHandler.animator.GetBool("Combo"))
            {
                // Vanguard_Heavy2
                if (currentAnimation == jobType.HeavyAttack[0] && !AnimHandler.animator.GetBool("Action"))
                {
                    AnimationCheck(jobType.HeavyAttack[1]);
                    AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Action);
                    AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.HeavyAttack);
                    playerMove.CombatMovement(jobType.HeavyStartTime[1], jobType.HeavyDuration[1], jobType.HeavySpeedType[1]);
                    statsManager.DecreaseStat(StatsManager.StatsType.stamina, jobType.HeavyStats[1].y);
                }

                // Vanguard_Heavy3
                if (currentAnimation == jobType.HeavyAttack[1] && !AnimHandler.animator.GetBool("Action"))
                {
                    AnimationCheck(jobType.HeavyAttack[2]);
                    AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Action);
                    AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.HeavyAttack);
                    playerMove.CombatMovement(jobType.HeavyStartTime[2], jobType.HeavyDuration[2], jobType.HeavySpeedType[2]);
                    statsManager.DecreaseStat(StatsManager.StatsType.stamina, jobType.HeavyStats[2].y);
                }
            }
        }
    }

    public void SpecialAttackCombo()
    {
        if ((AnimHandler.animator.GetBool("Combo") && !AnimHandler.animator.GetBool("Action")) && 
            (
            currentAnimation == jobType.LightAttack[1] ||
            currentAnimation == jobType.LightAttack[2] || 
            currentAnimation == jobType.LightAttack[3] ||
            currentAnimation == jobType.HeavyAttack[1] ||
            currentAnimation == jobType.HeavyAttack[2]
            ))
        {
            AnimationCheck(jobType.SpecialAttack[0]);
            AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Action);
            AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.SpecialAttack);
            playerMove.CombatMovement(jobType.SpecialStartTime[0], jobType.SpecialDuration[0], jobType.SpecialSpeedType[0]);
            statsManager.DecreaseStat(StatsManager.StatsType.stamina, jobType.SpecialStats[0].y);
        }
    }

    public void SpecialAction(bool action)
    {
        if (AnimHandler.animator.GetBool("RTHold") != action)
        {
            AnimHandler.AnimSetBool("RTHold", action);
            AnimHandler.AnimSetBool("Brace", action);

            // Start Special Action
            cameraChange.SpecialAction_Stance(action);
        }
    }

    public void Parry()
    {
        if (!AnimHandler.animator.GetBool("Interact") && !AnimHandler.animator.GetBool("Action"))
        {
            AnimHandler.animator.Play("Vanguard_BraceStart");
        }
    }

    public void Brace(bool action)
    {
        if (AnimHandler.animator.GetBool("LTHold") != action && !AnimHandler.animator.GetBool("RTHold"))
        {
            AnimHandler.AnimSetBool("LTHold", action);
        }
    }

    public void Execute()
    {
        if (!AnimHandler.animator.GetBool("Action") && AnimHandler.animator.GetBool("Parry") && AnimHandler.animator.GetBool("Combo"))
        {
            Debug.Log("EXECUTE");
            AnimHandler.ExecutionPlay();
            AnimationCheck(jobType.Execution[0]);
            AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Action);
            AnimHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Execute);
            playerMove.CombatMovement(jobType.ExecutionStartTime[0], jobType.ExecutionDuration[0], jobType.ExecutionSpeedType[0]);
        }
    }
}
