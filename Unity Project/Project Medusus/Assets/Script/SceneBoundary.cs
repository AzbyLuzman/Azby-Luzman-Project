using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBoundary : MonoBehaviour
{
    [Header("Scene Boundary - Transition")]
    public float leftSceneBoundary = -10000f;
    public float rightSceneBoundary = 10000f;

    private Transform player;
    private SceneManagerScript sceneManagerScript;

    string transitionObject;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        sceneManagerScript = GetComponent<SceneManagerScript>();
    }

    private void Update()
    {
        // LEFT = FALSE, RIGHT = TRUE
        if (player.transform.position.x < leftSceneBoundary)
        {
            OnBoundaryLoad(false);   
        }
        else if (player.transform.position.x > rightSceneBoundary)
        {
            OnBoundaryLoad(true);
        }

        // Object Transition
        if (Input.GetKeyDown(KeyCode.F) && transitionObject != null)
        {
            if (transitionObject == "RooftopObject")
            {
                sceneManagerScript.LoadScene("RooftopIntroScene");
            }

            if (transitionObject == "BasementObject")
            {
                sceneManagerScript.LoadScene("BasementIntroScene");
            }
        }
    }

    // LEFT = FALSE, RIGHT = TRUE
    void OnBoundaryLoad(bool flag)
    {
        if (sceneManagerScript.currentScene.name == "StartScene")
        {
            if (!flag)
            {
                sceneManagerScript.LoadScene("MainStreetScene");
            }
            else
            {

            }
        }

        if (sceneManagerScript.currentScene.name == "MainStreetScene")
        {
            if (!flag)
            {
                
            }
            else
            {
                sceneManagerScript.LoadScene("StartScene");
            }
        }

        if (sceneManagerScript.currentScene.name == "BasementScene")
        {
            if (!flag)
            {
                sceneManagerScript.LoadScene("MainStreetIntroScene");
            }
            else
            {
                
            }
        }
    }

    public void ObjectEnter(string objName, bool flag)
    {
        if (flag)
        {
            transitionObject = objName;
        }
        else
        {
            transitionObject = null;
        }
    }
}
