using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : CharacterData
{
    private Weapon main_Weapon;
    public Weapon Main_Weapon
    {
        get { return main_Weapon; }
        set
        {
            main_Weapon = value;
            //그 외 UI 적인 부분
        }
    }
    private Weapon sub_Weapon;
    public Weapon Sub_Weapon
    {
        get { return sub_Weapon; }
        set
        {
            sub_Weapon = value;
            //그 외 UI 적인 부분
        }
    }

    void Start()
    {
        //기본 무기
        Main_Weapon = new GameObject("Pistol").AddComponent<Pistol>();
        Main_Weapon.transform.parent = gameObject.transform;
        Main_Weapon.Init_Weapon();
    }

    public void ChangeWeapon()
    {

        Collider[] c = Physics.OverlapBox(transform.position, new Vector3(1f, 1f, 1f) * 0.5f);
        //box와 겹친 경우 -> sub / drop 간 교환

        bool alreadyGetted = false;

        foreach (var col in c)
        {
            if (col.gameObject.tag == "Box" && alreadyGetted == false)
            {
                Sub_Weapon = col.gameObject.GetComponent<DropBox>().GetWeapon(transform);
                alreadyGetted = true;
                Destroy(col.gameObject);
            }
        }

        //아닌 경우 -> main / sub 간 교환
        if (alreadyGetted == false)
        {
            if (sub_Weapon == null)
                return;

            Weapon temp = Main_Weapon;
            Main_Weapon = Sub_Weapon;
            Sub_Weapon = temp;
        }

        Debug.Log($"Main : {Main_Weapon.name} / Sub : {Sub_Weapon.name}");
    }
}
