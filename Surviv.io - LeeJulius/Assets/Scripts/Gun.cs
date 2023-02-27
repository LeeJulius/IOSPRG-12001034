using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Name")]
    [SerializeField] private string gunName;

    [Header("Gun Ammo")]
    [SerializeField] private int currentGunMagazine;
    [SerializeField] private int gunMagazine;
    [SerializeField] private int gunMaxAmmo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Getters and Setters
    public int GetCurrentAmmo() { return currentGunMagazine; } 
    public void SetCurrentAmmo(int _currentGunMagazine) { currentGunMagazine = _currentGunMagazine; }

    #endregion
}
