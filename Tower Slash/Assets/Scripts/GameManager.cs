using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    Camera Cam;
    Vector2 CamBounds;

    [SerializeField]
    private GameObject BackgroundLayer;

    [SerializeField]
    private GameObject Enemy;

    [SerializeField]
    private GameObject Player;

    [HideInInspector]
    public List<GameObject> SpawnedEnemies;

    void Awake()
    {
        instance = this;
    }

    void OnDestroy()
    {
        instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set Cam to Bottom Left
        Cam = Camera.main;
        CamBounds = Cam.ScreenToWorldPoint(Vector2.zero);

        // Spawn Background
        UpdateBackground();

        // Spawn Player
        SpawnPlayer();

        // Spawn Enemy
        InvokeRepeating("SpawnEnemy", 1.5f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateBackground()
    {
        Instantiate(BackgroundLayer, Vector3.up, Quaternion.identity).SetActive(true);
    }

    private void SpawnEnemy()
    {
        GameObject SpawnedEnemy = Instantiate(Enemy, CamBounds * new Vector3(0.3f, -1.2f, 0.0f), Quaternion.identity);
        SpawnedEnemies.Add(SpawnedEnemy);
        SpawnedEnemy.SetActive(true);
    }

    private void SpawnPlayer()
    {
        Instantiate(Player, CamBounds * new Vector3(-0.5f, 0, 0), Quaternion.identity).SetActive(true);
    }
}
