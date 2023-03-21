using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : BaseSpawner
{
    [SerializeField] private ObstacleSpawnerStruct[] obstacleSpawnData;

    protected override void Start()
    {
        base.Start();
    }

    public override void Spawn()
    {
        base.Spawn();

        for (int i = 0; i < obstacleSpawnData.Length; i++)
        {
            SpawnObstacle(i);
        }
    }

    public void SpawnObstacle(int currentObstacle)
    {
        ObstacleSpawnerStruct currentGunStruct = obstacleSpawnData[currentObstacle];

        int minSpawn = currentGunStruct.ObstacleSpawnInformation.MinSpawnAmount;
        int maxSpawn = currentGunStruct.ObstacleSpawnInformation.MaxSpawnAmount;

        int spawnAmount = Random.Range(minSpawn, maxSpawn);

        for (int i = 0; i < spawnAmount; i++)
        {
            float randomXSpawn = Random.Range(-mapSize.x, mapSize.x);
            float randomYSpawn = Random.Range(-mapSize.y, mapSize.y);
            GameObject itemSpawned = Instantiate(currentGunStruct.ObstacleSpawnInformation.ObjectPrefab, new Vector3(randomXSpawn, randomYSpawn, 0), Quaternion.identity);
            itemSpawned.transform.parent = map.transform;
        }
    }

    [System.Serializable]
    private struct ObstacleSpawnerStruct
    {
        [Header("Spawn Amount")]
        [SerializeField] private SpawnAmount obstacleSpawnInformation;

        public SpawnAmount ObstacleSpawnInformation { get { return obstacleSpawnInformation; } }
    }
}
