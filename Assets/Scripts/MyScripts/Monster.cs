using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : CharacterData
{
    public MonsterControl monsterControl;

    public virtual void Start()
    {
        monsterControl = GetComponent<MonsterControl>();
    }

    public MonsterControl GetControl()
    {
        return monsterControl;
    }

    public override void DestroyCall()
    {
        PlayManager.Instance.InstantiateDropBox(transform.position);

        Destroy(gameObject);
    }

}
