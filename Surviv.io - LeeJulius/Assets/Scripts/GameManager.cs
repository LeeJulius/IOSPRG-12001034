using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Map")]
    [SerializeField] private GameObject Map;
    private Vector3 mapSize;

    [Header("Spawn Objects Information")]
    [SerializeField] private SpawnObjectData[] Objects;

    [Header("Spawn Enemy Information")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] enemyGuns;
    [SerializeField] private int enemySpawnAmount;

    private void Start()
    {
        // Setting World Bounds
        mapSize = Map.GetComponent<SpriteRenderer>().bounds.size;
        mapSize /= 2;

        // Spawn Objects
        for (int i = 0; i < Objects.Length; i++)
        {
            SpawnObjects(Objects[i].GetItemToSpawn(), Objects[i].GetMinSpawnAmount(), Objects[i].GetMaxSpawnAmount());
        }

        SpawnEnemy(enemyPrefab, enemyGuns, enemySpawnAmount);
    }

    private void SpawnObjects(GameObject itemToSpawn, int minSpawnAmount, int maxSpawnAmount)
    {
        int spawnAmount = Random.Range(minSpawnAmount, maxSpawnAmount);

        for (int i = 0; i < spawnAmount; i++)
        {
            float randomXSpawn = Random.Range(-mapSize.x, mapSize.x);
            float randomYSpawn = Random.Range(-mapSize.y, mapSize.y);
            GameObject itemSpawned = Instantiate(itemToSpawn, new Vector3(randomXSpawn, randomYSpawn, 0), Quaternion.identity);
            itemSpawned.transform.parent = Map.transform;
        } 
    }

    private void SpawnEnemy(GameObject enemyPrefab, GameObject[] enemyGuns, int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            int randomGun = Random.Range(0, enemyGuns.Length);

            float randomXSpawn = Random.Range(-mapSize.x, mapSize.x);
            float randomYSpawn = Random.Range(-mapSize.y, mapSize.y);

            GameObject enemySpawned = Instantiate(enemyPrefab, new Vector3(randomXSpawn, randomYSpawn, 0), Quaternion.identity);
            Transform weaponSpawnLocation = enemySpawned.GetComponent<EnemyController>().EnemyWeaponLocation.transform;

            GameObject equippedGun = Instantiate(enemyGuns[randomGun], weaponSpawnLocation);
            enemySpawned.GetComponent<EnemyController>().EquippedGun = equippedGun;
        }
    }

    [System.Serializable]
    private struct SpawnObjectData
    {
        [SerializeField] private GameObject itemToSpawn;
        [SerializeField] private int minObjectSpawnAmount;
        [SerializeField] private int maxObjectSpawnAmount;

        public GameObject GetItemToSpawn() { return itemToSpawn; }
        public int GetMinSpawnAmount() { return minObjectSpawnAmount; }
        public int GetMaxSpawnAmount() { return maxObjectSpawnAmount; }
    }
}
