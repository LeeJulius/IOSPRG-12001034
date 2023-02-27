using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Map")]
    [SerializeField] private GameObject Map;
    private Vector3 mapSize;

    [Header("Spawn Information")]
    [SerializeField] private SpawnObjectData[] Objects;

    // Start is called before the first frame update
    private void Start()
    {
        // Setting World Bounds
        mapSize = Map.GetComponent<SpriteRenderer>().bounds.size;
        mapSize /= 2;

        for (int i = 0; i < Objects.Length; i++)
        {
            SpawnObjects(Objects[i].GetItemToSpawn(), Objects[i].GetMinSpawnAmount(), Objects[i].GetMaxSpawnAmount());
        }
        
    }

    private void SpawnObjects(GameObject itemToSpawn, int minSpawnAmount, int maxSpawnAmount)
    {
        int spawnAmount = Random.Range(minSpawnAmount, maxSpawnAmount);

        for (int i = 0; i < spawnAmount; i++)
        {
            float randomXSpawn = Random.Range(-mapSize.x, mapSize.x);
            float randomYSpawn = Random.Range(-mapSize.y, mapSize.y);
            Instantiate(itemToSpawn, new Vector3(randomXSpawn, randomYSpawn, 0), Quaternion.identity);
        } 
    }

    [System.Serializable]
    private struct SpawnObjectData
    {
        [SerializeField] private GameObject itemToSpawn;
        [SerializeField] private int minSpawnAmount;
        [SerializeField] private int maxSpawnAmount;

        public GameObject GetItemToSpawn() { return itemToSpawn; }
        public int GetMinSpawnAmount() { return minSpawnAmount; }
        public int GetMaxSpawnAmount() { return maxSpawnAmount; }
    }
}
