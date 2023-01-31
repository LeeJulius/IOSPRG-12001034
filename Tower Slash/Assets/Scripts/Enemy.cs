using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum ArrowTypes
    {
        GREEN,
        RED
    }

    public enum CorrentSwipe
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    private CorrentSwipe CorrectSwipe;

    public ArrowTypes ArrowType;

    [SerializeField]
    private List<GameObject> ArrowDirections;

    public GameObject Player;

    [SerializeField]
    private float moveSpeed;

    private bool inPlayerRange;

    // Start is called before the first frame update
    void Start()
    {
        inPlayerRange = false;

        // Setting Arrow Position
        int ArrowPosition = Random.Range(0, ArrowDirections.Count);
        ArrowDirections[ArrowPosition].SetActive(true);

        // Setting Enemy Type
        int EnemyType = Random.Range(1, 3);

        if (EnemyType == 1)
        {
            ArrowType = ArrowTypes.GREEN;
            ArrowDirections[ArrowPosition].GetComponent<SpriteRenderer>().color = Color.green;

            switch (ArrowPosition)
            {
                case 0:
                    CorrectSwipe = CorrentSwipe.UP;
                    break;

                case 1:
                    CorrectSwipe = CorrentSwipe.LEFT;
                    break;

                case 2:
                    CorrectSwipe = CorrentSwipe.DOWN;
                    break;

                case 3:
                    CorrectSwipe = CorrentSwipe.RIGHT;
                    break;

                default:
                    Debug.LogWarning("No Correct Swipe.");
                    break;
            }
        }
        else
        {
            ArrowType = ArrowTypes.RED;
            ArrowDirections[ArrowPosition].GetComponent<SpriteRenderer>().color = Color.red;

            switch (ArrowPosition)
            {
                case 0:
                    CorrectSwipe = CorrentSwipe.DOWN;
                    break;

                case 1:
                    CorrectSwipe = CorrentSwipe.RIGHT;
                    break;

                case 2:
                    CorrectSwipe = CorrentSwipe.LEFT;
                    break;

                case 3:
                    CorrectSwipe = CorrentSwipe.UP;
                    break;

                default:
                    Debug.LogWarning("No Correct Swipe.");
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        if (transform.position.y < -10.0f)
        {
            DestroySelf();
        }
    }

    public void AttackEnemy(Player.SwipeDirections PlayerSwipeDirection)
    {
        if (!inPlayerRange)
            return;

        if ((PlayerSwipeDirection == global::Player.SwipeDirections.UP && CorrectSwipe == CorrentSwipe.UP) ||
            (PlayerSwipeDirection == global::Player.SwipeDirections.DOWN && CorrectSwipe == CorrentSwipe.DOWN) ||
            (PlayerSwipeDirection == global::Player.SwipeDirections.LEFT && CorrectSwipe == CorrentSwipe.LEFT) ||
            (PlayerSwipeDirection == global::Player.SwipeDirections.RIGHT && CorrectSwipe == CorrentSwipe.RIGHT))
        {
            Debug.Log("Correct Swipe");
            DestroySelf();
        }
        else
        {
            Debug.Log("Player Missed");
        }

    }

    private void DestroySelf()
    {
        GameManager.instance.SpawnedEnemies.Remove(this.gameObject);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.name.StartsWith("Player"))
        {
            this.GetComponent<SpriteRenderer>().color = Color.black;
            inPlayerRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.StartsWith("Player"))
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
            Debug.Log("Player Got Hit");
            inPlayerRange = false;
        }
    }
}
