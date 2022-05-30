using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Spawn Point", menuName = "Spawn Data")]
public class SpawnPointData : ScriptableObject
{
    public string[] previousSceneName;
    public string[] currentSceneName;
    public Vector3[] spawnPoint;

    [Header("Enemy")]
    public Vector3[] spawnScale;
    public Vector3[] offSet;
    public float[] speed;
}

