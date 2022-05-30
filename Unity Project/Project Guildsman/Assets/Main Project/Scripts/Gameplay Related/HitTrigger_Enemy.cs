using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrigger_Enemy : MonoBehaviour
{
    public GameObject attacker;
    public GameObject cameraObjects;
    public AnimatorHandler_AI animatorHandler_AI;

    private void Start()
    {
        cameraObjects = GameObject.Find("Cameras");
        animatorHandler_AI = attacker.transform.GetChild(1).GetComponent<AnimatorHandler_AI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !animatorHandler_AI.animator.GetBool("HitCheck"))
        {
            animatorHandler_AI.animator.SetBool("HitCheck", true);
            AnimatorHandler animatorHandler = other.transform.GetChild(1).GetComponent<AnimatorHandler>();
            if (animatorHandler.animator.GetBool("Parry"))
            {
                //Debug.Log("Parried");

                // Player / Party reference
                animatorHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Impact);
                animatorHandler.animator.Play("Parry");
                cameraObjects.GetComponent<CameraChange>().CameraShake();


                // This / Enemy reference
                animatorHandler.ExecutionTarget(attacker);
                animatorHandler_AI.Start_Parameter(AnimatorHandler_AI.AnimatorParameter.Impact);
                animatorHandler_AI.animator.Play("Parried");
                attacker.GetComponent<EnemyBehaviour>().Flinch(other.transform, 0.75f);

                EnemyBehaviour enemyBehaviour = attacker.GetComponent<EnemyBehaviour>();
                enemyBehaviour.SwitchCombatState(enemyBehaviour.defensiveState);
            }
            else if (animatorHandler.animator.GetBool("Brace"))
            {
                //Debug.Log("Braced");
                animatorHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Impact);
                other.gameObject.GetComponent<PlayerMove>().Flinch(attacker.transform, 0.5f);
                cameraObjects.GetComponent<CameraChange>().CameraShake();
            }
            else
            {
                // if no superarmor
                if (animatorHandler.animator.GetBool("Superarmor"))
                {
                    //Debug.Log("Superarmor");
                }
                else
                {
                    animatorHandler.animator.Play("Impact1");
                    animatorHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Impact);
                    other.gameObject.GetComponent<PlayerMove>().Flinch(attacker.transform, 1f);
                    cameraObjects.GetComponent<CameraChange>().CameraShake();
                }
            }

            animatorHandler_AI.animator.speed = 0.05f;
            StartCoroutine(HitCoroutine());
        }
    }

    IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.075f);
        animatorHandler_AI.animator.speed = 1f;
        //Debug.Log("COROUTINE OVER");
    }
}
