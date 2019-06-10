using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public override void Init_Weapon()
    {
        bullet = PlayManager.Instance.bullet;

        attackSpd = 10f;
        bulletSpd = 20f;
        damage = 1f;
        max_bullet = 30f;
        cur_bullet = max_bullet;
    }

    public override void Shoot(Vector3 vec)
    {
        base.Shoot(vec);

    }
}
