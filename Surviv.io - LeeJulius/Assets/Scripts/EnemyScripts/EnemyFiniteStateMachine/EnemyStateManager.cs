using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    private EnemyBaseState currentState;

    private EnemyPatrolState enemyPatrolState = new EnemyPatrolState();
    private EnemyShootPhase enemyShootPhase = new EnemyShootPhase();
    private EnemyReloadPhase enemyReloadPhase = new EnemyReloadPhase();

    [SerializeField] private int changeDirectionInterval;
    [SerializeField] private int shootingInterval;

    private void Start()
    {
        currentState = enemyPatrolState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyStates enemyStates)
    {
        currentState.ExitState(this);

        switch(enemyStates)
        {
            case EnemyStates.PATROL:
                currentState = enemyPatrolState;
                break;

            case EnemyStates.SHOOT:
                currentState = enemyShootPhase;
                break;

            case EnemyStates.RELOAD:
                currentState = enemyReloadPhase;
                break;
        }

        currentState.EnterState(this);
    }

    public int ChangeDirectionInterval { get { return changeDirectionInterval; } }
}

public enum EnemyStates
{
    PATROL,
    SHOOT,
    RELOAD
}