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

    public void Init(InventorySlotTypes _inventorySlotType, int panelNumber, WeaponTypes weaponType, int currentAmmo, int maxAmmo)
    {
        // Setting Inventory Slot Information
        inventorySlotType = _inventorySlotType;
        SetPanelNumberText(panelNumber);

        // Setting Inventory Gun
        SetGunPrefab(weaponType);

        // Setting Inventory Slot Ammo
        SetCurrentAmmoText(currentAmmo);
        SetMaxAmmoText(maxAmmo);
    }

    public void UpdatePanel(WeaponTypes weaponType, int currentAmmo, int maxAmmo)
    {
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

    public void SetCurrentAmmoText(int currentAmmo)
    {
        CurrentAmmoText.text = currentAmmo.ToString();
    }

    public void SetMaxAmmoText(int totalAmmo)
    {
        TotalAmmoText.text = totalAmmo.ToString();
    }

    public void SetGunPrefab(WeaponTypes weapon)
    {
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

    public InventorySlotTypes GetInventorySlotType()
    {
        return inventorySlotType;
    }
}
