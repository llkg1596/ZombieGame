using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRanage : MonoBehaviour
{
    public Monster parentMonster;

    void Start()
    {
        parentMonster = GetComponentInParent<Monster>();
    }
}
