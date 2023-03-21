using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int speed;
    private Rigidbody2D rb;
    private int dmg;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void Init(int _dmg)
    {
        dmg = _dmg;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.StartsWith("Hitbox"))
        {
            collision.gameObject.GetComponentInParent<HealthComponent>().TakeDamage(dmg);
            Destroy(this.gameObject);
        }

        if (collision.name.StartsWith("WorldBounds") || collision.name.StartsWith("Rock"))
        {
            Destroy(this.gameObject);
        }

    }
}
