using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


// 아이템 구입할 때 사용하는 InputNumberClass
public class InputNumber_BuyItem : MonoBehaviour
{
    // 실행중인지 확인
    bool isactive;

    // Input
    [SerializeField]
    Text text_Preview;
    [SerializeField]
    Text text_Input;
    [SerializeField]
    InputField inputField;

    [SerializeField]
    GameObject inputBase;

    // Shop
    [SerializeField]
    Shop shop;
    // Inventory
    [SerializeField]
    Inventory inven;
    // Item
    [SerializeField]
    Item[] item;

    // idx
    int idx;
    int n;

    // Player
    [SerializeField]
    Player player;

    // ObjectManager
    [SerializeField]
    ObjectManager objectManager;



    void Update()
    {
        if (isactive)
        {
            // 실행중일 때 OK와 Cancel 선택
            if (Input.GetKeyDown(KeyCode.Return))
                OK();
            else if (Input.GetKeyDown(KeyCode.Escape))
                Cancel();
        }
    }

    // Potion Buy
    public void RedPotionBuy() // 빨간포션 구입
    {
        inputBase.SetActive(true);
        isactive = true;
        inputField.text = "";
        text_Preview.text = (player.money / shop.itemPrice[0]).ToString();
        idx = 0;
    }

    public void BluePotionBuy() // 파란포션 구입
    {
        inputBase.SetActive(true);
        isactive = true;
        inputField.text = "";
        text_Preview.text = (player.money / shop.itemPrice[1]).ToString();
        idx = 1;
    }

    public void GreenPotionBuy() // 그린포션 구입
    {
        inputBase.SetActive(true);
        isactive = true;
        inputField.text = "";
        text_Preview.text = (player.money / shop.itemPrice[2]).ToString();
        idx = 2;
       
    }

    // Cancel
    public void Cancel()
    {
        isactive = false;
        inputBase.SetActive(false);
    }

    public void OK() // 선택개수만큼 Buy
    {

        if (text_Input.text != "")
        {
            if (CheckNumber(text_Input.text))
            {
                
                n = int.Parse(text_Input.text);
                if (idx == 0)
                {
                    if (player.money >= (shop.itemPrice[0] * n))
                    {
                        shop.Buy(idx, n);
                        inven.AcquireItem(item[idx], n);
                    }
                    else
                        shop.StartCoroutine("Talk");
                }

                else if(idx == 1)
                {
                    if (player.money >= (shop.itemPrice[1] * n))
                    {
                        shop.Buy(idx, n);
                        inven.AcquireItem(item[idx], n);
                    }
                    else
                        shop.StartCoroutine("Talk");
                }

                else if(idx == 2)
                {
                    if (player.money >= (shop.itemPrice[2] * n))
                    {
                        shop.Buy(idx, n);
                        inven.AcquireItem(item[idx], n);
                    }
                    else
                        shop.StartCoroutine("Talk");

                }
            }
            else
                n = 1;
        }

        else
            n = int.Parse(text_Preview.text);

        inputBase.SetActive(false);


    }

    // 숫자확인
    bool CheckNumber(string s)
    {
        char[] tmp = s.ToCharArray();
        bool isNumber = true;

        for (int i = 0; i < tmp.Length; i++)
        {
            if (tmp[i] >= 48 && tmp[i] <= 57)
                continue;
            isNumber = false;
        }
        return isNumber;
    }
}
