using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipOpening : MonoBehaviour
{
    SceneManagerScript sceneManagerScript;

    private void Start()
    {
        sceneManagerScript = GetComponent<SceneManagerScript>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))            
        {
            sceneManagerScript.LoadScene("MenuScene");
        }
    }
}
