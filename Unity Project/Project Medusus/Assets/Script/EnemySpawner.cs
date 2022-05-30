using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    SceneKeeper sceneKeeper;
    public GameObject enemyPrefab;

    private void Start()
    {
        sceneKeeper = GameObject.Find("SceneKeeper").GetComponent<SceneKeeper>();
        if (!sceneKeeper.bossEnable)
        {
            StartCoroutine(EnemySpawn());
        }
    }

    IEnumerator EnemySpawn()
    {
        GameObject heartbeat = GameObject.Find("AudioObject").transform.Find("Heartbeat").gameObject;
        heartbeat.SetActive(true);

        yield return new WaitForSeconds(5f);
        heartbeat.SetActive(false);

        sceneKeeper.bossEnable = true;
        GameObject enemy = Instantiate(enemyPrefab, this.transform.position, this.transform.rotation);
        enemy.GetComponent<EnemyScript>().offset = new Vector3(-450, 150, 0);
        enemy.GetComponent<EnemyScript>().EnemyStandby(4f);
    }
}
