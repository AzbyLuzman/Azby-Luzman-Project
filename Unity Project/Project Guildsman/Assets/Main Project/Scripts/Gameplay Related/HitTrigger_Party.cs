using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrigger_Party : MonoBehaviour
{
    public GameObject attacker;
    public GameObject cameraObjects;
    public AnimatorHandler animatorHandler;

    private void Start()
    {
        cameraObjects = GameObject.Find("Cameras");
        animatorHandler = attacker.transform.GetChild(1).GetComponent<AnimatorHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !animatorHandler.animator.GetBool("HitCheck"))
        {
            animatorHandler.animator.SetBool("HitCheck", true);
            EnemyBehaviour enemyBehaviour = other.GetComponent<EnemyBehaviour>();
            AnimatorHandler_AI animatorHandler_AI = other.transform.GetChild(1).GetComponent<AnimatorHandler_AI>();

            //Debug.Log("HIT");
            if (animatorHandler_AI.animator.GetBool("Parry"))
            {
                // Enemy reference
                animatorHandler.ExecutionTarget(attacker);
                animatorHandler_AI.Start_Parameter(AnimatorHandler_AI.AnimatorParameter.Impact);
                animatorHandler_AI.animator.Play("Parry");

                // Player / Party reference
                animatorHandler.Start_Parameter(AnimatorHandler.AnimatorParameter.Impact);
                animatorHandler.animator.Play("Impact1");
                attacker.GetComponent<PlayerMove>().Flinch(other.transform, 0.75f);
            }
            else if (animatorHandler_AI.animator.GetBool("Brace"))
            {
                //Debug.Log("Braced");

                animatorHandler_AI.Start_Parameter(AnimatorHandler_AI.AnimatorParameter.Impact);
                enemyBehaviour.SwitchCombatState(enemyBehaviour.defensiveState);

                other.gameObject.GetComponent<EnemyBehaviour>().Flinch(attacker.transform, 0.5f);
                cameraObjects.GetComponent<CameraChange>().CameraShake();
                animatorHandler.animator.speed = 0.05f;
                StartCoroutine(HitCoroutine());
            }
            else
            {
                if (animatorHandler_AI.animator.GetBool("Superarmor"))
                {

                }
                else
                {
                    animatorHandler_AI.Start_Parameter(AnimatorHandler_AI.AnimatorParameter.Impact);
                    enemyBehaviour.SwitchCombatState(enemyBehaviour.defensiveState);

                    // if no superarmor
                    animatorHandler_AI.animator.Play("Impact1", -1, 0);
                    other.gameObject.GetComponent<EnemyBehaviour>().Flinch(attacker.transform, 1f);
                }

                //      TESTING
                // Camera
                cameraObjects.GetComponent<CameraChange>().CameraShake();
                // Vibrate
                //Handheld.Vibrate();

                animatorHandler.animator.speed = 0.05f;
                StartCoroutine(HitCoroutine());
            }



            //if (animHandler.animator.GetBool("Execute"))
            //{
            //    // Temporary
            //    Debug.Log("Execute");
            //    other.transform.GetChild(1).GetComponent<AnimatorHandler_AI>().Impact_Start();
            //    other.transform.GetChild(1).GetComponent<AnimatorHandler_AI>().animator.Play("Executed");
            //}
            //else
            //{
            //    Debug.Log("HIT");

            //    //      TESTING
            //    // Camera
            //    cameraObjects.GetComponent<CameraChange>().CameraShake();
            //    // Vibrate
            //    //Handheld.Vibrate();

            //    other.transform.GetChild(1).GetComponent<AnimatorHandler_AI>().Impact_Start();

            //    // if no superarmor
            //    other.transform.GetChild(1).GetComponent<AnimatorHandler_AI>().animator.Play("Impact1", -1, 0);
            //    other.gameObject.GetComponent<EnemyBehaviour>().Flinch(attacker.transform);

            //    animHandler.animator.speed = 0.05f;
            //    StartCoroutine(HitCoroutine());
            //}
        }
    }

    IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.075f);
        animatorHandler.animator.speed = 1f;
        //Debug.Log("COROUTINE OVER");
    }
}
