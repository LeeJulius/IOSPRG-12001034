using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReloadPhase : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.StartCoroutine(ReloadingGun(enemy));
    }
    public override void UpdateState(EnemyStateManager enemy)
    {

    }
    public override void ExitState(EnemyStateManager enemy)
    {
        enemy.StopCoroutine(ReloadingGun(enemy));
    }

    private IEnumerator ReloadingGun(EnemyStateManager enemy)
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        GameObject equippedGun = enemyController.EquippedGun;
        GunComponent gunComponent = equippedGun.GetComponent<GunComponent>();

        yield return enemy.StartCoroutine(gunComponent.Reload(gunComponent.MaxClip));

        if (enemyController.Targets.Count > 0)
            enemy.SwitchState(EnemyStates.SHOOT);
        else
            enemy.SwitchState(EnemyStates.PATROL);
    }
}
