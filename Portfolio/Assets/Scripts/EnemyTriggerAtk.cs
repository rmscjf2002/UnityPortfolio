using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enemy 공격 범위 클래스
public class EnemyTriggerAtk : MonoBehaviour
{
    

    public GameObject attackArea; // 공격범위
    public GameObject enemy;

    
    // Player가 공격범위에 있으면 Trigger 실행
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
           Enemy enemycs = enemy.GetComponent<Enemy>();
           EnemyAttack enemyAtk =  attackArea.GetComponent<EnemyAttack>();
            if (!enemycs.isAttack)
            {
                enemyAtk.StartCoroutine("Attack");
            }
        }
    }
}
