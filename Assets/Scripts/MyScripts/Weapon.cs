using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public GameObject bullet;

    public float max_bullet;
    public float cur_bullet;
    public float damage;
    public float attackSpd;     //초당 발사 개수
    public float bulletSpd;

    public float attackCool = 0f;

    public abstract void Init_Weapon();

    public virtual void Shoot(Vector3 vec)
    {
        SetCool();

        cur_bullet -= 1f;
    }
    
    public void SetCool()
    {
        attackCool = 1f/attackSpd;
    }

    public virtual void Update()
    {
        if (attackCool > 0f)
            attackCool -= Time.deltaTime;
    }

}
