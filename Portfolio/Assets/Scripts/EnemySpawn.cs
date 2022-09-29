using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Enemy지역에 생성될 Enemy할당
    public GameObject[] enemies;

    // Sounds
    [SerializeField]
    AudioSource enemyZoneSound;
    [SerializeField]
    AudioSource gameSound;
 

    // Trigger 함수 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { // Player가 입장하면 실행
            gameSound.Stop();
            enemyZoneSound.Play();
            foreach (GameObject enemy in enemies)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (!enemyScript.isDead)
                {
                    enemyScript.Sense();
                    enemyScript.isChase = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyZoneSound.Stop();
            gameSound.Play();
            foreach (GameObject enemy in enemies)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (!enemyScript.isDead)
                {
                    enemyScript.returnPos();
                }
                enemyScript.isChase = false;
                
            }
        }
    }

}
