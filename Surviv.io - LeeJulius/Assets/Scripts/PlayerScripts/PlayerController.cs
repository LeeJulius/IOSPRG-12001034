using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Speed")]
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    float speedX;
    float speedY;

    [Header("Player Rotation")]
    [SerializeField] private float rotationalSpeed;
    [SerializeField] private GameObject PlayerHands;
    float rotation;

    [Header("RigidBody")]
    private Rigidbody2D rb;

    [Header("Player Guns")]
    [SerializeField]private List<GameObject> EquipableItems;

    private void Start()
    {
        EquipableItems = new List<GameObject>();

        // Attaching rb to Player
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Changing Player (Position)
        rb.position += new Vector2(speedX, speedY) * Time.deltaTime;

        // Changing Aim (Rotating)
        PlayerHands.transform.Rotate(new Vector3(0, 0, 1), rotation);
    }

    #region Movement Functions
    public void HorizontalMovement(int direction)
    {
        speedX = direction * horizontalSpeed;
    }

    public void VerticalMovement(int direction)
    {
        speedY = direction * verticalSpeed;
    }
    #endregion

    #region Aim Functions
    public void RotateGun(int direction)
    {
        rotation = direction * rotationalSpeed;
    }
    #endregion

    #region Inventory Functions
    public void SwitchWeapon()
    {
        // Unequiping all weapons
        foreach(GameObject equipabbleWeapons in EquipableItems)
        {
            equipabbleWeapons.SetActive(false);
        }

        // Getting weapon to currently equip
        PlayerInventory playerInventory = this.GetComponent<PlayerInventory>();
        int currentInventorySlot = playerInventory.GetCurrentInventorySlot();

        Debug.Log(currentInventorySlot);

        // Equip Weapon
        EquipableItems[currentInventorySlot].SetActive(true);
    }

    public void EquipWeapon(GameObject gunToEquip, int inventorySlot)
    {
        GameObject CurrentGun = Instantiate(gunToEquip, PlayerHands.transform);
        CurrentGun.transform.parent = PlayerHands.transform;

        if (EquipableItems.Count <= inventorySlot)
        {
            
            EquipableItems.Add(CurrentGun);
        }
        else
        {
            Destroy(EquipableItems[inventorySlot]);
            EquipableItems.RemoveAt(inventorySlot);
            EquipableItems.Insert(inventorySlot, CurrentGun);
        }

        SwitchWeapon();
    }

    #endregion

    #region Gun Functions
    public void ShootGun()
    {
        // Getting weapon to currently equip

        PlayerInventory playerInventory = this.GetComponent<PlayerInventory>();
        int currentInventorySlot = playerInventory.GetCurrentInventorySlot();

        // Equip Weapon
        EquipableItems[currentInventorySlot].GetComponent<GunComponent>();
    }
    #endregion
}
