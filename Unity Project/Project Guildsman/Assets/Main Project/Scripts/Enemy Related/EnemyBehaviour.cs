using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    #region Initialize

    public GameObject target;
    public NavMeshAgent agent;
    public LayerMask playerMask;
    public LayerMask partyMask;
    public LayerMask stageMask;
    public EnemySight enemySight;

    [Header("States")]
    public bool targetInSightRange;
    public bool targetInAttackRange;

    [Header("Patrol")]
    public Vector3 patrolPoint;
    bool patrolSet;
    [Range(10f, 30f)]
    public float patrolRange = 20f;
    [Range(10f, 20f)]
    public float sightRange = 15f;
    [Range(1f, 10f)]
    public float attackRange = 5f;

    // Gameplay
    private float normalSpeed, slowerSpeed;
    private float normalAcc, slowerAcc;
    //private bool setAttack, waitAttack;
    //private string attackType;
    public bool inCombat;
    private bool flinch;

    // Properties
    public bool interupted;
    public AnimatorHandler_AI animatorHandler_AI;
    public AnimatorCombo_Enemy animatorCombo_Enemy;
    public StatsManager statsManager;
    public ActionManager_Enemy actionManager_Enemy;

    // Combat State Machine
    CombatState_Enemy combatState;
    public DefensiveState_Enemy defensiveState = new DefensiveState_Enemy();
    public CounterState_Enemy counterState = new CounterState_Enemy();
    public OffensiveState_Enemy offensiveState = new OffensiveState_Enemy();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerMask = LayerMask.GetMask("Player");
        partyMask = LayerMask.GetMask("Party");
        stageMask = LayerMask.GetMask("Stages");
        enemySight = GetComponent<EnemySight>();
        normalSpeed = agent.speed;
        slowerSpeed = agent.speed * 0.6f;
        normalAcc = agent.acceleration;
        slowerAcc = agent.acceleration * 0.25f;
        //setAttack = false;
        //waitAttack = false;
        //attackType = null;
        interupted = false;
        flinch = false;

        animatorHandler_AI = transform.GetChild(1).GetComponent<AnimatorHandler_AI>();
        animatorCombo_Enemy = transform.GetChild(1).GetComponent<AnimatorCombo_Enemy>();
        statsManager = GetComponent<StatsManager>();
        actionManager_Enemy = GetComponent<ActionManager_Enemy>();

        combatState = defensiveState;
        combatState.EnterState(this);
    }

    #endregion

    private void Update()
    {
        targetInSightRange = Physics.CheckSphere(transform.position, sightRange, partyMask + playerMask);
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, partyMask + playerMask);

        #region Temporary

        if (Vector3.Magnitude(agent.velocity) != 0)
        {
            animatorHandler_AI.AnimSetBool("Moving", true);
        }
        else if (animatorHandler_AI.animator.GetBool("Moving"))
        {
            animatorHandler_AI.AnimSetBool("Moving", false);
        }

        if (target != null && inCombat)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            //Debug.Log(enemyBehaviour.target.name);
        }

        #endregion

        if (!interupted)
        {
            // Patrol
            SetPatrol();

            // In Sight
            SetChase();

            // Select Target - Skip for now

            // Chase - SetChase()

            // In Attack Range
            EnterCombat();
        }
    }

    #region Patrol
    void SetPatrol()
    {
        if ((!targetInSightRange && !targetInAttackRange) || !enemySight.detected)
        {
            if (!patrolSet)
            {
                Patrolling();
            }

            if (patrolPoint == transform.position)
            {
                patrolSet = false;
            }
        }
        else if (patrolSet)
        {
            patrolSet = false;
        }
    }

    void Patrolling()
    {
        float pointX = Random.Range(-patrolRange, patrolRange);
        float pointZ = Random.Range(-patrolRange, patrolRange);
        patrolPoint = new Vector3(transform.position.x + pointX, transform.position.y, transform.position.z + pointZ);

        if (Physics.Raycast(patrolPoint, -transform.up, 10f, stageMask))
        {
            agent.SetDestination(patrolPoint);
            patrolSet = true;
            agent.speed = normalSpeed;
            agent.acceleration = normalAcc;
        }
    }

    #endregion

    #region Chase
    void SetChase()
    {
        if ((targetInSightRange && !targetInAttackRange) || (targetInSightRange && targetInAttackRange && target == null))
        {
            if (!enemySight.inView)
            {
                enemySight.inView = true;
                agent.speed = normalSpeed;
                agent.acceleration = normalAcc;
            }
            if (enemySight.detected)
            {
                Chasing();
            }
        }
        else if (enemySight.inView)
        {
            enemySight.inView = false;
        }
    }

    void Chasing()
    {
        agent.SetDestination(target.transform.position);
    }

    #endregion

    #region Combat

    void EnterCombat()
    {
        if (targetInSightRange && targetInAttackRange && target != null)
        {
            if (!inCombat)
            {
                inCombat = true;
            }

            // Getting Closer
            if (Vector3.Distance(target.transform.position, transform.position) > attackRange * 0.8f)
            {
                if (agent.isStopped)
                {
                    agent.isStopped = false;
                }
                agent.speed = slowerSpeed;
                agent.acceleration = slowerAcc;
                agent.SetDestination(target.transform.position);
            }
            else
            {
                if (!agent.isStopped)
                {
                    agent.isStopped = true;
                }

                // Attacking
                Attack();
                
            }

        }
        else if (inCombat)
        {
            inCombat = false;
        }
    }

    void Attack()
    {
        combatState.UpdateState(this);

        //if (!setAttack)
        //{
        //    attackType = SetAttack();
        //}

        //if (setAttack & !waitAttack)
        //{
        //    waitAttack = true;
        //    animatorHandler_AI.animator.Play(attackType);
        //    StartCoroutine(WaitAttack());
        //}
    }

    public void SwitchCombatState(CombatState_Enemy state)
    {
        combatState = state;
        combatState.EnterState(this);
    }

    //string SetAttack()
    //{
    //    setAttack = true;
    //    float range = Random.Range(0, 1);
    //    if (range == 0)
    //    {
    //        return "Light";
    //    }
    //    else
    //    {
    //        return "Heavy";
    //    }
    //}

    //IEnumerator WaitAttack()
    //{
    //    yield return new WaitForSeconds(5f);
    //    waitAttack = false;
    //    setAttack = false;
    //}

    #endregion

    #region Flinch
    public void Flinch(Transform attacker, float distance)
    {
        Vector3 direction = attacker.transform.position - transform.position;

        if (!flinch)
        {
            StartCoroutine(FlinchKnockback(direction, distance));
        }
    }

    IEnumerator FlinchKnockback(Vector3 direction, float distance)
    {
        flinch = true;
        float timer = 0;
        float duration = 0.5f;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            transform.position += (-direction.normalized) * distance / duration * Time.deltaTime;
            yield return null;
        }
        flinch = false;
    }

    #endregion

    #region CombatMovement

    public void CombatMovement(float startTime, float distance, bool fast)
    {
        Vector3 moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward;

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

        if (target != null)
        {
            moveDir = target.transform.position - transform.position;
            moveDir.y = 0f;
            moveDir = moveDir.normalized;

            float angle = Vector3.SignedAngle(moveDir, transform.forward, Vector3.up);
            transform.eulerAngles += new Vector3(0, -angle, 0);
        }

        if (!fast)
        {
            while (timer <= duration)
            {
                if (Vector3.Distance(target.transform.position, transform.position) > 1.75f)
                {
                    agent.Move(moveDir * slowerSpeed * Time.deltaTime);
                    //Debug.Log(Vector3.Distance(target.transform.position, transform.position));
                }
                timer += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (timer <= duration)
            {
                if (Vector3.Distance(target.transform.position, transform.position) > 1.75f)
                {
                    agent.Move(moveDir * normalSpeed * Time.deltaTime);
                    //Debug.Log(Vector3.Distance(target.transform.position, transform.position));
                }
                timer += Time.deltaTime;
                yield return null;
            }
        }
        //Debug.Log("COROUTINE OVER");
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
