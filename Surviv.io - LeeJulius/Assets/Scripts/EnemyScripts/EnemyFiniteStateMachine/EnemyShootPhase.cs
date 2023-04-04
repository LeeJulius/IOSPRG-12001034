using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootPhase : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        enemy.StartCoroutine(ShootGun(enemyController.EquippedGun, enemy));
    }
    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.GetComponent<Rigidbody2D>().position += enemy.GetComponent<EnemyController>().Direction * enemy.GetComponent<EnemyController>().Speed / 2 * Time.deltaTime;

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        float enemyRotation = GetRotation(enemyController.Targets[0], enemy.gameObject);
        enemyController.WeaponLocation.transform.eulerAngles = new Vector3(0, 0, enemyRotation);

        GameObject equippedGun = enemyController.EquippedGun;
        GunComponent gunComponent = equippedGun.GetComponent<GunComponent>();
        gunComponent.BulletSpawnLocation.transform.eulerAngles = new Vector3(0, 0, enemyRotation);
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        enemy.StopCoroutine(ShootGun(enemy.gameObject.GetComponent<EnemyController>().EquippedGun, enemy));
    }

    private float GetRotation(GameObject Target, GameObject Self)
    {
        // Getting x and y
        float verticalChange = Target.transform.position.y - Self.transform.position.y;
        float horizontalChange = Target.transform.position.x - Self.transform.position.x;

        // Getting angle using trigonemtry (T-O-A)
        return Mathf.Atan2(verticalChange, horizontalChange) * Mathf.Rad2Deg;
    }

    private IEnumerator ShootGun(GameObject _equippedGun, EnemyStateManager enemy)
    {
        GunComponent gunComponent = _equippedGun.GetComponent<GunComponent>();

        if (gunComponent.CurrentClip <= 0)
        {
            enemy.SwitchState(EnemyStates.RELOAD);
        }

        yield return new WaitForSecondsRealtime(Random.Range(enemy.ShootingIntervalMin, enemy.ShootingIntervalMax));

        yield return enemy.StartCoroutine(gunComponent.Shoot(gunComponent.BulletSpawnLocation));

        yield return new WaitForSecondsRealtime(gunComponent.FireRate);

        if (enemy.GetComponent<EnemyController>().Targets.Count <= 0)
        {
            enemy.SwitchState(EnemyStates.PATROL);
        }
        else
        {
            enemy.StartCoroutine(ShootGun(_equippedGun, enemy));
        }
    }
}
