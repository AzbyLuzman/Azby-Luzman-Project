using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Job Name", menuName = "Job/JobType_")]
public class JobType : ScriptableObject
{
    public string jobName;

    [Header("Light Attack")]
    public string[] LightAttack;
    public float[] LightStartTime;
    public float[] LightDuration;
    public bool[] LightSpeedType;
    public Vector3[] LightStats;
    
    [Header("Heavy Attack")]
    public string[] HeavyAttack;
    public float[] HeavyStartTime;
    public float[] HeavyDuration;
    public bool[] HeavySpeedType;
    public Vector3[] HeavyStats;

    [Header("Special Attack")]
    public string[] SpecialAttack;
    public float[] SpecialStartTime;
    public float[] SpecialDuration;
    public bool[] SpecialSpeedType;
    public Vector3[] SpecialStats;

    [Header("Opener : LHSD")]
    [Tooltip("Light, Heavy, Special, Dodge")]
    public string AttackOpener;


    [Header("Execution")]
    public string[] Execution;
    public float[] ExecutionStartTime;
    public float[] ExecutionDuration;
    public bool[] ExecutionSpeedType;


    #region Move Property
    //string _moveName;
    //float _duration;
    //public void MoveProperty(string moveName, float duration)
    //{
    //    _moveName = moveName;
    //    _duration = duration;
    //}
    //public string MoveName
    //{
    //    get => _moveName;
    //    set => _moveName = value;
    //}
    //public float Duration
    //{
    //    get => _duration;
    //    set => _duration = value;
    //}
    #endregion
}
