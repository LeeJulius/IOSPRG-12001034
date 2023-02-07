using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum SwipeDirections
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class GameManager : Singleton<GameManager>
{
    [Header("Camera")]
    Camera Cam;
    Vector2 CamBounds;

    [Header("Background")]
    [SerializeField]
    private GameObject BackgroundPrefab;
    public int BackgroundSpeed;

    [Header("Enemy")]
    [SerializeField]
    private GameObject EnemyPrefab;
    public int enemySpawnTimeMin;
    public int enemySpawnTimeMax;
    [HideInInspector]
    public List<GameObject> SpawnedEnemies;

    [Header("Player")]
    [SerializeField]
    private GameObject PlayerPrefab;
    [HideInInspector]
    public GameObject CurrentPlayer;

    [Header("UI")]
    [SerializeField]
    private Text LivesText;
    [SerializeField]
    private Text DashText;
    public GameObject RetryButton;
    public GameObject DashButton;

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
        StartCoroutine(SpawnEnemy());

        // Update Text
        UpdateLifeText();
        UpdateDashText();

        // Setting UI False
        RetryButton.SetActive(false);
        DashButton.SetActive(false);
    }

    public void UpdateBackground()
    {
        Instantiate(BackgroundPrefab, Vector3.up, Quaternion.identity).SetActive(true);
    }

    private void SpawnPlayer()
    {
         CurrentPlayer = Instantiate(PlayerPrefab, CamBounds * new Vector3(-0.5f, 0, 0), Quaternion.identity);
    }

    public void UpdateLifeText()
    {
        LivesText.text = "Lives: " + CurrentPlayer.GetComponent<Player>().GetLives();
    }

    public void UpdateDashText()
    {
        DashText.text = "Dash: " + CurrentPlayer.GetComponent<Player>().GetDash();
    }

    public void ShowGameOverScreen()
    {
        Time.timeScale = 0;
        RetryButton.SetActive(true);
    }

    public void ShowDashButton()
    {
        DashButton.SetActive(true);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator SpawnEnemy()
    {
        int currentSpawnSpeed = Random.Range(enemySpawnTimeMin, enemySpawnTimeMax);

        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            currentSpawnSpeed--;

            if (currentSpawnSpeed <= 0)
            {
                GameObject SpawnedEnemy = Instantiate(EnemyPrefab, CamBounds * new Vector3(0.3f, -1.2f, 0.0f), Quaternion.identity);
                SpawnedEnemies.Add(SpawnedEnemy);
                SpawnedEnemy.SetActive(true);

                currentSpawnSpeed = Random.Range(enemySpawnTimeMin, enemySpawnTimeMax);
            }
        }
    }

    public void ActivateDash()
    {
        DashButton.SetActive(false);
        int dashDuration = CurrentPlayer.GetComponent<Player>().dashDuration;
        StartCoroutine(DashActivated(dashDuration));
    }

    public IEnumerator DashActivated(int dashDuration)
    {
        foreach (GameObject CurrentEnemy in SpawnedEnemies)
        {
            CurrentEnemy.GetComponent<Enemy>().moveSpeed *= 2;
        }

        bool dashActivated = true;

        while (dashActivated)
        {
            yield return new WaitForSecondsRealtime(1);
            dashDuration--;
            CurrentPlayer.GetComponent<Player>().isImmune = true;

            if (dashDuration <= 0)
            {
                foreach (GameObject CurrentEnemy in SpawnedEnemies)
                {
                    CurrentEnemy.GetComponent<Enemy>().moveSpeed /= 2;
                    CurrentPlayer.GetComponent<Player>().isImmune = false;
                    dashActivated = false;
                }
            }
        } 
    }
}
