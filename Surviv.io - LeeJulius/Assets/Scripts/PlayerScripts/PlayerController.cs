using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitController
{
    [Header("Player Speed")]
    [SerializeField] private Joystick movementJoystick;

    [Header("Player Rotation")]
    [SerializeField] private Joystick rotationJoystick;

    [Header("Player Weapons")]
    [SerializeField] private GameObject playerFists;

    [Header("PlayerInventory")]
    private PlayerInventoryPanelManager playerInventoryPanelManager;
    private PlayerInventory playerInventory;

    [Header("Player Shoot")]
    private bool shootButtonPressed;

    protected override void Start()
    {
        base.Start();

        playerInventoryPanelManager = this.GetComponent<PlayerInventoryPanelManager>();
        playerInventory = this.GetComponent<PlayerInventory>();

        shootButtonPressed = false;
    }

    private void FixedUpdate()
    {
        // Changing Player (Position)
        if (movementJoystick.GetComponent<Joystick>().JoyStickTouched)
            this.GetComponent<Rigidbody2D>().position += movementJoystick.GetDirection() * speed * Time.deltaTime;

        // Changing Aim (Rotating)
        if (!float.IsNaN(rotationJoystick.GetRotation()) && rotationJoystick.GetComponent<Joystick>().JoyStickTouched)
        {
            weaponLocation.transform.eulerAngles = new Vector3(0, 0, rotationJoystick.GetRotation());
            CurrentGunProperties().BulletSpawnLocation.transform.eulerAngles = new Vector3(0, 0, rotationJoystick.GetRotation());
        }   
    }

    #region Shooting Functions
    public void OnShootButtonPressed()
    {
        shootButtonPressed = true;

        if (!CurrentGunProperties().IsReloading && CurrentGunProperties().CurrentClip > 0)
            StartCoroutine(PlayerShootGun());
    }

    public void OnShootButtonReleased()
    {
        shootButtonPressed = false;
    }

    private IEnumerator PlayerShootGun()
    {
        yield return StartCoroutine(CurrentGunProperties().Shoot(CurrentGunProperties().BulletSpawnLocation));
        playerInventoryPanelManager.UpdatePanelInformation();

        yield return new WaitForSecondsRealtime(CurrentGunProperties().FireRate);

        if (shootButtonPressed && CurrentGunProperties().IsAutomatic && CurrentGunProperties().CurrentClip > 0)
            yield return StartCoroutine(PlayerShootGun());
    }
    #endregion

    #region Reloading Functions
    public void OnReloadButtonClick()
    {
        if (!CurrentGunProperties().IsReloading && CurrentGunProperties().CurrentClip <= CurrentGunProperties().MaxClip)
            StartCoroutine(ReloadGun());
    }

    public IEnumerator ReloadGun()
    {
        int currentAmmo = playerInventory.GetCurrentAmmo(CurrentGunProperties().WeaponType);
        int ammoNeeded = CurrentGunProperties().MaxClip - CurrentGunProperties().CurrentClip;

        if (ammoNeeded > currentAmmo)
            ammoNeeded = currentAmmo;

        // Equip Weapon
        yield return StartCoroutine(CurrentGunProperties().Reload(ammoNeeded));
        playerInventory.SetCurrentAmmo(CurrentGunProperties().WeaponType, playerInventory.GetCurrentAmmo(CurrentGunProperties().WeaponType) - ammoNeeded);

        // Update Text
        playerInventoryPanelManager.UpdatePanelInformation();
        playerInventory.ChangeWeapon(playerInventoryPanelManager.GetCurrentInventorySlot());
    }
    #endregion

    private GunComponent CurrentGunProperties()
    { 
        int currentInventorySlot = playerInventoryPanelManager.GetCurrentInventorySlot();
        GameObject currentGun = playerInventory.GetCurrentItem(currentInventorySlot);
        return currentGun.GetComponent<GunComponent>();
    }

    protected override void Death()
    {
        GameManager.instance.EndGame(GameResult.ENEMY_WIN);
        Destroy(this.gameObject);
    }

    public GameObject PlayerFists { get { return playerFists; } }
}
