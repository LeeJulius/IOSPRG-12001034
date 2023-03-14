using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // Ask sir cuz this is so hard coded lmao (answer can be struct)
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
            playerInventory.EquipWeapon(playerController.PlayerFists, i);
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

        WeaponTypes currentWeaponType = currentGun.WeaponType;

        int currentClipAmmo = currentGun.CurrentClip;
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
