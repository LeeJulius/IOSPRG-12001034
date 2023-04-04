using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    private int hp;
    private GameObject lootToSpawn;

    public void Init(int _hp, GameObject _lootToSpawn)
    {
        hp = _hp;
        lootToSpawn = _lootToSpawn;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            hp = 0;
            Destroy();
        } 
    }

    private void Destroy()
    {
        GameObject itemSpawned = Instantiate(lootToSpawn, transform.position, transform.rotation);
        itemSpawned.transform.parent = MainGameManager.instance.Map.transform;
        Destroy(gameObject);
    }
}
