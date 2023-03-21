using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField] protected float speed;

    [Header("Weapon Location")]
    [SerializeField] protected GameObject weaponLocation;

    // Events
    public Action OnDeath;

    protected virtual void Start()
    {
        OnDeath += Death;
    }

    public GameObject WeaponLocation { get { return weaponLocation; } }
    public float Speed { get { return speed; } }

    protected virtual void Death() { }
}
