using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPointer : MonoBehaviour
{
    public float offset = 2.15f;

    public void MovePointerX(float direction)
    {
        transform.localPosition = new Vector3(direction * offset, 0, 0);
    }

    public void MovePointerY(float direction)
    {
        transform.localPosition = new Vector3(0, direction * offset, 0);
    }
}
