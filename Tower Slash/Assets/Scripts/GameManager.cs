using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SwipeDirections
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class GameManager : Singleton<GameManager>
{ 
    [Header("Background")]
    [SerializeField] private GameObject BackgroundPrefab;

    [Header("UI")]
    [SerializeField] private Text LivesText;
    [SerializeField] private Text DashText;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text ScoreGameOverText;
    public GameObject DashButton;

    private Player CurrentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        DashButton.SetActive(false);
    }

    public void StartGame()
    {
        CurrentPlayer = PlayerSelectionManager.instance.CurrentPlayer;
        
        // Spawn Enemy
        EnemySpawner.instance.StartCoroutine("SpawnEnemy");

        // Spawn Background
        UpdateBackground();

        // Update Text
        UpdateLifeText();
        UpdateDashText();
        UpdateScoreText();
    }

    public void UpdateBackground()
    {
        Instantiate(BackgroundPrefab, Vector3.up, Quaternion.identity).SetActive(true);
    }

    public void UpdateLifeText()
    {
        LivesText.text = "Lives: " + CurrentPlayer.GetLives();
    }

    public void UpdateDashText()
    {
        DashText.text = "Dash: " + CurrentPlayer.GetDash();
    }

    public void UpdateDashDurationText(int dashDuration)
    {
        DashText.text = "Dash Duration: " + dashDuration;
    }

    public void UpdateScoreText()
    {
        ScoreText.text = "Score: " + CurrentPlayer.GetScore();
        ScoreGameOverText.text = "Score: " + CurrentPlayer.GetScore();
    }

    public void ShowGameOverScreen()
    {
        Time.timeScale = 0;
        SceneManagement.instance.ActivatePanel(SceneManagement.instance.GameOverPanel);
    }

    public void ShowDashButton()
    {
        DashButton.SetActive(true);
    }

    public void ActivateDash()
    {
        DashButton.SetActive(false);
        StartCoroutine(DashActivated(CurrentPlayer.GetDashDuration()));
    }

    public IEnumerator DashActivated(int dashDuration)
    {
        CurrentPlayer.ActivateDash();
        UpdateDashText();
        CurrentPlayer.isImmune = true;

        yield return new WaitForSecondsRealtime(dashDuration);

        // Remove Immunity
        CurrentPlayer.DeactivateDash();
        CurrentPlayer.isImmune = false;
    }
}
