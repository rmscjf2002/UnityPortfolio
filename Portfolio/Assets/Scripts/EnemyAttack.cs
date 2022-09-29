using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    //Attack
    public int damage;
    protected BoxCollider boxCollider;
    public GameObject enemy;

    // Sounds
    public AudioSource attackSound;


    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

 
    // Attack Coroutine
    public virtual IEnumerator Attack()
    {
        Enemy enemycs = enemy.GetComponent<Enemy>();
        enemycs.isChase = false;
        enemycs.isAttack = true;
    

        int pattern = Random.Range(0, 2);
        yield return new WaitForSeconds(1f);
        Animator anim = enemy.GetComponentInChildren<Animator>();
        anim.SetTrigger("doAttack");
        anim.SetFloat("attackPattern", pattern);
        boxCollider.enabled = true;
        attackSound.Play();
        yield return new WaitForSeconds(1f);
        boxCollider.enabled = false;
        
        enemycs.isChase = true;
        enemycs.isAttack = false;
    }
}
