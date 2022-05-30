using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public GameObject character;
    public CharacterController characterController;
    public Animator animator;
    public AudioManager audioManager;
    public UIManager uiManager;
    public ProfileIconEffect profileIconEffect;

    public float speed = 100f;
    public float runSpeed = 200f;
    public float crouchSpeed = 50f;
    public bool move, run, crouch;
    float horizontal;
    float vertical;

    public float boundaryUp = 100f;
    public float boundaryDown = 0f;
    public float boundaryLeft = -10000f;
    public float boundaryRight = 10000f;

    // UI Pointer
    MovingPointer movingPointer;

    private void Start()
    {
        character = this.gameObject;
        characterController = GetComponent<CharacterController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        audioManager = GameObject.Find("AudioObject").GetComponent<AudioManager>();
        uiManager = GameObject.Find("UI Canvas").GetComponent<UIManager>();
        movingPointer = GameObject.Find("Moving Pointer").GetComponent<MovingPointer>();
        profileIconEffect = GameObject.Find("Character Profile").GetComponent<ProfileIconEffect>();
    }

    private void Update()
    {
        Run();
        Movement();
        Crouch();
    }

    void Movement()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");
        //move = horizontal == 0 && vertical == 0 ? false : true;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            move = true;

            if (!animator.GetBool("Moving"))
            {
                animator.SetBool("Moving", true);
                audioManager.PlayWalkAudio(true);
            }

            horizontal = 0;
            if (Input.GetKey(KeyCode.A))
            {
                if (transform.position.x >= boundaryLeft)
                {
                    horizontal = -1f;
                    movingPointer.MovePointerX(horizontal);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (transform.position.x <= boundaryRight)
                {
                    horizontal = 1f;
                    movingPointer.MovePointerX(horizontal);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
                }
            }

            vertical = 0;
            if (Input.GetKey(KeyCode.W))
            {
                if (transform.position.y <= boundaryUp)
                {
                    vertical = 1f;
                    movingPointer.MovePointerY(vertical);
                }

            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (transform.position.y >= boundaryDown)
                {
                    vertical = -1f;
                    movingPointer.MovePointerY(vertical);
                }
            }
        }
        else if (move)
        {
            if (animator.GetBool("Moving"))
            {
                animator.SetBool("Moving", false);
                audioManager.PlayWalkAudio(false);
            }
            move = false;
            horizontal = 0;
            vertical = 0;
            movingPointer.MovePointerX(horizontal);
            movingPointer.MovePointerY(vertical);
        }

        if (move && !run && !crouch)
        {
            characterController.Move(new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime, 0));
        }
        else if (move && run && !crouch)
        {
            characterController.Move(new Vector3(horizontal * runSpeed * Time.deltaTime, vertical * speed * Time.deltaTime, 0));
        }
        else if (move && !run && crouch)
        {
            characterController.Move(new Vector3(horizontal * crouchSpeed * Time.deltaTime, vertical * speed * Time.deltaTime, 0));
        }
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!animator.GetBool("Running"))
            {
                animator.SetBool("Running", true);
                audioManager.PlayRunAudio(true);
                uiManager.Dash_UI(true);
            }
            run = true;
        }
        else if (animator.GetBool("Running"))
        {
            animator.SetBool("Running", false);
            audioManager.PlayRunAudio(false);
            uiManager.Dash_UI(false);
            run = false;
        }
    }

    void Crouch()
    {
        if (!animator.GetBool("Moving") && (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftControl)))
        {
            if (!animator.GetBool("Crouch"))
            {
                animator.SetBool("Crouch", true);
                crouch = true;

                Color color = profileIconEffect.gameObject.GetComponent<SpriteRenderer>().color;
                profileIconEffect.gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.6f);

                if (DelayedHeartbeat != null)
                {
                    StopCoroutine(DelayedHeartbeat);
                }
                DelayedHeartbeat = StartCoroutine(HeartbeatDelay());
            }
            else if (animator.GetBool("Crouch"))
            {
                animator.SetBool("Crouch", false);
                crouch = false;

                Color color = profileIconEffect.gameObject.GetComponent<SpriteRenderer>().color;
                profileIconEffect.gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1f);

                audioManager.PlayHeartbeatAudio(false);
                uiManager.Crouch_UI(false);
            }
        }
    }

    Coroutine DelayedHeartbeat;
    IEnumerator HeartbeatDelay()
    {
        yield return new WaitForSeconds(1f);
        audioManager.PlayHeartbeatAudio(true);
        uiManager.Crouch_UI(true);
    }
}
