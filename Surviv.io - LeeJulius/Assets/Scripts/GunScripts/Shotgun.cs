using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunComponent
{
    [SerializeField] private int shotgunBullets;

    [SerializeField] private int minSpreadShot;
    [SerializeField] private int maxSpreadShot;

    public override IEnumerator Shoot(Transform bulletRotation)
    {
        if (currentClip <= 0)
            yield return null;

        for (int i = 0; i < shotgunBullets; i++)
        {
            bulletRotation.transform.eulerAngles += new Vector3(0, 0, SpreadBullets(minSpreadShot, maxSpreadShot));
            Instantiate(BulletPrefab, bulletRotation.transform.position, bulletRotation.transform.rotation);
        }

        currentClip -= 1;

        yield return null;
    }
}
