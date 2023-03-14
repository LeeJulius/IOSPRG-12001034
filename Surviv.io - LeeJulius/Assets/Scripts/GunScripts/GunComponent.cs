using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] protected GameObject BulletPrefab;

    [Header("Gun Ammo")]
    protected int currentClip;
    [SerializeField] private int maxClip;

    [Header("Gun Properties")]
    [SerializeField] private WeaponTypes weaponType;
    [SerializeField] private int dmg;

    [Header("Reloading Property")]
    [SerializeField] private float reloadSpeed;
    private bool isReloading;

    [Header("Autonmatic Property")]
    [SerializeField] private bool isAutomatic;
    [SerializeField] protected float fireRate;

    public virtual IEnumerator Shoot(Transform bulletRotation) { yield return null; }

    protected float SpreadBullets(int minSpead, int maxSpread) {
        return Random.Range(minSpead, maxSpread);
    }

    public IEnumerator Reload(int ammoGained)
    {
        isReloading = true;

        yield return new WaitForSecondsRealtime(reloadSpeed);

        currentClip += ammoGained;

        isReloading = false;
    }

    public int MaxClip { get { return maxClip; } }
    public int CurrentClip { get { return currentClip; } }
    public bool IsReloading { get { return isReloading; } }
    public WeaponTypes WeaponType { get { return weaponType; } }
    public bool IsAutomatic { get { return isAutomatic; } }
    public float FireRate { get { return fireRate; } }
}
