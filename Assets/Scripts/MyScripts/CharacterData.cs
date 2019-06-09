using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterData : MonoBehaviour
{
    private float hp;
    public float HP
    {
        get { return hp; } set
        {
            hp = value;

            if (hp <= 0f)
            {
                DestroyCall();
            }
        }
    }

    public virtual void GetDamage(float damage)
    {
        HP -= damage;
    }

    public virtual void DestroyCall()
    {
        Destroy(gameObject);
    }
}
