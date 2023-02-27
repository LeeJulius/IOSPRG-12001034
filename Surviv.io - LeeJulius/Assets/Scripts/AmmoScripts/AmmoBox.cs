using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] protected int ammo;
    [SerializeField] protected WeaponTypes weapon;

    private void OnCollisionEnter2D(Collision2D other)
    {
        // If Ammo Box collided with Player
        if (other.collider.name.StartsWith("Player"))
        {
            // Pickup Ammo Box
            PlayerInventory playerInventory = other.gameObject.GetComponent<PlayerInventory>();
            playerInventory.PickUpAmmo(weapon, ammo);

            // Despawn Ammo Box
            Destroy(gameObject);
        }
    }
}
