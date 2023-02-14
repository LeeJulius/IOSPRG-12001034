using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum ArrowTypes
    {
        GREEN,
        RED,
        YELLOW
    }

    [Header("Arrow Sprites")]
    [SerializeField]
    private GameObject[] ArrowDirections;

    [Header("Enemy Properties")]
    public float moveSpeed;
    public ArrowTypes ArrowType;
    private int ArrowPosition;
    private SwipeDirections CorrectSwipe;

    [Header("Enemy Conditions")]
    private bool inPlayerRange;
    private bool isAlive;
    [HideInInspector] public bool isDashed;

    // Start is called before the first frame update
    void Start()
    {
        inPlayerRange = false;
        isAlive = true;
        isDashed = false;

        // Setting Arrow Position
        ArrowPosition = Random.Range(0, ArrowDirections.Length);
        ArrowDirections[ArrowPosition].SetActive(true);

        // Setting Enemy Type
        int EnemyType = Random.Range(1, 4);

        // Applying Enemy Characteristics
        switch (EnemyType)
        {
            case 1:
                ArrowType = ArrowTypes.GREEN;
                ArrowDirections[ArrowPosition].GetComponent<SpriteRenderer>().color = Color.green;
                break;

            case 2:
                ArrowType = ArrowTypes.RED;
                ArrowDirections[ArrowPosition].GetComponent<SpriteRenderer>().color = Color.red;
                break;

            case 3:
                ArrowType = ArrowTypes.YELLOW;

                for (int i = 0; i < ArrowDirections.Length; i++)
                {
                    ArrowDirections[i].GetComponent<SpriteRenderer>().color = Color.yellow;
                }

                StartCoroutine(RotateArrow(ArrowPosition));
                break;

            default:
                Debug.LogWarning("No Avaiable Arrow Type");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    public void EnemyMovement()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        if (transform.position.y < -10.0f)
        {
            DestroySelf();
        }
    }

    public void AttackEnemy(SwipeDirections PlayerSwipeDirection)
    {
        if (!inPlayerRange)
            return;

        Player CurrentPlayer = PlayerSelectionManager.instance.CurrentPlayer.GetComponent<Player>();

        // Checking if Player Swiped Correct Direction
        if ((PlayerSwipeDirection == SwipeDirections.UP && CorrectSwipe == SwipeDirections.UP) ||
            (PlayerSwipeDirection == SwipeDirections.DOWN && CorrectSwipe == SwipeDirections.DOWN) ||
            (PlayerSwipeDirection == SwipeDirections.LEFT && CorrectSwipe == SwipeDirections.LEFT) ||
            (PlayerSwipeDirection == SwipeDirections.RIGHT && CorrectSwipe == SwipeDirections.RIGHT))
        {
            // Randomizing Powerup Chance
            int randomPowerUpChance = Random.Range(0, 100);

            if (randomPowerUpChance < 3)
            {
                CurrentPlayer.SetLives(CurrentPlayer.GetLives() + 1);
                GameManager.instance.UpdateLifeText();
            }

            // Add Dash
            CurrentPlayer.SetDash(CurrentPlayer.GetDash() + CurrentPlayer.GetDashIncrement());
            GameManager.instance.UpdateDashText();

            // Add Score
            CurrentPlayer.SetScore(CurrentPlayer.GetScore() + 1);
            GameManager.instance.UpdateScoreText();

            // Kill Enemy
            isAlive = false;
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        EnemySpawner.instance.SpawnedEnemies.Remove(this.gameObject);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.StartsWith("Player"))
        {
            this.GetComponent<SpriteRenderer>().color = Color.black;
            SetCorrectDirection(ArrowPosition, ArrowType);

            inPlayerRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.StartsWith("Player"))
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
            inPlayerRange = false;

            Player CurrentPlayer = other.gameObject.GetComponent<Player>();

            if (isAlive && !CurrentPlayer.isImmune)
            {
                CurrentPlayer.SetLives(CurrentPlayer.GetLives() - 1);
                GameManager.instance.UpdateLifeText();
            }
        }
    }

    private IEnumerator RotateArrow(int startingPosition)
    {
        int i = startingPosition;

        while (!inPlayerRange)
        {
            ArrowDirections[i].SetActive(false);

            i++;

            if (i >= ArrowDirections.Length)
            {
                i = 0;
            }
                
            ArrowDirections[i].SetActive(true);

            yield return new WaitForSecondsRealtime(0.4f);
        }
    }

    private void SetCorrectDirection(int ArrowPosition, ArrowTypes ArrowType)
    {
        switch (ArrowPosition)
        {
            case 0:

                if (ArrowType == ArrowTypes.GREEN || ArrowType == ArrowTypes.YELLOW)
                {
                    CorrectSwipe = SwipeDirections.UP;
                }
                else
                {
                    CorrectSwipe = SwipeDirections.DOWN;
                }
                
                break;

            case 1:

                if (ArrowType == ArrowTypes.GREEN || ArrowType == ArrowTypes.YELLOW)
                {
                    CorrectSwipe = SwipeDirections.LEFT;
                }
                else
                {
                    CorrectSwipe = SwipeDirections.RIGHT;
                }

                break;

            case 2:

                if (ArrowType == ArrowTypes.GREEN || ArrowType == ArrowTypes.YELLOW)
                {
                    CorrectSwipe = SwipeDirections.DOWN;
                }
                else
                {
                    CorrectSwipe = SwipeDirections.UP;
                }

                break;

            case 3:

                if (ArrowType == ArrowTypes.GREEN || ArrowType == ArrowTypes.YELLOW)
                {
                    CorrectSwipe = SwipeDirections.RIGHT;
                }
                else
                {
                    CorrectSwipe = SwipeDirections.LEFT;
                }

                break;

            default:
                Debug.LogWarning("No Correct Swipe.");
                break;
        }
    }
}
