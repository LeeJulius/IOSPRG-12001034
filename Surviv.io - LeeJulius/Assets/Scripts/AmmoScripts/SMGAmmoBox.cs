using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGAmmoBox : AmmoBox
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name.StartsWith("Player"))
        {
            Debug.Log("Player Hit SMG Ammo");

            AmmoBoxPickedUp(other);
        }
    }
}
