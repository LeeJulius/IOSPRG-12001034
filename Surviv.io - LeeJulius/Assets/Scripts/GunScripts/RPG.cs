using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : GunComponent
{
    public override IEnumerator Shoot(Transform bulletSpawnLocation)
    {
        if (currentClip <= 0)
            yield break;

        GameObject Bullet = Instantiate(BulletPrefab, bulletSpawnLocation.transform.position, bulletSpawnLocation.transform.rotation);
        Bullet.transform.parent = MainGameManager.instance.BulletsLocation.transform;

        Bullet.GetComponentInChildren<Projectiles>().Init(dmg);

        currentClip -= 1;

        yield return null;
    }
}
