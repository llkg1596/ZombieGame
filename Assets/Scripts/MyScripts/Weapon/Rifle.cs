using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public override void Init_Weapon()
    {
        bullet = Resources.Load("Prefabs/Bullet_Pistol") as GameObject;

        attackSpd = 10f;
        bulletSpd = 20f;
        damage = 1f;
        max_bullet = 30f;
        cur_bullet = max_bullet;
    }

    public override void Shoot(Vector3 vec)
    {
        if (attackCool > 0f || cur_bullet <= 0f)
            return;

        base.Shoot(vec);

        GameObject temp = Instantiate(bullet, PlayManager.Instance.GetData().transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().Init_Bullet(vec, damage, bulletSpd);
    }
}
