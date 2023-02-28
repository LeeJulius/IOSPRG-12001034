using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryPanel : MonoBehaviour
{
    [Header("Panel Type")]
    [SerializeField] private InventorySlotTypes inventorySlotType;

    [Header("Panel Number Text")]
    [SerializeField] private TextMeshProUGUI PanelNumberText;

    [Header("Ammo Panel Text")]
    [SerializeField] private TextMeshProUGUI CurrentAmmoText;
    [SerializeField] private TextMeshProUGUI TotalAmmoText;

    [Header("Gun")]
    [SerializeField] private List<GameObject> GunPrefab;
    private WeaponTypes weaponEquipped;

    public void Init(InventorySlotTypes _inventorySlotType, int panelNumber, WeaponTypes weaponType, int currentAmmo, int maxAmmo)
    {
        inventorySlotType = _inventorySlotType;
        SetPanelNumberText(panelNumber);
        SetGunPrefab(weaponType);
        SetCurrentAmmoText(currentAmmo);
        SetMaxAmmoText(maxAmmo);
    }

    private void SetPanelNumberText(int panelNumber)
    {
        switch (inventorySlotType)
        {
            case InventorySlotTypes.PRIMARY:
                PanelNumberText.text = panelNumber.ToString() + ": Primary";
                break;

            case InventorySlotTypes.SECONDARY:
                PanelNumberText.text = panelNumber.ToString() + ": Secondary";
                break;

            default:
                Debug.LogWarning("No Avaiable Inventory Slot Type");
                break;
        }

        
    }

    private void SetCurrentAmmoText(int currentAmmo)
    {
        CurrentAmmoText.text = currentAmmo.ToString();
    }

    public void SetMaxAmmoText(int totalAmmo)
    {
        TotalAmmoText.text = totalAmmo.ToString();
    }

    public void SetGunPrefab(WeaponTypes weapon)
    {
        weaponEquipped = weapon;

        foreach (GameObject CurrentGunPrefab in GunPrefab)
        {
            CurrentGunPrefab.SetActive(false);
        }

        // Applying weapons to the UI 
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

    // sus
    public WeaponTypes GetWeaponType()
    {
        return weaponEquipped;
    }

    public InventorySlotTypes GetInventorySlotType()
    {
        return inventorySlotType;
    }
}
