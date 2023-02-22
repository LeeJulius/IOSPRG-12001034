using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : Singleton<SceneManagement>
{
    public GameObject MainGamePanel;
    public GameObject PlayerSelectPanel;
    public GameObject GameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        ActivatePanel(PlayerSelectPanel);
    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        ActivatePanel(PlayerSelectPanel);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ActivatePanel(GameObject panelToBeActivated)
    {
        PlayerSelectPanel.SetActive(panelToBeActivated.Equals(PlayerSelectPanel));

        MainGamePanel.SetActive(panelToBeActivated.Equals(MainGamePanel));

        GameOverPanel.SetActive(panelToBeActivated.Equals(GameOverPanel));
    }
}
