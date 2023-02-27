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
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.position += new Vector2(speedX, speedY) * Time.deltaTime;

        PlayerHands.transform.Rotate(new Vector3(0, 0, 1), rotation);
    }

    #region Movement Functions
    public void HorizontalMovement(int direction)
    {
        speedX = direction * horizontalSpeed;
        Debug.Log("Horizontal Speed: " + speedX);
    }

    public void VerticalMovement(int direction)
    {
        speedY = direction * verticalSpeed;
        Debug.Log("Vertical Speed: " + speedY);
    }
    #endregion

    #region Aim Functions
    public void RotateGun(int direction)
    {
        rotation = direction * rotationalSpeed;
        Debug.Log("Rotation Speed: " + rotation);
    }
    #endregion

    #region Equip Gun
    public void EquipGun()
    {
        foreach(GameObject equipabbleWeapons in EquipableItems)
        {
            equipabbleWeapons.SetActive(false);
        }

        PlayerInventory playerInventory = this.GetComponent<PlayerInventory>();

        WeaponTypes weaponEquipped = playerInventory.GetCurrentInventoryPrefab().GetComponent<InventoryPanel>().GetWeaponType();

        switch(weaponEquipped)
        {
            case WeaponTypes.FIST:
                Debug.Log("Fist Equipped");
                EquipableItems[0].SetActive(true);
                break;

            case WeaponTypes.PISTOL:
                Debug.Log("Pistol Equipped");
                EquipableItems[1].SetActive(true);
                break;

            case WeaponTypes.SHOTGUN:
                Debug.Log("Shotgun Equipped");
                EquipableItems[2].SetActive(true);
                break;

            case WeaponTypes.SMG:
                Debug.Log("SMG Equipped");
                EquipableItems[3].SetActive(true);
                break;

            case WeaponTypes.RPG:
                Debug.Log("RPG Equipped");
                EquipableItems[4].SetActive(true);
                break;

            default:
                Debug.LogError("No Equipablle Weapon");
                break;
        }
    }


    #endregion
}
