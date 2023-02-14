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
        CurrentPlayer = PlayerSelectionManager.instance.CurrentPlayer;

        // Spawn Background
        UpdateBackground();

        // Update Text
        UpdateLifeText();
        UpdateDashText();
        UpdateScoreText();

        // Setting UI False
        DashButton.SetActive(false);
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

        yield return new WaitForSecondsRealtime(1);
        dashDuration--;
        CurrentPlayer.isImmune = true;

        if (dashDuration <= 0)
        {
            CurrentPlayer.DeactivateDash();
            CurrentPlayer.isImmune = false;
        }
    }
}
