using System.Collections;
using UnityEngine;

public abstract class CombatState_Enemy
{
    public abstract void EnterState(EnemyBehaviour enemy);

    public abstract void UpdateState(EnemyBehaviour enemy);
}
