using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // UI
    [SerializeField]
    RectTransform ShopGroup;
    [SerializeField]
    Text talkText;
    [SerializeField]
    GameObject visitText;


    // Animation
    [SerializeField]
    Animator anim;

    // Player
    [SerializeField]
    Player player;

    
    public int[] itemPrice; // 아이템 가격 배열

    // string배열 기본대사와, 돈이없을 때 대사를 텍스트에 할당함.
    [SerializeField]
    string[] talkData;


    // Sounds
    [SerializeField]
    AudioSource enterSound;
 
    // 구입함수
    public void Buy(int idx, int n) // n개를 구입하고 player의 돈을 뺀다.
    {
        int price = itemPrice[idx] * n;
        player.money -= price;
    }
   

    // Trigger 함수
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            enterSound.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(!player.isShop)
            {
                visitText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    visitText.SetActive(false);
                    player.isShop = true;
                    ShopGroup.anchoredPosition = Vector3.zero;
                }
            }

            else if(player.isShop && Input.GetKeyDown(KeyCode.Escape))
            {
                visitText.SetActive(true);
                player.isShop = false;
                anim.SetTrigger("doHello");
                ShopGroup.anchoredPosition = Vector3.down * 500;
            }

        }

    }

    // 상점을 이용 중 취소버튼을 누르면 상점창이 닫힌다.
    public void Cancel()
    {
        visitText.SetActive(true);
        player.isShop = false;
        anim.SetTrigger("doHello");
        ShopGroup.anchoredPosition = Vector3.down * 500;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            anim.SetTrigger("doHello");
            visitText.SetActive(false);
        }
    }

    // 상점에 입장하면 나오는 대화
    public IEnumerator Talk()
    {
        talkText.text = talkData[1];
        yield return new WaitForSeconds(5f);
        talkText.text = talkData[0];
    }
}
