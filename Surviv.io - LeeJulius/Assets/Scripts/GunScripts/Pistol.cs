using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : GunComponent
{
    public override IEnumerator Shoot(Transform bulletRotation)
    {
        if (currentClip <= 0)
            yield return null;

        Instantiate(BulletPrefab, bulletRotation.transform.position, bulletRotation.transform.rotation);

        currentClip -= 1;

        yield return null;
    }
}
