using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Transform player;
    Animator animator;
    public Vector3 offset;
    public float speed = 100f;
    bool moving;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        animator = transform.GetChild(0).GetComponent<Animator>();
        moving = false;
    }

    private void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z), speed * Time.deltaTime);
        }
    }

    public void EnemyStandby(float timer)
    {
        if (Standby != null)
        {
            StopCoroutine(Standby);
        }
        Standby = StartCoroutine(OnStandby(timer));
    }
    Coroutine Standby;
    IEnumerator OnStandby(float timer)
    {
        moving = false;
        yield return new WaitForSeconds(timer);
        moving = true;
        animator.SetBool("Sprint", true);
    }
}
