using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        StartCoroutine(AsyncLoadScene("Menu", OnMenuLoaded));
    }

    IEnumerator AsyncLoadScene(string name, Action OnCallBack = null)
    {
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        while (!asyncLoadScene.isDone)
            yield return null;

        if (OnCallBack != null)
        {
            OnCallBack.Invoke();
        }
    }

    void AsyncDeloadScene(string name)
    {
        SceneManager.UnloadSceneAsync(name);
    }

    private void OnMenuLoaded()
    {
        MenuManager.instance.ShowCanvas(MenuTypes.MAIN_SCREEN);
    }

    public void StartGame()
    {
        MenuManager.instance.HideAll();
        StartCoroutine(AsyncLoadScene("MainGame", OnMapLoaded));
    }

    private void OnMapLoaded()
    {
        SpawnMap();
        MainGameManager.instance.StartGame();
    }

    public void EndGame(GameResult _gameResult)
    {
        AsyncDeloadScene("MainGame");
        OnGameEnd(_gameResult);
    } 

    private void OnGameEnd(GameResult _gameResult)
    {
        if (_gameResult == GameResult.PLAYER_WIN)
        {
            MenuManager.instance.ShowCanvas(MenuTypes.WIN_GAME);
        }
        else if (_gameResult == GameResult.ENEMY_WIN)
        {
            MenuManager.instance.ShowCanvas(MenuTypes.GAME_OVER);
        }
    }

    private void SpawnMap()
    {
        SceneManager.MoveGameObjectToScene(MainGameManager.instance.LoadWorld(), SceneManager.GetSceneByName("MainGame"));
    }
}

public enum GameResult
{
    PLAYER_WIN,
    ENEMY_WIN
}



