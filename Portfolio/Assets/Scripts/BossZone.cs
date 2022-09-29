using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{
    [SerializeField]
    GameObject enterBossZoneText;

    // 보스관련 변수
    [SerializeField]
    GameObject boss;
    [SerializeField]
    GameObject bossFloor;

    // 마을관련 변수
    [SerializeField]
    GameObject returnZone;
    [SerializeField]
    GameObject Floor;
    [SerializeField]
    Player player;
    [SerializeField]
    GameObject shop;
    [SerializeField]
    GameObject npc;
    [SerializeField]
    GameObject enemySpawnZone;

    // 게임매니저
    [SerializeField]
    GameManager manager;


    // Trigger 함수
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            enterBossZoneText.SetActive(true);

            if(Input.GetKeyDown(KeyCode.F))
            {
                BossZoneFunc();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            enterBossZoneText.SetActive(false);
        }
    }

    // F키를 눌렀을 때 오브젝트들의 활성화/비활성화 관리
    void BossZoneFunc()
    {
        manager.boss = boss.GetComponent<Boss>();
        enemySpawnZone.SetActive(false);
        enterBossZoneText.SetActive(false);
        Floor.SetActive(false);
        returnZone.SetActive(true);
        boss.SetActive(true);
        bossFloor.SetActive(true);
        shop.SetActive(false);
        npc.SetActive(false);
        this.gameObject.SetActive(false);
        player.transform.position = Vector3.zero;
    }
}
