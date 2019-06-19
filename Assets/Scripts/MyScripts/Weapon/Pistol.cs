using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Init_Weapon()
    {
        bullet = PlayManager.Instance.bullet;
        attackSpd = 1f;
        bulletSpd = 10f;
        damage = 1f;
        max_bullet = 10f;
        cur_bullet = max_bullet;
        sprite = PlayManager.Instance.LoadGunSprite(Weapons.PISTOL);
    }

    public override void Shoot(Vector3 vec)
    {
        base.Shoot(vec);
    }
}
