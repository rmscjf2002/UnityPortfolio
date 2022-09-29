using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{

    // 상호작용위한 오브젝트
    [SerializeField]
    GameObject visitText;

    // GameManager
    [SerializeField]
    GameManager manager;

    // Animation
    [SerializeField]
    Animator anim;


    // Trigger 함수

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {

            Player player = other.GetComponent<Player>();
            if (!player.isTalk) // isTalk가 false면 대화시작
            {
                visitText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    visitText.SetActive(false);
                    player.isTalk = true;
                    manager.isQuest = true;
                    anim.SetTrigger("doTalk");
                    manager.Action();
                }
            }


            else if (player.isTalk) // 대화중이면 계속Action
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    manager.isQuest = true;
                    manager.Action();
                }
            }



        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            anim.SetTrigger("doHello");
            player.isTalk = false;
            manager.isQuest = false;
            visitText.SetActive(false);
        }
    }
}
