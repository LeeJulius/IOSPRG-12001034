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
    [SerializeField] private List<GameObject> EquipableItems;

    private void Start()
    {
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

    #region Equip Gun
    public void EquipGun()
    {
        // Unequiping all weapons
        foreach(GameObject equipabbleWeapons in EquipableItems)
        {
            equipabbleWeapons.SetActive(false);
        }

        // Getting weapon to currently equip
        PlayerInventory playerInventory = this.GetComponent<PlayerInventory>();
        WeaponTypes weaponEquipped = playerInventory.GetCurrentInventoryPrefab().GetComponent<InventoryPanel>().GetWeaponType();

        // Equip Weapon
        switch(weaponEquipped)
        {
            case WeaponTypes.FIST:
                EquipableItems[0].SetActive(true);
                break;

            case WeaponTypes.PISTOL:
                EquipableItems[1].SetActive(true);
                break;

            case WeaponTypes.SHOTGUN:
                EquipableItems[2].SetActive(true);
                break;

            case WeaponTypes.SMG:
                EquipableItems[3].SetActive(true);
                break;

            case WeaponTypes.RPG:
                EquipableItems[4].SetActive(true);
                break;

            default:
                Debug.LogError("No Equipablle Weapon");
                break;
        }
    }
    #endregion
}
