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

            // Get Initial Touch Location
            if (touch.phase == TouchPhase.Began)
            {
                initialPosition = touch.position;
                StartCoroutine(ActivateDash(2));
            }

            // Get Final Touch Location and Calculate for Distance
            if (touch.phase == TouchPhase.Ended)
            {
                finalPosition = touch.position;
                CheckSwipeDirection();
            }
        }
    }

    private void CheckSwipeDirection()
    {
        SwipeDirections SwipeDirection;

        // Return if no enemies spawns
        if (EnemySpawner.instance.SpawnedEnemies.Count <= 0)
            return;

        // Calculate what direction player swaped
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

        // Send player swipe location to enemy
        for (int i = 0; i < EnemySpawner.instance.SpawnedEnemies.Count; i++)
        {
            EnemySpawner.instance.SpawnedEnemies[i].GetComponent<Enemy>().AttackEnemy(SwipeDirection);
        }
    }

    #region DashPowerups
    public IEnumerator ActivateDash(int dashTime)
    {
        if (EnemySpawner.instance.SpawnedEnemies.Count <= 0)
            yield return null;

        foreach (GameObject CurrentEnemy in EnemySpawner.instance.SpawnedEnemies)
        {
            if (!CurrentEnemy.GetComponent<Enemy>().isDashed)
            {
                CurrentEnemy.GetComponent<Enemy>().moveSpeed *= 2;
                CurrentEnemy.GetComponent<Enemy>().isDashed = true;
            }
        }

        yield return new WaitForSecondsRealtime(dashTime);

        foreach (GameObject CurrentEnemy in EnemySpawner.instance.SpawnedEnemies)
        {
            if (CurrentEnemy.GetComponent<Enemy>().isDashed)
            {
                CurrentEnemy.GetComponent<Enemy>().moveSpeed /= 2;
                CurrentEnemy.GetComponent<Enemy>().isDashed = false;
            }
        }
    }
    #endregion

    #region Getters and Setters
    public int GetLives() { return lives; }
    public void SetLives(int _lives) { 
        
        lives = _lives;

        if (lives <= 0)
        {
            GameManager.instance.ShowGameOverScreen();
            EnemySpawner.instance.StopCoroutine(EnemySpawner.instance.SpawnEnemyCoroutine);
            EnemySpawner.instance.DespawnSpawnedEnemies();
            Destroy(this.gameObject);
        }
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
    public void SetDashDuration(int _dashDuration) { dashDuration = _dashDuration; }

    public int GetScore() { return score; }
    public void SetScore(int _score) { score = _score; }

    #endregion
}
