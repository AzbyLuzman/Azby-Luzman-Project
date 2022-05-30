using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneManagerScript : MonoBehaviour
{
    SceneKeeper sceneKeeper;
    [HideInInspector]
    public Scene currentScene;

    [Header("Scene with VideoPlayer")]
    public VideoPlayer videoPlayer;

    private void Start()
    {
        sceneKeeper = GameObject.Find("SceneKeeper").GetComponent<SceneKeeper>();

        SetCurrentScene();

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += VideoEnd;
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}
    }

    public void SetCurrentScene()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void VideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        if (currentScene.name == "OpeningScene")
        {
            LoadScene("MenuScene");
        }
        else if (currentScene.name == "NewGameScene")
        {
            LoadScene("StartScene");
        }
        else if (currentScene.name == "RooftopIntroScene")
        {
            LoadScene("ClimbCutscene");
        }
        else if (currentScene.name == "ClimbCutscene")
        {
            LoadScene("RooftopScene");
        }
        else if (currentScene.name == "BasementIntroScene")
        {
            LoadScene("BasementScene");
        }
        else if (currentScene.name == "MainStreetIntroScene")
        {
            LoadScene("MainStreetScene");
        }
    }

    public void LoadScene(string sceneName)
    {
        sceneKeeper.SetPreviousScene();
        SceneManager.LoadScene(sceneName);
    }
}
