using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] protected int speed;
    protected Rigidbody2D rb;
    protected int dmg;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = this.GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        rb.velocity = transform.right * speed;
    }

    public virtual void Init(int _dmg)
    {
        dmg = _dmg;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) { }
}
