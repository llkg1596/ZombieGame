using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Monster : CharacterData
{
    public MonsterControl monsterControl;
    public Animator enemyAnimator; // 애니메이터 컴포넌트
    private NavMeshAgent pathFinder; // 경로계산 AI 에이전트

    public virtual void Start()
    {
        monsterControl = GetComponent<MonsterControl>();
        enemyAnimator = GetComponent<Animator>();
        pathFinder = GetComponent<NavMeshAgent>();

    }

    public MonsterControl GetControl()
    {
        return monsterControl;
    }

    public override void DestroyCall()
    {
        StartCoroutine(DestroyEnemyRoutine());
    }

    private IEnumerator DestroyEnemyRoutine()
    {
        PlayManager.Instance.InstantiateDropBox(transform.position + new Vector3(0f, 0.5f, 0f));

        Collider[] enemyColliders = GetComponents<Collider>();
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].enabled = false;
        }

        // AI 추적을 중지하고 내비메쉬 컴포넌트를 비활성화
        pathFinder.isStopped = true;
        pathFinder.enabled = false;
        monsterControl.StopAllCoroutines();

        // 사망 애니메이션 재생
        enemyAnimator.SetTrigger("Die");

        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }

}
