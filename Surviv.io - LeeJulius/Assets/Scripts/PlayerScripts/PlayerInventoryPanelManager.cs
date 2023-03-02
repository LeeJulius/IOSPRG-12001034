using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Rename to player inventory manager

// Note to professional ape lord

/*
======================================================================================
1. When Shooting/Reloading/Picking Up Ammo take note of the following:

Ammo Texts (Top Right) = Pistol, SMG, Shotgun, RPG [This should be unreloaded ammo] (mostly for reloading imo)

Inventory Panel (Bottom Middle)
 - How much ammo gun has (in the magazine)
 - Total Ammo [Unreloaded ammo]
 - Selecting correct Gun Panel
======================================================================================
2. When Switching Guns (wether panel or switch pick up)/ Picking up Guns take note of the following:

Changing Prefabs of Gun

Inventory Panel (Bottom Middle)
 - How much ammo gun has (in the magazine)
 - Total Ammo [Unreloaded ammo]
 - Selecting correct Gun Panel
*/

public class PlayerInventoryPanelManager : MonoBehaviour
{
    [Header("Inventory Size")]
    private int currentInventorySlot;
    [SerializeField] private int inventorySize;
    private List<GameObject> InventoryPanelList;

    [Header("Inventory Panel")]
    [SerializeField] private GameObject InventorySlotPanel;
    [SerializeField] private GameObject InventorySlotLocation;

    [Header("Ammo Text")]
    [SerializeField] private TextMeshProUGUI PistolAmmoText;
    [SerializeField] private TextMeshProUGUI SMGAmmoText;
    [SerializeField] private TextMeshProUGUI ShotgunAmmoText;
    [SerializeField] private TextMeshProUGUI RPGAmmoText;

    // Ask sir cuz this is so hard coded lmao
    [Header("Inventory Type")]
    [SerializeField] private InventorySlotTypes[] inventroySlotType = new InventorySlotTypes[2]
        {
            InventorySlotTypes.PRIMARY,
            InventorySlotTypes.SECONDARY
        };

    [Header("Other Components")]
    private PlayerInventory playerInventory;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = this.GetComponent<PlayerInventory>();
        playerController = this.GetComponent<PlayerController>();

        InventoryPanelList = new List<GameObject>();

        // Checking if inventory is positive number
        if (inventorySize <= 0)
            Debug.LogError("Cannot have negative inverntory size!");

        // Setting to First Inventory Slot
        currentInventorySlot = 0;

        // Creating the different inventory slot
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject CurrentInverntoryPanel;

            // Creating inverntort slots
            CurrentInverntoryPanel = Instantiate(InventorySlotPanel, InventorySlotLocation.transform);
            InventoryPanelList.Add(CurrentInverntoryPanel);

            // Equipping the Fists as the weapon (by default)
            CurrentInverntoryPanel.GetComponent<InventoryPanel>().Init(inventroySlotType[i], i + 1, WeaponTypes.FIST, 0, 0);
            playerInventory.EquipWeapon(playerController.GetPlayerFists(), i);
        }

        // Activate first inventory slot
        UpdatePanelInformation();
    }

    #region UI Functions
    public void NextGun()
    {
        currentInventorySlot++;

        if (currentInventorySlot >= InventoryPanelList.Count)
        {
            currentInventorySlot = 0;
        }

        UpdatePanelInformation();
        playerInventory.ChangeWeapon(currentInventorySlot);
    }

    public void PrevGun()
    {
        currentInventorySlot--;

        if (currentInventorySlot < 0)
        {
            currentInventorySlot = InventoryPanelList.Count - 1;
        }

        UpdatePanelInformation();
        playerInventory.ChangeWeapon(currentInventorySlot);
    }
    #endregion

    public void UpdatePanelInformation()
    {
        // Update Ammo Text
        InventoryPanel currentPanel = InventoryPanelList[currentInventorySlot].GetComponent<InventoryPanel>();     
        GunComponent currentGun = playerInventory.GetCurrentItem(currentInventorySlot).GetComponent<GunComponent>();

        WeaponTypes currentWeaponType = currentGun.GetWeaponTypes();

        int currentClipAmmo = currentGun.GetCurrentClip();
        int gunAmmo = playerInventory.GetCurrentAmmo(currentWeaponType);
        

        currentPanel.UpdatePanel(currentWeaponType, currentClipAmmo, gunAmmo);

        foreach (GameObject InventoryPanel in InventoryPanelList)
        {
            InventoryPanel.SetActive(false);
        }

        // Activate Panel
        InventoryPanelList[currentInventorySlot].SetActive(true);
        UpdateAmmoText();
    }

    // Ammo Counter
    public void UpdateAmmoText()
    {
        PistolAmmoText.text = playerInventory.GetCurrentAmmo(WeaponTypes.PISTOL).ToString();
        SMGAmmoText.text = playerInventory.GetCurrentAmmo(WeaponTypes.SMG).ToString();
        ShotgunAmmoText.text = playerInventory.GetCurrentAmmo(WeaponTypes.SHOTGUN).ToString();
        RPGAmmoText.text = playerInventory.GetCurrentAmmo(WeaponTypes.RPG).ToString();
    }

    public int GetCurrentInventorySlot()
    {
        return currentInventorySlot;
    }

    public GameObject GetCurrentPanel()
    {
        return InventoryPanelList[currentInventorySlot];
    }
}
