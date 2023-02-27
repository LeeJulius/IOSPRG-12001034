using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed;

    void Update()
    {
        // Move Background Down
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        // Delete Background if out of bounds
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
            GameManager.instance.UpdateBackground();
        }
    }
}
