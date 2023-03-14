using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyStateManager enemyStateManager;

    [SerializeField] private GameObject enemyWeaponLocation;
    [SerializeField] private int speed;
    private List<GameObject> targets;
    private GameObject equippedGun;
    private Vector2 direction;

    private void Start()
    {
        enemyStateManager = GetComponent<EnemyStateManager>();
        targets = new List<GameObject>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // bug trying to target other enemies
        if (collision.name.StartsWith("Player") || collision.name.StartsWith("Enemy"))
        {
            targets.Add(collision.gameObject);
            enemyStateManager.SwitchState(EnemyStates.SHOOT);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.StartsWith("Player") || collision.name.StartsWith("Enemy"))
        {
            targets.Remove(collision.gameObject);

            if (targets.Count <= 0)
            {
                enemyStateManager.SwitchState(EnemyStates.PATROL);
            }
        }
    }

    public int Speed { get { return speed; } }
    public GameObject EnemyWeaponLocation { get { return enemyWeaponLocation; } }
    public List<GameObject> Targets { get { return targets; } }
    public GameObject EquippedGun { get { return equippedGun; } set { equippedGun = value; } }
    public Vector2 Direction { get { return direction; } set { direction = value; } }
}
