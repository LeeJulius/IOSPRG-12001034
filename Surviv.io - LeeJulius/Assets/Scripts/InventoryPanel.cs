using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryPanel : MonoBehaviour
{
    [Header("Panel Number Text")]
    [SerializeField] private TextMeshProUGUI PanelNumberText;

    [Header("Ammo Panel Text")]
    [SerializeField] private TextMeshProUGUI CurrentAmmoText;
    [SerializeField] private TextMeshProUGUI TotalAmmoText;

    [Header("Gun")]
    [SerializeField] private List<GameObject> GunPrefab;
    public WeaponTypes weaponEquipped;

    public void Init(int panelNumber, WeaponTypes weaponType, int currentAmmo, int maxAmmo)
    {
        SetPanelNumberText(panelNumber);
        SetGunPrefab(weaponType);
        SetCurrentAmmoText(currentAmmo);
        SetMaxAmmoText(maxAmmo);
    }

    private void SetPanelNumberText(int panelNumber)
    {
        Debug.Log("Panel Number: " + panelNumber);
        PanelNumberText.text = panelNumber.ToString();
    }

    private void SetCurrentAmmoText(int currentAmmo)
    {
        Debug.Log("Current Ammo: " + currentAmmo);
        CurrentAmmoText.text = currentAmmo.ToString();
    }

    public void SetMaxAmmoText(int totalAmmo)
    {
        Debug.Log("Max Ammo: " + totalAmmo);
        TotalAmmoText.text = totalAmmo.ToString();
    }

    public void SetGunPrefab(WeaponTypes weapon)
    {
        weaponEquipped = weapon;

        switch (weapon)
        {
            case WeaponTypes.FIST:
                break;

            case WeaponTypes.PISTOL:
                GunPrefab[0].SetActive(true);
                break;

            case WeaponTypes.SMG:
                GunPrefab[1].SetActive(true);
                break;

            case WeaponTypes.SHOTGUN:
                GunPrefab[2].SetActive(true);
                break;

            case WeaponTypes.RPG:
                GunPrefab[3].SetActive(true);
                break;

            default:
                Debug.LogError("No Selected Weapon");
                break;
        }
    }

    public WeaponTypes GetWeaponType()
    {
        return weaponEquipped;
    }
}
