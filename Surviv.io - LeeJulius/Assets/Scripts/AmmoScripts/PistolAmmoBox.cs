using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmoBox : AmmoBox
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name.StartsWith("Player"))
        {
            Debug.Log("Player Hit Pistol Ammo");

            AmmoBoxPickedUp(other);
        }
    }
}
