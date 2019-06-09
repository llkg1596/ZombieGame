using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : Monster
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        HP = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
