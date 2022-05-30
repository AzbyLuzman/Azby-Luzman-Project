using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Color beforeColor;
    public Color afterColor;
    public SpriteRenderer crouch_UI;
    public SpriteRenderer dash_UI;

    private void Start()
    {
        crouch_UI = transform.Find("Crouch Icon").GetComponent<SpriteRenderer>();
        dash_UI = transform.Find("Dash Icon").GetComponent<SpriteRenderer>();
    }

    public void Crouch_UI(bool trigger)
    {
        if (!trigger)
        {
            crouch_UI.color = beforeColor;
        }
        else
        {
            crouch_UI.color = afterColor;
        }
    }

    public void Dash_UI(bool trigger)
    {
        if (!trigger)
        {
            dash_UI.color = beforeColor;
        }
        else
        {
            dash_UI.color = afterColor;
        }
    }
}
