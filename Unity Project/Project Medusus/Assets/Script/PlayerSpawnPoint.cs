using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public SpawnPointData spawnPointData;
    SceneManagerScript sceneManagerScript;
    SceneKeeper sceneKeeper;
    
    private void Start()
    {
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        sceneManagerScript.SetCurrentScene();
        sceneKeeper = GameObject.Find("SceneKeeper").GetComponent<SceneKeeper>();
        int order = CheckSpawnData();
        SpawnPlayer(order);
    }

    int CheckSpawnData()
    {
        bool completed = false;
        int order = 0;
        
        while (!completed)
        {
            if (spawnPointData.previousSceneName[order] == sceneKeeper.previousSceneCheck &&
                spawnPointData.currentSceneName[order] == sceneManagerScript.currentScene.name)
            {
                //Debug.Log(order);
                completed = true;
                break;
            }
            else
            {
                order++;
                if (order > spawnPointData.spawnPoint.Length)
                {
                    break;
                }
            }
        }

        return order;
    }

    void SpawnPlayer(int order)
    {
        Debug.Log(order);
        GameObject player = GameObject.Find("Player");
        player.transform.position = spawnPointData.spawnPoint[order];
    }
}
