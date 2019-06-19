using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    float spd;

    Vector3 vec;

    public void Init_Bullet(Vector3 _vec, float _damage, float _spd)
    {
        vec = _vec.normalized;
        damage = _damage;
        //spd = _spd;

        //StartCoroutine(Move());
        transform.rotation = Quaternion.LookRotation(vec, Vector3.up);
    }
    /*
    IEnumerator Move()
    {
        float timer = 0f;

        while(true)
        {
            if(timer >= 5f)
            {
                break;
            }

            transform.Translate(vec * spd * MyTime.deltaTime);

            timer += MyTime.deltaTime;
            yield return null;
        }

        DestroyCall();
    }
    */
    public void DestroyCall()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Enemy")
        {
            c.GetComponent<Monster>().GetDamage(damage);
            DestroyCall();
        }
    }
}
