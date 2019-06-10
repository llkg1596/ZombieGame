using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject mob;
    public int mobNum = 10;      //소환할 몹 개수
    public float spawnSpd = 1f;

    private void Start()
    {
        mobNum = 10;
        StartCoroutine(SpawnMob());
    }

    //Init 함수 만들어야함

    IEnumerator SpawnMob()
    {
        int count = 0;

        while(true)
        {
            if (mobNum == count)
                break;

            GameObject temp = Instantiate(mob, transform.position, Quaternion.identity);

            count++;

            yield return new WaitForSeconds(1f * spawnSpd);
        }

    }
}
