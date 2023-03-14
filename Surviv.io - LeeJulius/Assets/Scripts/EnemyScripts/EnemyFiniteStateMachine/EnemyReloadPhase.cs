using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReloadPhase : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {

    }
    public override void UpdateState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Reloading");
    }
    public override void ExitState(EnemyStateManager enemy)
    {

    }
}
