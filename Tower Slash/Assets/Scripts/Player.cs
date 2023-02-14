using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 finalPosition;

    [Header("Lives")]
    private int lives;

    [Header("Score")]
    private int score;

    [Header("Dash")]
    [SerializeField] private int dashIncrement;
    [SerializeField] private int dash;
    [SerializeField] private int dashDuration;

    [HideInInspector] public bool isImmune;

    // Start is called before the first frame update
    void Start()
    {
        isImmune = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                initialPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                ActivateDash();
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                DeactivateDash();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                finalPosition = touch.position;
                CheckSwipeDirection();
                DeactivateDash();
            }
        }
    }

    private void CheckSwipeDirection()
    {
        SwipeDirections SwipeDirection;

        if (EnemySpawner.instance.SpawnedEnemies.Count <= 0)
            return;

        if (Mathf.Max(finalPosition.x - initialPosition.x, initialPosition.x - finalPosition.x) > Mathf.Max(finalPosition.y - initialPosition.y, initialPosition.y - finalPosition.y))
        {
            if (initialPosition.x > finalPosition.x)
            {
                SwipeDirection = SwipeDirections.LEFT;
            }
            else
            {
                SwipeDirection = SwipeDirections.RIGHT;
            }
        }
        else
        {
            if (initialPosition.y > finalPosition.y)
            {
                SwipeDirection = SwipeDirections.DOWN;
            }
            else
            {
                SwipeDirection = SwipeDirections.UP;
            }
        }

        for (int i = 0; i < EnemySpawner.instance.SpawnedEnemies.Count; i++)
        {
            EnemySpawner.instance.SpawnedEnemies[i].GetComponent<Enemy>().AttackEnemy(SwipeDirection);
        }
    }

    public void ActivateDash()
    {
        if (EnemySpawner.instance.SpawnedEnemies.Count <= 0)
            return;

        foreach (GameObject CurrentEnemy in EnemySpawner.instance.SpawnedEnemies)
        {
            if (!CurrentEnemy.GetComponent<Enemy>().isDashed)
            {
                CurrentEnemy.GetComponent<Enemy>().moveSpeed *= 2;
                CurrentEnemy.GetComponent<Enemy>().isDashed = true;
            }
        }
    }

    public void DeactivateDash()
    {
        if (EnemySpawner.instance.SpawnedEnemies.Count <= 0)
            return;

        foreach (GameObject CurrentEnemy in EnemySpawner.instance.SpawnedEnemies)
        {
            if (CurrentEnemy.GetComponent<Enemy>().isDashed)
            {
                CurrentEnemy.GetComponent<Enemy>().moveSpeed /= 2;
                CurrentEnemy.GetComponent<Enemy>().isDashed = false;
            }
        }
    }

    #region Getters and Setters
    public int GetLives() { return lives; }
    public void SetLives(int _lives) { 
        
        lives = _lives;
        
        if (lives <= 0)
            GameManager.instance.ShowGameOverScreen();
    }

    public int GetDash() { return dash; }
    public void SetDash(int _dash) { 
        
        dash = _dash;

        if (dash >= 100)
        {
            GameManager.instance.ShowDashButton();
            dash = 100;
        }
    }

    public int GetDashIncrement() { return dashIncrement; }
    public void SetDashIncrement(int _dashIncrement) { dashIncrement = _dashIncrement; }

    public int GetDashDuration() { return dashDuration; }
    public void SetDashDuration(int _dashDuration) { dashDuration = _dashDuration; 
    }

    public int GetScore() { return score; }
    public void SetScore(int _score) { score = _score; }
    #endregion
}
