using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// EnemyAttack의 상속을 받은 클래스
// boss의 근접공격에 대한 스크립트
public class BossMeleeAttack : EnemyAttack

{

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }


    // Attack
    public override IEnumerator Attack()
    {
       // Boss enemycs = enemy.GetComponent<Boss>();

        boxCollider.enabled = true;
        attackSound.Play();
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        Boss bosscs = enemy.GetComponent<Boss>();
        bosscs.StartCoroutine("Think");

    }
}
