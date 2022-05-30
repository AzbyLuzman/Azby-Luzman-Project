using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraChange : MonoBehaviour
{
    public GameObject player;
    public PlayerMove playerMove;
    public SearchRange searchRange;
    public PlayerControls controls;
    public AnimatorHandler AnimHandler;
    public InputHandler inputHandler;
   
    public GameObject mainCamera;
    public GameObject freeCamera;
    public GameObject combatCamera;
    public GameObject stanceCamera;
    public GameObject aimCamera;
    public GameObject lockonCamera;
    public GameObject lockonGroupTarget;
    public GameObject executionCamera;
    public bool lockon;
    private bool lockonDelay, switchOnce;
    public Transform currentEnemy;
    private CinemachineImpulseSource impulseSource;

    public GameObject UIObjects;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.CameraLock.performed += ctx => LockOnCamera(); 
    }
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void Start()
    {
        UIObjects = GameObject.Find("UI Objects");
        player = GameObject.Find("Player");
        playerMove = player.GetComponent<PlayerMove>();
        searchRange = player.GetComponent<SearchRange>();
        AnimHandler = player.transform.GetChild(1).GetComponent<AnimatorHandler>();
        inputHandler = player.GetComponent<InputHandler>();
        mainCamera = transform.GetChild(0).gameObject;
        freeCamera = transform.GetChild(1).gameObject;
        combatCamera = transform.GetChild(2).gameObject;
        stanceCamera = transform.GetChild(3).gameObject;
        aimCamera = transform.GetChild(4).gameObject;
        lockonCamera = transform.GetChild(5).gameObject;
        lockonGroupTarget = transform.GetChild(6).gameObject;
        executionCamera = player.transform.GetChild(2).gameObject;
        lockon = false;
        lockonDelay = false;
        switchOnce = false;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        SwitchLockonInput();
    }
    public void CameraShake()
    {
        impulseSource.GenerateImpulse();
    }

    #region Lockon Camera
    void LockOnCamera()
    {
        if (!lockon && !lockonDelay && searchRange.enemyInSightRange)
        {
            string enemyName = searchRange.LockOnEnemy();
            
            if(enemyName != null)
            {
                // Add EnemyTarget to lockonGroupTarget
                currentEnemy = GameObject.Find(enemyName).transform.GetChild(0);
                if (lockonGroupTarget.GetComponent<CinemachineTargetGroup>().m_Targets.Length == 1)
                {
                    lockonGroupTarget.GetComponent<CinemachineTargetGroup>().AddMember(currentEnemy, 1, 1);
                    currentEnemy.GetChild(0).gameObject.SetActive(true);
                }

                lockon = true;
                lockonDelay = true;
                AnimHandler.AnimSetBool("CameraLock", lockon);

                if (!AnimHandler.animator.GetBool("RTHold"))
                {
                    combatCamera.SetActive(false);
                }
                else
                {
                    stanceCamera.SetActive(false);
                }
                lockonCamera.SetActive(true);
            }
        }
        else if (lockon) 
        {
            lockon = false;
            AnimHandler.AnimSetBool("CameraLock", lockon);

            lockonCamera.SetActive(false);
            if (!AnimHandler.animator.GetBool("RTHold"))
            {
                combatCamera.SetActive(true);
            }
            else
            {
                stanceCamera.SetActive(true);
            }

            // Remove EnemyTarget to lockonGroupTarget
            StartCoroutine(RemoveCurrentEnemy());
        }
    }

    void SwitchLockonInput()
    {
        if (lockon && !switchOnce && (Mathf.Abs(inputHandler.cameraInput.x) > 0.5f))
        {
            switchOnce = true;
            bool direction; // Left = False, Right = True
            direction = inputHandler.cameraInput.x > 0 ? true : false;
            SwitchLockonTarget(direction);

            //Debug.Log("Switch Camera");
        }
        else if (lockon && switchOnce && (Mathf.Abs(inputHandler.cameraInput.x) < 0.5f && Mathf.Abs(inputHandler.cameraInput.y) < 0.5f))
        {
            switchOnce = false;
            //Debug.Log("Switch Reset");
        }
    }

    void SwitchLockonTarget(bool direction)
    {
        // direction Left = False, Right = True
        searchRange.LockOnEnemy();
        string switchEnemy;
        switchEnemy = searchRange.MeasureSwitchDirection(direction, currentEnemy);
        
        //Debug.Log(switchEnemy);

        if (switchEnemy != null)
        {
            Transform newEnemy = GameObject.Find(switchEnemy).transform.GetChild(0);
            if (lockonGroupTarget.GetComponent<CinemachineTargetGroup>().m_Targets.Length == 2)
            {
                lockonGroupTarget.GetComponent<CinemachineTargetGroup>().m_Targets[1].target = GameObject.Find(switchEnemy).transform.GetChild(0);
            }
        }

        if (switchEnemy != null)
        {
            currentEnemy.GetChild(0).gameObject.SetActive(false);
            currentEnemy = GameObject.Find(switchEnemy).transform.GetChild(0);
            currentEnemy.GetChild(0).gameObject.SetActive(true);
        }
    }

    IEnumerator RemoveCurrentEnemy()
    {
        yield return new WaitForSeconds(0.5f);

        if (lockonGroupTarget.GetComponent<CinemachineTargetGroup>().m_Targets.Length > 1)
        {
            lockonGroupTarget.GetComponent<CinemachineTargetGroup>().RemoveMember(currentEnemy);
            currentEnemy.GetChild(0).gameObject.SetActive(false);
        }
        lockonDelay = false;
    }

    #endregion

    #region Special Action
    public void SpecialAction_Stance(bool action)
    {
        if (action)
        {
            //StartCoroutine(RotateSpecial());
            StartCoroutine(playerMove.WaitSpecialMove(true));
            if (!lockon)
            {
                StartCoroutine(RotateSpecial());
                combatCamera.SetActive(false);
                stanceCamera.SetActive(true);
            }
            UIObjects.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(ZoomEffect(true));
        }
        else
        {
            StartCoroutine(playerMove.WaitSpecialMove(false));
            if (!lockon)
            {
                stanceCamera.SetActive(false);
                combatCamera.SetActive(true);
            }
            UIObjects.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(ZoomEffect(false));
            Image image = UIObjects.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
            image.color = new Color(0, 0, 0, 0);
        }
    }

    IEnumerator ZoomEffect(bool zoom)
    {
        Image image = UIObjects.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        if (zoom)
        {
            image.color = new Color(0, 0, 0, 0);
            while (image.color.a < 0.6f)
            {
                image.color += new Color(0, 0, 0, 0.01f);
                yield return null;
            }
        }
        else if (!zoom)
        {
            image.color = new Color(0, 0, 0, 0.6f);
            while (image.color.a > 0)
            {
                image.color -= new Color(0, 0, 0, 0.01f);
                yield return null;
            }
        }
    }
    
    float turnSmoothVelocity = 0f;
    IEnumerator RotateSpecial()
    {
        float saveSpeed = stanceCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed;
        stanceCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 0;
        float turnSmoothTime = 0.1f;
        float targetAngle = mainCamera.transform.eulerAngles.y;
        float timer = 0f, timeLimit = 0.25f;
        while (timer < timeLimit)
        {
            float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            timer += Time.deltaTime;
            yield return null;
        }
        player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, targetAngle, player.transform.eulerAngles.z);
        stanceCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = saveSpeed;
    }

    #endregion

    #region Execution
    public void ExecutionCamera_Start()
    {
        executionCamera.transform.position = mainCamera.transform.position;
        executionCamera.transform.rotation = mainCamera.transform.rotation;
        mainCamera.SetActive(false);
        executionCamera.SetActive(true);
    }

    public void ExecutionCamera_End()
    {
        executionCamera.SetActive(false);
        mainCamera.SetActive(true);
    }

    #endregion
}
