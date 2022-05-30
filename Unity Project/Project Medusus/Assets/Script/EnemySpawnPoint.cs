using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public SpawnPointData spawnPointData;
    public GameObject enemyPrefab;
    SceneManagerScript sceneManagerScript;
    SceneKeeper sceneKeeper;

    private void Start()
    {
        sceneKeeper = GameObject.Find("SceneKeeper").GetComponent<SceneKeeper>();
        if (sceneKeeper.bossEnable)
        {
            sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
            sceneManagerScript.SetCurrentScene();

            int order = CheckSpawnData();
            SpawnEnemy(order);
        }
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

    void SpawnEnemy(int order)
    {
        Debug.Log(order);
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = spawnPointData.spawnPoint[order];
        enemy.transform.localScale = spawnPointData.spawnScale[order];

        enemy.GetComponent<EnemyScript>().offset = spawnPointData.offSet[order];
        enemy.GetComponent<EnemyScript>().EnemyStandby(2f);
    }
}