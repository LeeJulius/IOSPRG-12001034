using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private GameObject AmmoBoxPrefab;
    [SerializeField] protected int ammo;
    [SerializeField] protected WeaponTypes weapon;

    protected virtual void AmmoBoxPickedUp(Collision2D other)
    {
        PlayerInventory playerInventory = other.gameObject.GetComponent<PlayerInventory>();
        playerInventory.PickUpAmmo(weapon, ammo);

        Destroy(gameObject);
    }
}
