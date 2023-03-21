using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : GunComponent
{
    [SerializeField] private int minSpreadShot;
    [SerializeField] private int maxSpreadShot;

    public override IEnumerator Shoot(Transform bulletSpawnLocation)
    {
        if (currentClip <= 0)
            yield break;

        bulletSpawnLocation.transform.eulerAngles += new Vector3(0, 0, SpreadBullets(minSpreadShot, maxSpreadShot));
        GameObject Bullet = Instantiate(BulletPrefab, bulletSpawnLocation.transform.position, bulletSpawnLocation.transform.rotation);
        Bullet.transform.parent = MainGameManager.instance.BulletsLocation.transform;

        Bullet.GetComponent<Bullet>().Init(dmg);

        currentClip -= 1;

        yield return null;
    }
}
