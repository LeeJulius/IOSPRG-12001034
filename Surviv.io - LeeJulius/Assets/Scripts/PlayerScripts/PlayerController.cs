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
    
    float rotation;

    [Header("RigidBody")]
    private Rigidbody2D rb;

    [Header("Player Default Weapon")]
    [SerializeField] private GameObject WeaponSpawnLocation;
    [SerializeField] private GameObject PlayerFists;

    [Header("Other Components")]
    private PlayerInventoryPanelManager playerInventoryPanelManager;
    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventoryPanelManager = this.GetComponent<PlayerInventoryPanelManager>();
        playerInventory = this.GetComponent<PlayerInventory>();

        // Attaching rb to Player
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Changing Player (Position)
        rb.position += new Vector2(speedX, speedY) * Time.deltaTime;

        // Changing Aim (Rotating)
        WeaponSpawnLocation.transform.Rotate(new Vector3(0, 0, 1), rotation);
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

    #region Gun Functions
    public void UseWeapon()
    {
        // Getting weapon to currently equip
        int currentInventorySlot = playerInventoryPanelManager.GetCurrentInventorySlot();
        
        // Equip Weapon
        GameObject currentGun = playerInventory.GetCurrentItem(currentInventorySlot);
        int currentClip = currentGun.GetComponent<GunComponent>().GetCurrentClip();

        if (currentClip <= 0)
            return;

        currentGun.GetComponent<GunComponent>().Shoot(WeaponSpawnLocation.transform);
        currentGun.GetComponent<GunComponent>().SetCurrentClip(currentClip - 1);

        // Update Text
        playerInventoryPanelManager.UpdatePanelInformation();
        playerInventory.ChangeWeapon(currentInventorySlot);
    }

    public void OnReloadButtonClick()
    {
        StartCoroutine(ReloadGun());
    }

    public IEnumerator ReloadGun()
    {
        // Getting weapon to currently equip
        int currentInventorySlot = playerInventoryPanelManager.GetCurrentInventorySlot();
        GameObject currentGun = playerInventory.GetCurrentItem(currentInventorySlot);

        WeaponTypes weapon = currentGun.GetComponent<GunComponent>().GetWeaponTypes();
        int currentClip = currentGun.GetComponent<GunComponent>().GetCurrentClip();
        int maxClip = currentGun.GetComponent<GunComponent>().GetMaxClip();

        int currentAmmo = playerInventory.GetCurrentAmmo(weapon);

        // Cancel Reload if Fully Reloaded
        if (currentClip >= maxClip)
        {
            yield break;
        }
            
        int ammoNeeded = maxClip - currentClip;

        if (ammoNeeded > currentAmmo)
        {
            ammoNeeded = currentAmmo;
        }

        // Equip Weapon
        yield return StartCoroutine(currentGun.GetComponent<GunComponent>().Reload(ammoNeeded));
        playerInventory.SetCurrentAmmo(weapon, playerInventory.GetCurrentAmmo(weapon) - ammoNeeded);

        // Update Text
        playerInventoryPanelManager.UpdatePanelInformation();
        playerInventory.ChangeWeapon(currentInventorySlot);
    }
    #endregion

    public GameObject GetPlayerFists()
    {
        return PlayerFists;
    }
}
