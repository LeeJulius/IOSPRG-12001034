using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    private List<BaseSpawner> spawnerList = new List<BaseSpawner>();

    public void RegisterMenu(BaseSpawner currentSpawner)
    {
        spawnerList.Add(currentSpawner);
    }

    public void SpawnAll()
    {
        foreach (BaseSpawner currentSpawner in spawnerList)
        {
            currentSpawner.Spawn();
        }
    }
}
