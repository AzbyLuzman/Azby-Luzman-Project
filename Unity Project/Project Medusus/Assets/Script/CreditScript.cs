using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScript : MonoBehaviour
{
    SceneManagerScript sceneManagerScript;

    private void Start()
    {
        sceneManagerScript = GetComponent<SceneManagerScript>();    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            sceneManagerScript.LoadScene("MenuScene");
        }
    }
}
