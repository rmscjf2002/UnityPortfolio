using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


// Weapon클래스
public class Weapon : MonoBehaviour
{

    public BoxCollider meleeArea; // 공격 범위
    public TrailRenderer trailEffect; // 트레일이펙트
    public int damage; // 데미지



    // 실행중인 어택코루틴을 중지시키고 어택코루틴 실행
    public void use()
    {
        StopCoroutine(Attack());
        StartCoroutine(Attack());
    }

    // 어택코루틴
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.1f);

        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;

     
 

    }

}
