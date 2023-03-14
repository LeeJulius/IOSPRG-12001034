using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ammo
{
    [SerializeField] private int pistolAmmo;
    [SerializeField] private int smgAmmo;
    [SerializeField] private int shotgunAmmo;
    [SerializeField] private int rpgAmmo;

    // Getters
    public int GetAmmo(WeaponTypes weapon)
    {
        switch (weapon)
        {
            case WeaponTypes.FIST:
                return 0;

            case WeaponTypes.PISTOL:
                return pistolAmmo;

            case WeaponTypes.SMG:
                return smgAmmo;

            case WeaponTypes.SHOTGUN:
                return shotgunAmmo;

            case WeaponTypes.RPG:
                return rpgAmmo;

            default:
                Debug.LogError("No Available Weapon");
                return 0;
        }
    }

    public void SetAmmo(WeaponTypes weapon, int ammo)
    {
        switch (weapon)
        {
            case WeaponTypes.PISTOL:
                pistolAmmo = ammo;
                break;

            case WeaponTypes.SMG:
                smgAmmo = ammo;
                break;

            case WeaponTypes.SHOTGUN:
                shotgunAmmo = ammo;
                break;

            case WeaponTypes.RPG:
                rpgAmmo = ammo;
                break;

            default:
                Debug.LogError("No Available Weapon");
                break;
        }
    }
}