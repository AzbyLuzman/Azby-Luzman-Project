using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : InputHandler
{
    public GameObject player;
    public CharacterController controller;
    public Transform playerCamera;
    public CameraChange cameraChange;
    public float walkSpeed = 1.5f;
    public float runSpeed = 3f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public AnimatorHandler AnimHandler;
    private bool moving;
    private bool specialmoving;
   
    void Start()
    {
        player = this.gameObject;
        controller = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Cameras").transform.GetChild(0);
        cameraChange = playerCamera.parent.GetComponent<CameraChange>();
        AnimHandler = transform.GetChild(1).GetComponent<AnimatorHandler>();
        moving = false;
    }

    void Update()
    {
        // Animator
        AnimHandler.MoveXY();

        // Normal Movement (Walk / Run)
        if (!interact && !AnimHandler.animator.GetBool("RTHold") && !AnimHandler.animator.GetBool("CameraLock"))
        {
            NormalMove();
        }
        else if (!interact && AnimHandler.animator.GetBool("RTHold") && specialmoving && !AnimHandler.animator.GetBool("CameraLock"))
        {
            SpecialMove();
        }
        else if (!interact && AnimHandler.animator.GetBool("CameraLock"))
        {
            LockonMovement();
        }
    }

    #region Normal Movement
    void NormalMove()
    {
        Vector3 direction = new Vector3(move.x, 0f, move.y).normalized;
        if (direction != Vector3.zero)
        {
            // Animator
            moving = true;
            AnimHandler.AnimSetBool("Moving", true);
           
            // Character Move
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (Mathf.Abs(move.x) < 0.5f && Mathf.Abs(move.y) < 0.5f) // Walk or Strafe
            {
                AnimHandler.AnimSetBool("Run", false);
                controller.Move(moveDir * walkSpeed * Time.deltaTime);
            }
            else // Run
            {
                AnimHandler.AnimSetBool("Run", true);
                controller.Move(moveDir * runSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (moving)
            {
                // Animator
                AnimHandler.AnimSetBool("Moving", false);
                AnimHandler.AnimSetBool("Run", false);
                moving = false;
            }
        }
    }

    #endregion

    #region Special Movement

    void SpecialMove()
    {
        // Camera Rotate
        if(cameraInput.x != 0 && cameraInput.y != 0)
        {
            float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, playerCamera.transform.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
            player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        // Movement
        Vector3 direction = new Vector3(move.x, 0f, move.y).normalized;
        if (direction != Vector3.zero)
        {
            // Animator
            moving = true;
            AnimHandler.AnimSetBool("Moving", true);
           
            // Character Move
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * walkSpeed * Time.deltaTime);
        }
        else
        {
            if (moving)
            {
                // Animator
                AnimHandler.AnimSetBool("Moving", false);
                moving = false;
            }
        }
    }

    public IEnumerator WaitSpecialMove(bool move)
    {
        if (move)
        {
            yield return new WaitForSeconds(0.5f);
            specialmoving = true;
            if (AnimHandler.animator.GetBool("Run"))
            {
                AnimHandler.AnimSetBool("Run", false);
            }
        }
        else
        {
            yield return null;
            specialmoving = false;
        }
    }

    #endregion

    #region Lockon Movement
    void LockonMovement()
    {
        Quaternion targetRotation = Quaternion.LookRotation(cameraChange.currentEnemy.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        // Movement
        Vector3 direction = new Vector3(move.x, 0f, move.y).normalized;
        if (direction != Vector3.zero)
        {
            // Animator
            moving = true;
            AnimHandler.AnimSetBool("Moving", true);

            // Character Move
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * walkSpeed * 0.8f * Time.deltaTime);
        }
        else
        {
            if (moving)
            {
                // Animator
                AnimHandler.AnimSetBool("Moving", false);
                moving = false;
            }
        }
    }
    #endregion

    #region Flinch
    public void Flinch(Transform attacker, float distance)
    {
        Vector3 direction = attacker.transform.position - transform.position;

        StartCoroutine(FlinchKnockback(direction, distance));
    }

    IEnumerator FlinchKnockback(Vector3 direction, float distance)
    {
        float timer = 0;
        float duration = 0.5f;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            transform.position += (-direction.normalized) * distance / duration * Time.deltaTime;
            yield return null;
        }
    }
    #endregion

    #region Combat Movement

    public void CombatMovement(float startTime, float distance, bool fast)
    {
        Vector3 moveDir = Quaternion.Euler(0f, player.transform.eulerAngles.y, 0f) * Vector3.forward;

        StartCoroutine(StartTime(startTime, distance, moveDir, fast));
    }

    IEnumerator StartTime(float startTime, float distance, Vector3 moveDir, bool fast)
    {
        //Debug.Log("Timer :" + startTime);
        yield return new WaitForSeconds(startTime);
        //Debug.Log("Finished");
        StartCoroutine(CombatMovement(distance, moveDir, fast));
    }

    IEnumerator CombatMovement(float distance, Vector3 moveDir, bool fast)
    {

        float duration = distance;
        float timer = 0;

        if (cameraChange.lockon)
        {   
            moveDir = cameraChange.currentEnemy.position - transform.position;
            moveDir.y = 0f;
            moveDir = moveDir.normalized;

            float angle = Vector3.SignedAngle(moveDir, transform.forward, Vector3.up);
            transform.eulerAngles += new Vector3(0, -angle, 0);
        }

        if (interact)   // In Action
        {
            if (!fast)
            {
                while (timer <= duration)
                {
                    if (cameraChange.lockon)
                    {
                        if (Vector3.Distance(cameraChange.currentEnemy.position, transform.position) > 1.75f)
                        {
                            controller.Move(moveDir * walkSpeed * Time.deltaTime);
                        }
                    }
                    else
                    {
                        controller.Move(moveDir * walkSpeed * Time.deltaTime);
                    }
                    
                    timer += Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                while (timer <= duration)
                {
                    if (cameraChange.lockon)
                    {
                        if (Vector3.Distance(cameraChange.currentEnemy.position, transform.position) > 1.75f)
                        {
                            controller.Move(moveDir * runSpeed * Time.deltaTime);
                        }
                    }
                    else
                    {
                        controller.Move(moveDir * runSpeed * Time.deltaTime);
                    }
                    timer += Time.deltaTime;
                    yield return null;
                }
            }
        }
        else
        {
            yield return null;
        }
        //Debug.Log("COROUTINE OVER");
    }

    #endregion
}
