using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : BaseSpawner
{
    [SerializeField] private GunSpawnerStruct[] gunSpawnData;

    protected override void Start()
    {
        base.Start();
    }

    public override void Spawn()
    {
        base.Spawn();

        for (int i = 0; i < gunSpawnData.Length; i++)
        {
            SpawnGun(i);
        }
    }

    private void SpawnGun(int currentGun)
    {
        GunSpawnerStruct currentGunStruct = gunSpawnData[currentGun];

        int minSpawn = currentGunStruct.GunSpawnInformation.MinSpawnAmount;
        int maxSpawn = currentGunStruct.GunSpawnInformation.MaxSpawnAmount;

        int spawnAmount = Random.Range(minSpawn, maxSpawn);

        for (int i = 0; i < spawnAmount; i++)
        {
            float randomXSpawn = Random.Range(-mapSize.x, mapSize.x);
            float randomYSpawn = Random.Range(-mapSize.y, mapSize.y);
            GameObject itemSpawned = Instantiate(currentGunStruct.GunSpawnInformation.ObjectPrefab, new Vector3(randomXSpawn, randomYSpawn, 0), Quaternion.identity);
            itemSpawned.transform.parent = map.transform;
        }
    }

    [System.Serializable]
    private struct GunSpawnerStruct
    {
        [Header("Spawn Amount")]
        [SerializeField] private SpawnAmount gunSpawnInformation;

        public SpawnAmount GunSpawnInformation { get { return gunSpawnInformation; } }
    }
}
