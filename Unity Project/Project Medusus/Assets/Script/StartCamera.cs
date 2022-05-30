using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    SceneKeeper sceneKeeper;
    GameObject player;

    GameObject introObject;
    [Header("Intro Duration")]
    public float introTimer = 3f;

    private void Start()
    {
        sceneKeeper = GameObject.Find("SceneKeeper").GetComponent<SceneKeeper>();
        player = GameObject.Find("Player");
        introObject = this.gameObject;

        if (sceneKeeper.gameStart)
        {
            if (Intro != null)
            {
                StopCoroutine(Intro);
            }
            Intro = StartCoroutine(CameraIntro());
        }
    }

    Coroutine Intro;
    IEnumerator CameraIntro()
    {
        player.GetComponent<CharacterMovement>().enabled = false;
        for (int i = 0; i < introObject.transform.childCount; i++)
        {
            introObject.transform.GetChild(i).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(introTimer);

        player.GetComponent<CharacterMovement>().enabled = true;
        for (int i = 0; i < introObject.transform.childCount; i++)
        {
            introObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        sceneKeeper.gameStart = false;
    }
}
