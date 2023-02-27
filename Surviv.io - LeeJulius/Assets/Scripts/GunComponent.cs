using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject BulletPrefab;

    [Header("Gun Ammo")]
    [SerializeField] int currentAmmo;
    [SerializeField] int maxClip;
    [SerializeField] int maxAmmo;

    [Header("Gun Properties")]
    [SerializeField] bool isBurst;
    [SerializeField] bool hasSpread;
    [SerializeField] int dmg;

    [Header("Relod Clip")]
    [SerializeField] float reloadSpeed;

    public virtual void Shoot()
    {

    }

    public virtual void Reload()
    {

    }
}
