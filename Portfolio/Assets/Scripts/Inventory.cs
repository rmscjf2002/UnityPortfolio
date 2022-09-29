using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


// 인벤토리 클래스
public class Inventory : MonoBehaviour
{
    // 인벤토리 활성화 여부
    public bool inventoryActivated;

    // 인벤토리 Base
    [SerializeField]
    GameObject inventoryBase;

    public Slot[] slots; // Slot들 배열



    void Update()
    {
        TryOpenInventroy();
    }


    // 인벤토리 실행
    void TryOpenInventroy()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    // 인벤토리 열기
    void OpenInventory()
    {
        inventoryBase.SetActive(true);
    }

    // 인벤토리 닫기
    public void CloseInventory()
    {
        if(inventoryActivated) // 버튼을 누를 떄 
            inventoryActivated = !inventoryActivated;
        inventoryBase.SetActive(false);
    }

    // 아이템 획득시 인벤토리에 추가
    public void AcquireItem(Item _item, int cnt = 1)
    {
        if(Item.ItemType.Equipment != _item.itemType)
        {
            for(int i=0; i<slots.Length; i++)
            {
                if(slots[i].item != null)
                {
                    if(slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCnt(cnt);
                        return;
                    }
                }
            }
        }

        for(int i=0; i<slots.Length; i++)
        {
            if(slots[i].item == null)
            {
                slots[i].AddItem(_item, cnt);
                return;
            }
        }
    }


}
