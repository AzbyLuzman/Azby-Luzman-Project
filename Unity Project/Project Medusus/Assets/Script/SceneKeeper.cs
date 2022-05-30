using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneKeeper : MonoBehaviour
{
    public Scene previousScene;
    public string previousSceneCheck;

    public bool gameStart;
    public bool bossEnable;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPreviousScene()
    {
        previousScene = SceneManager.GetActiveScene();
        previousSceneCheck = previousScene.name;
        SetEventTrigger();
    }

    void SetEventTrigger()
    {
        if (previousSceneCheck == "MenuScene")
        {
            gameStart = false;
        }
        if (previousSceneCheck == "NewGameScene")
        {
            gameStart = true;
            bossEnable = false;
        }
    }
}
