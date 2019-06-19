using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    public Weapon weapon;
    Sprite[] gunSprite;

    public void Init_DropBox()
    {
        //드랍 박스 안에 무슨 무기 들었는지 초기화
        if (CheckRandom(0f, 1f))
        {
            Debug.Log("Rifle");

            weapon = new GameObject("Rifle").AddComponent<Rifle>();
            weapon.transform.parent = gameObject.transform.parent;
            weapon.Init_Weapon();
        }
    }

    bool CheckRandom(float min, float max)
    {
        return (Random.Range(0f, 1f) >= min) && (Random.Range(0f, 1f) < max);
    }

    public Weapon GetWeapon(Transform parent)
    {
        //PlayerData에서 호출
        weapon.transform.parent = parent;

        return weapon;
    }
}
