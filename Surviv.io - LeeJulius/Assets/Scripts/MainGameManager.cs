using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : Singleton<MainGameManager>
{
    [Header("Map")]
    [SerializeField] private GameObject world;

    private GameObject map;
    private GameObject remainingPlayers;
    private GameObject bulletsLocation;

    [SerializeField] private GameObject playerPrefab;

    private List<GameObject> enemiesAlive;

    public GameObject LoadWorld()
    {
        GameObject _loadedWorld = Instantiate(world, new Vector3(0, 0, 0), Quaternion.identity);

        map = _loadedWorld.transform.Find("Map").gameObject;
        remainingPlayers = _loadedWorld.transform.Find("RemainingPlayers").gameObject;
        bulletsLocation = _loadedWorld.transform.Find("BulletsLocation").gameObject;

        return _loadedWorld;
    }

    public void StartGame()
    {
        enemiesAlive = new List<GameObject>();
        SpawnerManager.instance.SpawnAll();

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        GameObject playerSpawned = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        playerSpawned.transform.parent = remainingPlayers.transform;
    }

    public void AddEnemyAlive(GameObject enemySpawned)
    {
        enemiesAlive.Add(enemySpawned);
    }

    public void UpdateEnemiesAlive(GameObject killedEnemy)
    {
        foreach (GameObject enemies in enemiesAlive)
        {
            if (enemies == killedEnemy)
            {
                enemiesAlive.Remove(enemies);
                break;
            }
        }

        if (enemiesAlive.Count <= 0)
        {
            GameManager.instance.EndGame(GameResult.PLAYER_WIN);
        }
    }

    public GameObject Map { get { return map; } }
    public GameObject BulletsLocation { get { return bulletsLocation; } }
    public GameObject RemainingPlayers { get { return remainingPlayers; } }
}