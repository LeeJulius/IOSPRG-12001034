using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectiles
{
    [SerializeField] private BlastDamage blastRadius;

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
        if (collision.name.StartsWith("WorldBounds") || collision.name.StartsWith("Rock") || collision.name.StartsWith("Crate") || collision.name.StartsWith("Hitbox"))
        {
            blastRadius.Explode(dmg);
        }
    }
}
