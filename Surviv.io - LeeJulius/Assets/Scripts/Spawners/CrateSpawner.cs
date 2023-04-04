using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawner : BaseSpawner
{
    [SerializeField] private CrateSpawnerStruct[] crateSpawnData;

    protected override void Start()
    {
        base.Start();
    }

    public override void Spawn()
    {
        base.Spawn();

        for (int i = 0; i < crateSpawnData.Length; i++)
        {
            SpawnObstacle(i);
        }
    }

    public void SpawnObstacle(int currentObstacle)
    {
        CrateSpawnerStruct currentCrateStruct = crateSpawnData[currentObstacle];

        int minSpawn = currentCrateStruct.CrateSpawnInformation.MinSpawnAmount;
        int maxSpawn = currentCrateStruct.CrateSpawnInformation.MaxSpawnAmount;

        int spawnAmount = Random.Range(minSpawn, maxSpawn);

        for (int i = 0; i < spawnAmount; i++)
        {
            float randomXSpawn = Random.Range(-mapSize.x, mapSize.x);
            float randomYSpawn = Random.Range(-mapSize.y, mapSize.y);
            GameObject itemSpawned = Instantiate(currentCrateStruct.CrateSpawnInformation.ObjectPrefab, new Vector3(randomXSpawn, randomYSpawn, 0), Quaternion.identity);
            itemSpawned.transform.parent = map.transform;

            int randomChance = Random.Range(0, 100);
            GameObject lootToSpawn;

            if (randomChance >= currentCrateStruct.ChanceToSpawnGun)
            {
                lootToSpawn = currentCrateStruct.Guns[Random.Range(0, currentCrateStruct.Guns.Length - 1)];
            }
            else 
            {
                lootToSpawn = currentCrateStruct.Ammo[Random.Range(0, currentCrateStruct.Ammo.Length - 1)];
            }

            itemSpawned.GetComponent<Crate>().Init(currentCrateStruct.Hp, lootToSpawn);
        }
    }

    [System.Serializable]
    private struct CrateSpawnerStruct
    {
        [Header("Spawn Amount")]
        [SerializeField] private SpawnAmount crateSpawnInformation;
        [SerializeField] private int hp;
        [SerializeField, Range(0, 100)] private int chanceToSpawnGun;
        [SerializeField] private GameObject[] guns;
        [SerializeField] private GameObject[] ammo;

        public SpawnAmount CrateSpawnInformation { get { return crateSpawnInformation; } }
        public int Hp { get { return hp; } }
        public int ChanceToSpawnGun { get { return chanceToSpawnGun; } }
        public GameObject[] Guns { get { return guns; } }
        public GameObject[] Ammo { get { return ammo; } }
    }
}
