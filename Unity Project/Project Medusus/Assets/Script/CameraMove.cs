using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public float boundaryLeft = -500;
    public float boundaryRight = 500;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        transform.position = new Vector2(player.transform.position.x, transform.position.y);
        if (transform.position.x < boundaryLeft)
        {
            transform.position = new Vector3(boundaryLeft, transform.position.y);
        }
        if (transform.position.x > boundaryRight)
        {
            transform.position = new Vector3(boundaryRight, transform.position.y);
        }
    }
}
