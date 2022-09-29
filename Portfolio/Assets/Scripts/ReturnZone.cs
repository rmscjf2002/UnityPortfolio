using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnZone : MonoBehaviour
{
    // Text Object
    [SerializeField]
    GameObject enterReturnZoneText;

    // boss 
    [SerializeField]
    GameObject boss;
    [SerializeField]
    GameObject bossFloor;

    // map
    [SerializeField]
    GameObject Floor;
    [SerializeField]
    Player player;
    [SerializeField]
    GameObject shop;
    [SerializeField]
    GameObject bossZone;
    [SerializeField]
    GameObject npc;
    [SerializeField]
    GameObject enemySpawnZone;

    // Sounds
    [SerializeField]
    AudioSource bossBgm;
    [SerializeField]
    AudioSource gameSound;

    // GameManager
    [SerializeField]
    GameManager manager;


    // Trigger 함수

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            enterReturnZoneText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                SetActiveFunc();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enterReturnZoneText.SetActive(false);
        }
    }

    // 오브젝트들의 활성화/비활성화 
    public void SetActiveFunc()
    {
        manager.boss = null;
        bossBgm.Stop();
        gameSound.Play();
        enemySpawnZone.SetActive(true);
        enterReturnZoneText.SetActive(false);
        boss.SetActive(false);
        bossZone.SetActive(true);
        bossFloor.SetActive(false);
        Floor.SetActive(true);
        shop.SetActive(true);
        npc.SetActive(true);
        this.gameObject.SetActive(false);
        player.transform.position = Vector3.zero;
    }
}
