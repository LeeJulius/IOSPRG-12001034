using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum WeaponTypes
{
    FIST,
    PISTOL,
    SMG,
    SHOTGUN,
    RPG
}

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Size")]
    private List<GameObject> InventoryList;
    private int currentInventorySlot;
    [SerializeField] private int inventorySize;

    [Header("Inventory Panel")]
    [SerializeField] private GameObject InventorySlotPanel;
    [SerializeField] private GameObject InventorySlotLocation;

    [Header("Ammo")]
    private int pistolAmmo;
    private int smgAmmo;
    private int shotgunAmmo;
    private int rpgAmmo;

    [Header("Max Ammo")]
    [SerializeField] private int pistolMaxAmmo;
    [SerializeField] private int smgMaxAmmo;
    [SerializeField] private int shotgunMaxAmmo;
    [SerializeField] private int rpgMaxAmmo;

    [Header("Ammo Text")]
    [SerializeField] private TextMeshProUGUI PistolAmmoText;
    [SerializeField] private TextMeshProUGUI SMGAmmoText;
    [SerializeField] private TextMeshProUGUI ShotgunAmmoText;
    [SerializeField] private TextMeshProUGUI RPGAmmoText;

    private void Start()
    {
        if (inventorySize < 0)
            Debug.LogError("Cannot have negative inverntory size!");

        InventoryList = new List<GameObject>();

        currentInventorySlot = 0;

        for (int i = 0; i < inventorySize; i++)
        {
            GameObject CurrentInverntoryPanel;
            CurrentInverntoryPanel = Instantiate(InventorySlotPanel, InventorySlotLocation.transform);
            InventoryList.Add(CurrentInverntoryPanel);
            CurrentInverntoryPanel.GetComponent<InventoryPanel>().Init(i + 1, WeaponTypes.FIST, 0, GetTotalAmmo(WeaponTypes.FIST));
        }

        // Note to test the inventory... this is not part of the final build
        InventoryList[0].GetComponent<InventoryPanel>().SetGunPrefab(WeaponTypes.PISTOL);
        InventoryList[1].GetComponent<InventoryPanel>().SetGunPrefab(WeaponTypes.SHOTGUN);
        InventoryList[2].GetComponent<InventoryPanel>().SetGunPrefab(WeaponTypes.SMG);
        InventoryList[3].GetComponent<InventoryPanel>().SetGunPrefab(WeaponTypes.RPG);

        ActivateInventoryPanel(currentInventorySlot);

        this.GetComponent<PlayerController>().EquipGun();
    }

    public void NextGun()
    {
        currentInventorySlot++;

        if (currentInventorySlot >= InventoryList.Count)
        {
            currentInventorySlot = 0;
        }

        ActivateInventoryPanel(currentInventorySlot);
    }

    public void PrevGun()
    {
        currentInventorySlot--;

        if(currentInventorySlot < 0)
        {
            currentInventorySlot = InventoryList.Count - 1;
        }

        ActivateInventoryPanel(currentInventorySlot);
    }

    private void ActivateInventoryPanel(int panelToActivate)
    {
        foreach (GameObject InventoryPanel in InventoryList)
        {
            InventoryPanel.SetActive(false);
        }

        InventoryList[panelToActivate].SetActive(true);

        InventoryPanel currentInventoryProperties = InventoryList[panelToActivate].GetComponent<InventoryPanel>();
        currentInventoryProperties.SetMaxAmmoText(GetTotalAmmo(currentInventoryProperties.GetWeaponType()));
    }

    public void PickUpGun()
    {
        
    }

    public void PickUpAmmo(WeaponTypes weaponType, int ammoGained)
    {
        switch(weaponType)
        {
            case WeaponTypes.PISTOL:
                pistolAmmo += ammoGained;

                if (pistolAmmo > pistolMaxAmmo)
                {
                    pistolAmmo = pistolMaxAmmo;
                }
                break;

            case WeaponTypes.SMG:
                smgAmmo += ammoGained;

                if (smgAmmo > smgMaxAmmo)
                {
                    smgAmmo = smgMaxAmmo;
                }
                break;

            case WeaponTypes.SHOTGUN:
                shotgunAmmo += ammoGained;

                if (shotgunAmmo > smgMaxAmmo)
                {
                    shotgunAmmo = smgMaxAmmo;
                }

                break;
        }

        InventoryPanel currentInventoryProperties = GetCurrentInventoryPrefab().GetComponent<InventoryPanel>();
        currentInventoryProperties.SetMaxAmmoText(GetTotalAmmo(currentInventoryProperties.GetWeaponType()));
        SetAmmoText(pistolAmmo, smgAmmo, shotgunAmmo, rpgAmmo);
    }

    public void SetAmmoText(int pistolAmmo, int smgAmmo, int shotgunAmmo, int rpgAmmo)
    {
        PistolAmmoText.text = pistolAmmo.ToString();
        SMGAmmoText.text = smgAmmo.ToString();
        ShotgunAmmoText.text = shotgunAmmo.ToString();
        RPGAmmoText.text = rpgAmmo.ToString();
    }

    public int GetTotalAmmo(WeaponTypes weapon)
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

    public GameObject GetCurrentInventoryPrefab()
    {
        return InventoryList[currentInventorySlot];
    }
}
