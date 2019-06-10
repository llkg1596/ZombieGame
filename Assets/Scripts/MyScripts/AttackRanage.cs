using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRanage : MonoBehaviour
{
    public Monster parentMonster;
    public MonsterControl control;


    void Start()
    {
        parentMonster = GetComponentInParent<Monster>();
        control = parentMonster.GetComponent<MonsterControl>();

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player" || c.tag == "Object")
        {
            control.state = MobState.ATTACK;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player" || c.tag == "Object")
        {
            control.state = MobState.MOVE;
        }
    }
}
