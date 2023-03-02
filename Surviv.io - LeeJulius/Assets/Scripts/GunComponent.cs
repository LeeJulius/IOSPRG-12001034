using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject BulletPrefab;

    [Header("Gun Ammo")]
    [SerializeField]private int currentClip;
    [SerializeField] private int maxClip;

    [Header("Gun Properties")]
    [SerializeField] private WeaponTypes weaponType;
    [SerializeField] private bool isBurst;
    [SerializeField] private bool hasSpread;
    [SerializeField] private int dmg;

    [Header("Relod Clip")]
    [SerializeField] private float reloadSpeed;
    private bool isReloading;

    private void Start()
    {

    }

    public virtual void Shoot(Transform bulletRotation)
    {
        Instantiate(BulletPrefab, bulletRotation.transform.position, bulletRotation.transform.rotation);
    }

    public virtual IEnumerator Reload(int ammoGained)
    {
        isReloading = true;

        Debug.Log("Ammo: " + ammoGained);

        yield return new WaitForSecondsRealtime(reloadSpeed);

        Debug.Log("Done Reloading");

        currentClip += ammoGained;

        isReloading = false;

        Debug.Log("Current Ammo: " + currentClip);
    }

    public int GetMaxClip()
    {
        return maxClip;
    }

    public int GetCurrentClip()
    {
        return currentClip;
    }

    public void SetCurrentClip(int _currentClip)
    {
        currentClip = _currentClip;
    }

    public bool GetIsReloading()
    {
        return isReloading;
    }

    public WeaponTypes GetWeaponTypes()
    {
        return weaponType;
    }
}
