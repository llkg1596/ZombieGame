using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MonsterMoveAI();

public class MonsterControl : Control
{
    public event MonsterMoveAI monsterMoveAI;   //AttackAIRange 스크립트 참고
    public Animator enemyAnimator; // 애니메이터 컴포넌트

    void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        monsterMoveAI();
    }

    

}
