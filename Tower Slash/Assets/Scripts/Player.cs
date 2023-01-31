using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 finalPosition;

    public enum SwipeDirections
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    // Start is called before the first frame update
    void Start()
    {

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

        for (int i = 0; i < GameManager.instance.SpawnedEnemies.Count; i++)
        {
            GameManager.instance.SpawnedEnemies[i].GetComponent<Enemy>().AttackEnemy(SwipeDirection);
        }
    }
}
