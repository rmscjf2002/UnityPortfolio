using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockShot : MonoBehaviour
{
    Rigidbody rigid; // Rigidbody
    public Player player; // player
    public ObjectManager objectManager; // objectManager
    public bool isRock; // isRock을 사용했는지 확인
    public int damage; // 데미지

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        if(isRock)
            StartCoroutine(Attack());
        
    }

    // Attack Coroutine

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.35f);
        Vector3 diff = (player.trans.position - transform.position);
        rigid.AddForce(diff * Time.deltaTime * 100, ForceMode.Impulse);
    }

    // 벽에닿으면 비활성화

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
    }

}
