using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : BaseSpawner
{
    [SerializeField] private EnemySpawnerStruct[] enemySpawnerData;

    protected override void Start()
    {
        SpawnerManager.instance.RegisterMenu(this);
    }

    public override void Spawn()
    {
        base.Spawn();

        for (int i = 0; i < enemySpawnerData.Length; i++)
        {
            SpawnEnemy(i);
        }
    }

    private void SpawnEnemy(int currentEnemyIndex)
    {
        EnemySpawnerStruct currentEnemyInfo = enemySpawnerData[currentEnemyIndex];

        int minSpawn = currentEnemyInfo.EnemySpawnInformation.MinSpawnAmount;
        int maxSpawn = currentEnemyInfo.EnemySpawnInformation.MaxSpawnAmount;

        int spawnAmount = Random.Range(minSpawn, maxSpawn);

        for (int i = 0; i < spawnAmount; i++)
        {
            int randomGun = Random.Range(0, currentEnemyInfo.EnemyGuns.Length);

            float randomXSpawn = Random.Range(-mapSize.x, mapSize.x);
            float randomYSpawn = Random.Range(-mapSize.y, mapSize.y);

            GameObject enemySpawned = Instantiate(currentEnemyInfo.EnemySpawnInformation.ObjectPrefab, new Vector3(randomXSpawn, randomYSpawn, 0), Quaternion.identity);
            enemySpawned.transform.parent = MainGameManager.instance.RemainingPlayers.transform;

            Transform weaponSpawnLocation = enemySpawned.GetComponent<EnemyController>().WeaponLocation.transform;
            GameObject equippedGun = Instantiate(currentEnemyInfo.EnemyGuns[randomGun], weaponSpawnLocation);
            enemySpawned.GetComponent<EnemyController>().EquippedGun = equippedGun;

            MainGameManager.instance.AddEnemyAlive(enemySpawned);
        }
    }

    [System.Serializable]
    private struct EnemySpawnerStruct
    {
        [Header("Spawn Amount")]
        [SerializeField] private SpawnAmount enemySpawnInformation;

        [Header("Equipped Guns")]
        [SerializeField] private GameObject[] enemyGuns;

        public SpawnAmount EnemySpawnInformation { get { return enemySpawnInformation; } }

        public GameObject[] EnemyGuns { get { return enemyGuns; } }
    }
}
