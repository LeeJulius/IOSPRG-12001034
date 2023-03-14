using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootPhase : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        if (enemyController.GetComponent<EnemyController>().Targets.Count <= 0)
            return;
    }
    public override void UpdateState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Shooting");
        enemy.GetComponent<Rigidbody2D>().position += enemy.GetComponent<EnemyController>().Direction * enemy.GetComponent<EnemyController>().Speed / 2 * Time.deltaTime;

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        float enemyRotation = GetRotation(enemyController.Targets[0], enemy.gameObject);
        enemyController.EnemyWeaponLocation.transform.eulerAngles = new Vector3(0, 0, enemyRotation);
    }

    public override void ExitState(EnemyStateManager enemy)
    {

    }

    private float GetRotation(GameObject Target, GameObject Self)
    {
        // Getting x and y
        float verticalChange = Target.transform.position.y - Self.transform.position.y;
        float horizontalChange = Target.transform.position.x - Self.transform.position.x;

        // if no change return nothing (stationary)
        if (horizontalChange == 0 && verticalChange == 0)
            return float.NaN;

        // Getting angle using trigonemtry (T-O-A)
        return Mathf.Atan2(verticalChange, horizontalChange) * Mathf.Rad2Deg;
    }
}
