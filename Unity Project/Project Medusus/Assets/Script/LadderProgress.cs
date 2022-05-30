using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderProgress : MonoBehaviour
{
    GameObject ladder;

    SceneKeeper sceneKeeper;

    private void Start()
    {
        sceneKeeper = GameObject.Find("SceneKeeper").GetComponent<SceneKeeper>();

        ladder = transform.GetChild(0).gameObject;

        if (!sceneKeeper.bossEnable)
        {
            ladder.SetActive(false);
        }
        else
        {
            ladder.SetActive(true);
        }
    }
}
