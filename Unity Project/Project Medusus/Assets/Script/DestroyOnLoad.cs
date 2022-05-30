using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnLoad : MonoBehaviour
{
    Scene currentScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name != "TutorialScene")
        {
            if (GameObject.Find("MenuBGM(Clone)") != null)
            {
                Destroy(GameObject.Find("MenuBGM(Clone)"));
                MenuBGMScript.created = false;
            }            
        }
    }
}
