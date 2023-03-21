using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuTypes
{
    MAIN_SCREEN,
    MAIN_GAME,
    WIN_GAME,
    GAME_OVER
}

public class BaseCanvas : MonoBehaviour
{
    [SerializeField] private MenuTypes menuType;

    protected virtual void Start()
    {
        MenuManager.instance.RegisterMenu(this);
    }

    public void ShowCanvas()
    {
        if (!this.gameObject.activeSelf)
            gameObject.SetActive(true);
    }

    public void HideCanvas()
    {
        if (this.gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    public virtual void OnStartGame()
    {
        GameManager.instance.StartGame();
    }

    public virtual void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else 
            Application.Quit();
        #endif
    }

    public MenuTypes MenuType { get { return menuType; } }
}
