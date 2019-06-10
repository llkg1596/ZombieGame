using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // AI, 내비게이션 시스템 관련 코드를 가져오기

public delegate void MonsterMoveAI();
public delegate void MonsterAttackAI();

public enum MobState
{
    MOVE, ATTACK
}
public class MonsterControl : Control
{
    public MobState state;
    public event MonsterMoveAI monsterMoveAI;   //AttackAIRange 스크립트 참고
    public event MonsterAttackAI monsterAttackAI;   //AttackAIRange 스크립트 참고
    public Animator enemyAnimator; // 애니메이터 컴포넌트

    public GameObject targetEntity; // 추적할 대상
    private NavMeshAgent pathFinder; // 경로계산 AI 에이전트

    private bool hasTarget
    {
        get
        {
            if (targetEntity != null)
            {
                return true;
            }

            return false;
        }
    }

    void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        state = MobState.MOVE;

        pathFinder = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        targetEntity = PlayManager.Instance.GetTheObject();
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        while (true)
        {
            if (hasTarget)
            {
                // 추적 대상 존재 : 경로를 갱신하고 AI 이동을 계속 진행
                pathFinder.isStopped = false;
                pathFinder.SetDestination(
                    targetEntity.transform.position);
            }
            else
            {
                // 추적 대상 없음 : AI 이동 중지
                pathFinder.isStopped = true;
            }

            // 0.25초 주기로 처리 반복
            yield return new WaitForSeconds(0.25f);
        }
    }

    void Update()
    {
        if (state == MobState.MOVE)
            //monsterMoveAI();

        enemyAnimator.SetBool("HasTarget", hasTarget);
    }
}
