using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectiles
{

    protected override void Start()
    {
        base.Start();
    }

    public override void Init(int _dmg)
    {
        base.Init(_dmg);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.name.StartsWith("Hitbox"))
        {
            collision.gameObject.GetComponentInParent<HealthComponent>().TakeDamage(dmg);
            Destroy(this.gameObject);
        }

        if (collision.name.StartsWith("WorldBounds") || collision.name.StartsWith("Rock"))
        {
            Destroy(this.gameObject);
        }

        if (collision.name.StartsWith("Crate"))
        {
            collision.gameObject.GetComponentInParent<Crate>().TakeDamage(dmg);
            Destroy(this.gameObject);
        }
    }
}
