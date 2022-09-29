using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{



    // Item
    GameObject[] redPotion; 
    GameObject[] bluePotion;
    GameObject[] greenPotion;
    GameObject[] coin;

    // Pool
    GameObject[] targetPool;

    // Boss 패턴 중 Rock 패턴
    GameObject[] bossRock;


    // Prefabs

    [SerializeField]
    GameObject redPotion_Prefab;
    [SerializeField]
    GameObject bluePotion_Prefab;
    [SerializeField]
    GameObject greenPotion_Prefab;
    [SerializeField]
    GameObject coin_Prefab;
    [SerializeField]
    GameObject bossRock_Prefab;


    void Awake()
    {

        redPotion = new GameObject[20];
        bluePotion = new GameObject[20];
        greenPotion = new GameObject[20];
        coin = new GameObject[20];
        bossRock = new GameObject[5];

        Generate();
    }

    // 프리펩을 미리 생성하는 함수
    void Generate()
    {
       

        // Item

        for(int i=0; i<redPotion.Length; i++)
        {
            redPotion[i] = Instantiate(redPotion_Prefab);
            redPotion[i].SetActive(false);
        }

        for(int i=0; i<bluePotion.Length; i++)
        {
            bluePotion[i] = Instantiate(bluePotion_Prefab);
            bluePotion[i].SetActive(false);
        }

        for(int i=0; i<greenPotion.Length; i++)
        {
            greenPotion[i] = Instantiate(greenPotion_Prefab);
            greenPotion[i].SetActive(false);
        }

        for(int i=0; i<coin.Length; i++)
        {
            coin[i] = Instantiate(coin_Prefab);
            coin[i].SetActive(false);
        }

        // Boss

        for(int i=0; i<bossRock.Length; i++)
        {
            bossRock[i] = Instantiate(bossRock_Prefab);
            bossRock[i].SetActive(false);
        }
    }

    // 오브젝트를 활성화
    public GameObject MakeObj(string Type)
    {
        switch (Type)
        {
            
            case "RedPotion":
                targetPool = redPotion;
                break;

            case "BluePotion":
                targetPool = bluePotion;
                break;

            case "GreenPotion":
                targetPool = greenPotion;
                break;

            case "Coin":
                targetPool = coin;
                break;

            case "BossRock":
                targetPool = bossRock;
                break;
        }

        for(int i=0; i<targetPool.Length; i++)
        {
            if(!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }


 
}
