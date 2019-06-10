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
        if (attackCool > 0f || cur_bullet <= 0f)
            return;

        SetCool();

        cur_bullet -= 1f;

        GameObject temp = Instantiate(bullet, PlayManager.Instance.GetData().transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        temp.GetComponent<Bullet>().Init_Bullet(vec, damage, bulletSpd);
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
