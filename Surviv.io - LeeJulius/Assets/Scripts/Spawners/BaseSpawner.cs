using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    protected GameObject map;
    protected Vector2 mapSize;

    protected virtual void Start()
    {
        SpawnerManager.instance.RegisterMenu(this);
    }

    public virtual void Spawn()
    {
        map = MainGameManager.instance.Map;
        mapSize = map.GetComponent<SpriteRenderer>().bounds.size;
        mapSize /= 2;
    }

    [System.Serializable]
    protected struct SpawnAmount
    {
        [SerializeField] private GameObject objectPrefab;
        [SerializeField] private int minSpawnAmount;
        [SerializeField] private int maxSpawnAmount;

        public GameObject ObjectPrefab { get { return objectPrefab; } }
        public int MinSpawnAmount { get { return minSpawnAmount; } }
        public int MaxSpawnAmount { get { return maxSpawnAmount; } }
    }
}
