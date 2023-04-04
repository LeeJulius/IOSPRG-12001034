using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGunComponent : MonoBehaviour
{
    [SerializeField] private UnitController unitController;
    [SerializeField] private GameObject droppedGun;
    void Start()
    {
        unitController.OnDeath += DropGun;
    }

    private void DropGun()
    {
        GameObject itemSpawned = Instantiate(droppedGun, this.transform.position, Quaternion.identity);
        itemSpawned.transform.parent = MainGameManager.instance.Map.transform;
    }
}
