using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rename to player item manager
public class PlayerInventory : MonoBehaviour
{
    [Header("Ammo")]
    [SerializeField] private Ammo currentAmmo;
    [SerializeField] private Ammo maxAmmo;

    [Header("Player Guns")]
    private List<GameObject> Weapons = new List<GameObject>();

    [Header("Other Components")]
    private PlayerInventoryPanelManager playerInventoryPanelManager;

    [Header("Spawn Location")]
    [SerializeField] private GameObject WeaponSpawnLocation;

    private void Start()
    {
        playerInventoryPanelManager = this.GetComponent<PlayerInventoryPanelManager>();
    }

    public void EquipWeapon(GameObject gunToEquip, int inventorySlot)
    {
        GameObject CurrentGun = Instantiate(gunToEquip, WeaponSpawnLocation.transform);
        CurrentGun.transform.parent = WeaponSpawnLocation.transform;

        if (Weapons.Count <= inventorySlot)
        {
            Weapons.Add(CurrentGun);
        }
        else
        {
            Destroy(Weapons[inventorySlot]);
            Weapons.RemoveAt(inventorySlot);
            Weapons.Insert(inventorySlot, CurrentGun);
        }

        ChangeWeapon(inventorySlot);
    }

    public void PickUpGun(WeaponTypes weapon, GameObject weaponPrefab)
    {
        playerInventoryPanelManager.UpdatePanelInformation();
        EquipWeapon(weaponPrefab, playerInventoryPanelManager.GetCurrentInventorySlot());
    }

    public void PickUpAmmo(WeaponTypes weaponType, int ammoGained)
    {
        // Check if Ammo will be filled
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
        playerInventoryPanelManager.UpdatePanelInformation();
    }

    public bool IsPickUpValid(WeaponTypes weapon)
    {
        InventorySlotTypes inventorySlotTypes = playerInventoryPanelManager.GetCurrentPanel().GetComponent<InventoryPanel>().GetInventorySlotType();

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

    #region Inventory Functions
    public void ChangeWeapon(int inventorySlot)
    {
        // Unequiping all weapons
        foreach (GameObject equipabbleWeapons in Weapons)
        {
            equipabbleWeapons.SetActive(false);
        }

        // Equip Weapon
        Weapons[inventorySlot].SetActive(true);
    }
    #endregion

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

    public int GetCurrentAmmo(WeaponTypes weapon)
    {
        return currentAmmo.GetAmmo(weapon);
    }

    public void SetCurrentAmmo(WeaponTypes weapon, int remainingAmmo)
    {
        currentAmmo.SetAmmo(weapon, remainingAmmo);
    }

    public GameObject GetCurrentItem(int currentIndex)
    {
        return Weapons[currentIndex];
    }
}
