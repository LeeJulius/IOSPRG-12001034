using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Size")]
    private List<GameObject> InventoryList;
    private int currentInventorySlot;
    [SerializeField] private int inventorySize;

    [Header("Inventory Panel")]
    [SerializeField] private GameObject InventorySlotPanel;
    [SerializeField] private GameObject InventorySlotLocation;

    [Header("Player Default Weapon")]
    [SerializeField] private GameObject PlayerFists;

    [Header("Ammo")]
    [SerializeField] private Ammo currentAmmo;
    [SerializeField] private Ammo maxAmmo;

    [SerializeField] private InventorySlotTypes[] inventroySlotType;

    [Header("Ammo Text")]
    [SerializeField] private TextMeshProUGUI PistolAmmoText;
    [SerializeField] private TextMeshProUGUI SMGAmmoText;
    [SerializeField] private TextMeshProUGUI ShotgunAmmoText;
    [SerializeField] private TextMeshProUGUI RPGAmmoText;

    private void Start()
    {
        // Checking if inventory is positive number
        if (inventorySize < 0)
            Debug.LogError("Cannot have negative inverntory size!");

        // Creating List of Inventory
        InventoryList = new List<GameObject>();

        // This is so scuffed HELP (consult sir)
        inventroySlotType = new InventorySlotTypes[2]
        {
            InventorySlotTypes.PRIMARY,
            InventorySlotTypes.SECONDARY
        };

        // Setting to First Inventory Slot
        currentInventorySlot = 0;

        // Creating the different inventory slot
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject CurrentInverntoryPanel;

            // Creating inverntort slots
            CurrentInverntoryPanel = Instantiate(InventorySlotPanel, InventorySlotLocation.transform);
            InventoryList.Add(CurrentInverntoryPanel);

            // Equipping the Fists as the weapon (by default)
            CurrentInverntoryPanel.GetComponent<InventoryPanel>().Init(inventroySlotType[i], i + 1, WeaponTypes.FIST, 0, currentAmmo.GetAmmo(WeaponTypes.FIST));
            this.GetComponent<PlayerController>().EquipWeapon(PlayerFists, i);
        }

        // Activate first inventory slot
        ActivateInventoryPanel(currentInventorySlot);
    }

    #region UI Functions
    public void NextGun()
    {
        currentInventorySlot++;

        if (currentInventorySlot >= InventoryList.Count)
        {
            currentInventorySlot = 0;
        }

        ActivateInventoryPanel(currentInventorySlot);
        this.GetComponent<PlayerController>().SwitchWeapon();
    }

    public void PrevGun()
    {
        currentInventorySlot--;

        if(currentInventorySlot < 0)
        {
            currentInventorySlot = InventoryList.Count - 1;
        }

        ActivateInventoryPanel(currentInventorySlot);
        this.GetComponent<PlayerController>().SwitchWeapon();
    }

    private void ActivateInventoryPanel(int panelToActivate)
    {
        foreach (GameObject InventoryPanel in InventoryList)
        {
            InventoryPanel.SetActive(false);
        }

        // Activate Panel
        InventoryList[panelToActivate].SetActive(true);

        // Update Ammo Text
        InventoryPanel currentInventoryProperties = InventoryList[panelToActivate].GetComponent<InventoryPanel>();
        currentInventoryProperties.SetMaxAmmoText(currentAmmo.GetAmmo(currentInventoryProperties.GetWeaponType()));
    }
    #endregion

    public void PickUpGun(WeaponTypes weapon, GameObject weaponPrefab)
    {
        InventoryList[currentInventorySlot].GetComponent<InventoryPanel>().SetGunPrefab(weapon);
        this.GetComponent<PlayerController>().EquipWeapon(weaponPrefab, currentInventorySlot);
        ActivateInventoryPanel(currentInventorySlot);
    }

    public void PickUpAmmo(WeaponTypes weaponType, int ammoGained)
    {
        // Add Ammo
        if (currentAmmo.GetAmmo(weaponType) + ammoGained > maxAmmo.GetAmmo(weaponType))
        {
            // Normalize Ammo (to make it not go over the total ammo)
            currentAmmo.SetAmmo(weaponType, maxAmmo.GetAmmo(weaponType));
        }
        else
        {
            // Add Ammo
            currentAmmo.SetAmmo(weaponType, currentAmmo.GetAmmo(weaponType) + ammoGained);
        }

        // Update Text
        ActivateInventoryPanel(currentInventorySlot);
        UpdateAmmoText();
    }

    public bool IsPickUpValid(WeaponTypes weapon)
    {
        InventorySlotTypes inventorySlotTypes = InventoryList[currentInventorySlot].GetComponent<InventoryPanel>().GetInventorySlotType();

        switch (weapon) {
            case WeaponTypes.FIST:
                return true;

            case WeaponTypes.PISTOL:
                if (inventorySlotTypes == InventorySlotTypes.SECONDARY) return true;
                else return false;

            case WeaponTypes.SMG:
                if (inventorySlotTypes == InventorySlotTypes.PRIMARY) return true;
                else return false;

            case WeaponTypes.SHOTGUN:
                if (inventorySlotTypes == InventorySlotTypes.PRIMARY) return true;
                else return false;

            case WeaponTypes.RPG:
                if (inventorySlotTypes == InventorySlotTypes.PRIMARY) return true;
                else return false;

            default:
                Debug.LogWarning("Not an existing Gun");
                return false;
        }
    }

    public void UpdateAmmoText()
    {
        PistolAmmoText.text = currentAmmo.GetAmmo(WeaponTypes.PISTOL).ToString();
        SMGAmmoText.text = currentAmmo.GetAmmo(WeaponTypes.SMG).ToString();
        ShotgunAmmoText.text = currentAmmo.GetAmmo(WeaponTypes.SHOTGUN).ToString();
        RPGAmmoText.text = currentAmmo.GetAmmo(WeaponTypes.RPG).ToString();
    }

    public int GetCurrentInventorySlot()
    {
        return currentInventorySlot;
    }

    [System.Serializable]
    protected class Ammo
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

        // Setters
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
}
