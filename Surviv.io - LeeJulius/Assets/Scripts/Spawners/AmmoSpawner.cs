using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : BaseSpawner
{
    [SerializeField] private AmmoSpawnerStruct[] ammoSpawnData;

    protected override void Start()
    {
        base.Start();
    }

    public override void Spawn()
    {
        base.Spawn();

        for (int i = 0; i < ammoSpawnData.Length; i++)
        {
            SpawnAmmo(i);
        }
    }

    private void SpawnAmmo(int currentAmmo)
    {
        AmmoSpawnerStruct currentGunStruct = ammoSpawnData[currentAmmo];

        int minSpawn = currentGunStruct.AmmoSpawnInformation.MinSpawnAmount;
        int maxSpawn = currentGunStruct.AmmoSpawnInformation.MaxSpawnAmount;

        int spawnAmount = Random.Range(minSpawn, maxSpawn);

        for (int i = 0; i < spawnAmount; i++)
        {
            float randomXSpawn = Random.Range(-mapSize.x, mapSize.x);
            float randomYSpawn = Random.Range(-mapSize.y, mapSize.y);
            GameObject itemSpawned = Instantiate(currentGunStruct.AmmoSpawnInformation.ObjectPrefab, new Vector3(randomXSpawn, randomYSpawn, 0), Quaternion.identity);
            itemSpawned.transform.parent = map.transform;
        }
    }

    [System.Serializable]
    private struct AmmoSpawnerStruct
    {
        [Header("Spawn Amount")]
        [SerializeField] private SpawnAmount ammoSpawnInformation;

        public SpawnAmount AmmoSpawnInformation { get { return ammoSpawnInformation; } }
    }
}
