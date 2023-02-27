using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [Header("Camera")]
    Camera Cam;
    Vector2 CamBounds;

    [Header("Enemy")]
    [SerializeField] private GameObject EnemyPrefab;
    public int enemySpawnTimeMin;
    public int enemySpawnTimeMax;
    [HideInInspector] public List<GameObject> SpawnedEnemies;
    public IEnumerator SpawnEnemyCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        // Set Cam to Bottom Left
        Cam = Camera.main;
        CamBounds = Cam.ScreenToWorldPoint(Vector2.zero);
        SpawnEnemyCoroutine = SpawnEnemy();
    }

    private IEnumerator SpawnEnemy()
    {
        float currentSpawnSpeed;

        while (true)
        {
            // Get Random Time
            currentSpawnSpeed = Random.Range(enemySpawnTimeMin, enemySpawnTimeMax);
            yield return new WaitForSecondsRealtime(currentSpawnSpeed);

            // Spawn Enemy
            GameObject SpawnedEnemy = Instantiate(EnemyPrefab, CamBounds * new Vector3(0.3f, -1.2f, 0.0f), Quaternion.identity);
            SpawnedEnemies.Add(SpawnedEnemy);
        }
    }

    public void DespawnSpawnedEnemies()
    {
        foreach (GameObject Enemy in SpawnedEnemies)
        {
            Destroy(Enemy);
        }

        SpawnedEnemies.Clear();
    }
}
