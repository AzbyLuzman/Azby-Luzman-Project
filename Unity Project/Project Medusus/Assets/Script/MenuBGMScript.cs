using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGMScript : MonoBehaviour
{
    public static bool created = false;
    public GameObject MenuBGM;

    private void Start()
    {
        if (!created)
        {
            created = true;
            Instantiate(MenuBGM);
        }
    }
}
