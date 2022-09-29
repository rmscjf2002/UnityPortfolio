using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotManager : MonoBehaviour
{
    [SerializeField]
    Slot[] quickSlots; // 퀵슬롯 배열
    [SerializeField]
    Transform parent_trans; // 퀵슬롯 부모오브젝트의 트랜스폼




    [SerializeField]
    ItemUseEffect itemUse; // ItemUseEffect

    void Start()
    {
        quickSlots = parent_trans.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
        GetInput();
    }

    void GetInput() // 퀵슬롯버튼을 누르고 사용
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (quickSlots[0] != null && quickSlots[0].itemCnt > 0)
            {
                itemUse.UseItem(quickSlots[0].item);
                quickSlots[0].SetSlotCnt(-1);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (quickSlots[1] != null && quickSlots[1].itemCnt > 0)
            {
                itemUse.UseItem(quickSlots[1].item);
                quickSlots[1].SetSlotCnt(-1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (quickSlots[2] != null && quickSlots[2].itemCnt > 0)
            {
                itemUse.UseItem(quickSlots[2].item);
                quickSlots[2].SetSlotCnt(-1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (quickSlots[3] != null && quickSlots[3].itemCnt > 0)
            {
                itemUse.UseItem(quickSlots[3].item);
                quickSlots[3].SetSlotCnt(-1);
            }
        }
    }




    
    

}
