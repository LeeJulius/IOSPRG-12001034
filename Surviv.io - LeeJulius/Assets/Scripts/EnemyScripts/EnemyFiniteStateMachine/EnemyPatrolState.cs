using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.StartCoroutine(ChangeRandomPosition(enemy));
    }
    public override void UpdateState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Patrolling");
        enemy.GetComponent<Rigidbody2D>().position += enemy.GetComponent<EnemyController>().Direction * enemy.GetComponent<EnemyController>().Speed * Time.deltaTime;
    }
    public override void ExitState(EnemyStateManager enemy)
    {
        enemy.StopCoroutine(ChangeRandomPosition(enemy));
    }

    private IEnumerator ChangeRandomPosition(EnemyStateManager enemy)
    {
        int xDirection = Random.Range(-1, 2);
        int yDirection = Random.Range(-1, 2);

        enemy.GetComponent<EnemyController>().Direction = new Vector2(xDirection, yDirection);

        yield return new WaitForSecondsRealtime(enemy.ChangeDirectionInterval);
        yield return enemy.StartCoroutine(ChangeRandomPosition(enemy));
    }
}
