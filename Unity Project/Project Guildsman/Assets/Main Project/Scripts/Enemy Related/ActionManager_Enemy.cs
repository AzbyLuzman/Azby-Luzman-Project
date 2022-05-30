using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager_Enemy : MonoBehaviour
{
    public EnemyBehaviour enemyBehaviour;
    public JobType jobType;
    string currentAction;

    private void Start()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    public void ReadClipInfo(string clipName)
    {
        currentAction = clipName;
        ActionValueCheck();
    }

    void ActionValueCheck()
    {
        bool actionCheck = false;

        while (!actionCheck)
        {
            for (int i = 0; i < jobType.LightAttack.Length; i++)
            {
                if (currentAction == jobType.LightAttack[i])
                {
                    actionCheck = true;
                    enemyBehaviour.statsManager.DecreaseStat(StatsManager.StatsType.stamina, jobType.LightStats[i].y);
                    enemyBehaviour.CombatMovement(jobType.LightStartTime[i], jobType.LightDuration[i], jobType.LightSpeedType[i]);
                    break;
                }
            }

            for (int i = 0; i < jobType.HeavyAttack.Length; i++)
            {
                if (currentAction == jobType.HeavyAttack[i])
                {
                    actionCheck = true;
                    enemyBehaviour.statsManager.DecreaseStat(StatsManager.StatsType.stamina, jobType.HeavyStats[i].y);
                    enemyBehaviour.CombatMovement(jobType.HeavyStartTime[i], jobType.HeavyDuration[i], jobType.HeavySpeedType[i]);
                    break;
                }
            }

            for (int i = 0; i < jobType.SpecialAttack.Length; i++)
            {
                if (currentAction == jobType.SpecialAttack[i])
                {
                    actionCheck = true;
                    enemyBehaviour.statsManager.DecreaseStat(StatsManager.StatsType.stamina, jobType.SpecialStats[i].y);
                    enemyBehaviour.CombatMovement(jobType.SpecialStartTime[i], jobType.SpecialDuration[i], jobType.SpecialSpeedType[i]);
                    break;
                }
            }
        }
    }
}
