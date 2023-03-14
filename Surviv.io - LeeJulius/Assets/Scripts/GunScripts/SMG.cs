using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : GunComponent
{
    [SerializeField] private int minSpreadShot;
    [SerializeField] private int maxSpreadShot;

    public override IEnumerator Shoot(Transform bulletRotation)
    {
        if (currentClip <= 0)
            yield return null;

        bulletRotation.transform.eulerAngles += new Vector3(0, 0, SpreadBullets(minSpreadShot, maxSpreadShot));
        Instantiate(BulletPrefab, bulletRotation.transform.position, bulletRotation.transform.rotation);

        currentClip -= 1;

        yield return null;
    }
}
