using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : InputHandler
{
    public Animator animator;

    [Header("Assign Weapon Manually")]
    public GameObject weaponRight;
    public GameObject weaponLeft;

    private GameObject target;

    public enum AnimatorParameter
    {
        Interact,
        Action,
        LightAttack,
        HeavyAttack,
        SpecialAttack,
        Combo,
        DoCombo,
        R_HitTrigger,
        L_HitTrigger,
        Parry,
        Brace,
        Execute,
        tobeExecuted,
        Impact,
        Superarmor,
        HitCheck
    };

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void MoveXY()
    {
        animator.SetFloat("MoveX", move.x);
        animator.SetFloat("MoveY", move.y);
    }

    public void AnimSetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    #region Combat Related Flag
    
    void ResetAllCombatFlag()
    {
        interact = false;
        animator.SetBool("Interact", false);
        animator.SetBool("Action", false);
        animator.SetBool("Combo", false);
        animator.SetBool("DoCombo", false);
        animator.SetBool("Hit", false);
        animator.SetBool("LightAttack", false);
        animator.SetBool("HeavyAttack", false);
        animator.SetBool("SpecialAttack", false);
        animator.SetBool("Parry", false);
        animator.SetBool("Brace", false);
        animator.SetBool("Superarmor", false);
        animator.SetBool("Execute", false);
        animator.SetBool("tobeExecuted", false);
        animator.SetBool("HitCheck", false);
        weaponRight.GetComponent<BoxCollider>().enabled = false;
        weaponLeft.GetComponent<BoxCollider>().enabled = false;
    }
    
    void ResetCombo()
    {
        animator.SetBool("Combo", false);
        animator.SetBool("DoCombo", false);
        animator.SetBool("LightAttack", false);
        animator.SetBool("HeavyAttack", false);
        animator.SetBool("SpecialAttack", false);
    }

    public void Start_Parameter(AnimatorParameter name)
    {
        switch (name)
        {
            case AnimatorParameter.Interact:
                interact = true;
                animator.SetBool("Interact", true);
                break;
            case AnimatorParameter.Action:
                animator.SetBool("Action", true);
                break;
            case AnimatorParameter.LightAttack:
                animator.SetBool("LightAttack", true);
                break;
            case AnimatorParameter.HeavyAttack:
                animator.SetBool("HeavyAttack", true);
                break;
            case AnimatorParameter.SpecialAttack:
                animator.SetBool("SpecialAttack", true);
                break;
            case AnimatorParameter.Combo:
                animator.SetBool("Combo", true);
                break;
            case AnimatorParameter.DoCombo:
                animator.SetBool("DoCombo", true);
                break;
            case AnimatorParameter.R_HitTrigger:
                animator.SetBool("Hit", true);
                weaponRight.GetComponent<BoxCollider>().enabled = true;
                break;
            case AnimatorParameter.L_HitTrigger:
                animator.SetBool("Hit", true);
                weaponLeft.GetComponent<BoxCollider>().enabled = true;
                break;
            case AnimatorParameter.Parry:
                animator.SetBool("Parry", true);
                break;
            case AnimatorParameter.Brace:
                animator.SetBool("Brace", true);
                break;
            case AnimatorParameter.Execute:
                animator.SetBool("Execute", true);
                break;
            case AnimatorParameter.tobeExecuted:
                animator.SetBool("tobeExecuted", true);
                break;
            case AnimatorParameter.Impact:
                animator.SetBool("Impact", true);
                break;
            case AnimatorParameter.Superarmor:
                animator.SetBool("Superarmor", true);
                break;
        }
    }

    public void End_Parameter(AnimatorParameter name)
    {
        switch (name)
        {
            case AnimatorParameter.Interact:
                interact = false;
                animator.SetBool("Interact", false);
                break;
            case AnimatorParameter.Action:
                animator.SetBool("Action", false);
                break;
            case AnimatorParameter.LightAttack:
                animator.SetBool("LightAttack", false);
                break;
            case AnimatorParameter.HeavyAttack:
                animator.SetBool("HeavyAttack", false);
                break;
            case AnimatorParameter.SpecialAttack:
                animator.SetBool("SpecialAttack", false);
                break;
            case AnimatorParameter.Combo:
                animator.SetBool("Combo", false);
                break;
            case AnimatorParameter.DoCombo:
                animator.SetBool("DoCombo", false);
                break;
            case AnimatorParameter.R_HitTrigger:
                animator.SetBool("Hit", false);
                weaponRight.GetComponent<BoxCollider>().enabled = false;
                break;
            case AnimatorParameter.L_HitTrigger:
                animator.SetBool("Hit", false);
                weaponLeft.GetComponent<BoxCollider>().enabled = false;
                break;
            case AnimatorParameter.Parry:
                animator.SetBool("Parry", false);
                break;
            case AnimatorParameter.Brace:
                animator.SetBool("Brace", false);
                break;
            case AnimatorParameter.Execute:
                animator.SetBool("Execute", false);
                break;
            case AnimatorParameter.tobeExecuted:
                animator.SetBool("tobeExecuted", false);
                break;
            case AnimatorParameter.Impact:
                animator.SetBool("Impact", false);
                break;
            case AnimatorParameter.Superarmor:
                animator.SetBool("Superarmor", false);
                break;
            case AnimatorParameter.HitCheck:
                animator.SetBool("HitCheck", false);
                break;
        }
    }

    #endregion

    #region Execution Related

    public void ExecutionTarget(GameObject executionTarget)
    {
        target = executionTarget;
    }
    public void ExecutionPlay()
    {
        if (target != null)
        {
            Debug.Log(target.name);
            Debug.Log("Execution");

            target.transform.GetChild(1).GetComponent<AnimatorHandler_AI>().animator.Play("Executed");

            // CAMERA WORKS
            if (animator.transform.parent.name == "Player")
            {
                GameObject.Find("Cameras").GetComponent<CameraChange>().ExecutionCamera_Start();

                // For future use, maybe change into character's move name
                GameObject.Find("Player").transform.GetChild(2).GetComponent<Animator>().Play("ExecutionCamera");
            }
        }
    }

    void ExecutionEnd()
    {
        if (target != null)
        {
            // CAMERA WORKS
            if (animator.transform.parent.name == "Player")
            {
                GameObject.Find("Cameras").GetComponent<CameraChange>().ExecutionCamera_End();
            }
        }
    }

    #endregion
}
