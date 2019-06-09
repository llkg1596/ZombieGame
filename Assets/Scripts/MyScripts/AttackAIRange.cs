using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIRange : MonoBehaviour
{
    public Monster parentMonster;
    public MonsterControl control;
    public GameObject ob;
    public PlayerData pl;

    MonsterMoveAI moveToOb;
    MonsterMoveAI moveToPl;

    void Start()
    {
        ob = PlayManager.Instance.GetTheObject();
        pl = PlayManager.Instance.GetData();
        parentMonster = GetComponentInParent<Monster>();
        control = parentMonster.GetComponent<MonsterControl>();
        moveToOb = new MonsterMoveAI(MoveToObject);
        moveToPl = new MonsterMoveAI(MoveToPlayer);
        control.monsterMoveAI += moveToOb;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            control.monsterMoveAI += moveToPl;
            control.monsterMoveAI -= moveToOb;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            control.monsterMoveAI += moveToOb;
            control.monsterMoveAI -= moveToPl;
        }
    }
    void MoveToPlayer()
    {
        Vector3 vec = pl.transform.position - transform.position;
        vec.Normalize();

        control.enemyAnimator.SetBool("HasTarget", true);

        control.Move(vec);
    }

    void MoveToObject()
    {
        Vector3 vec = ob.transform.position - transform.position;
        vec.Normalize();

        control.enemyAnimator.SetBool("HasTarget", true);

        control.Move(vec);
    }
}

