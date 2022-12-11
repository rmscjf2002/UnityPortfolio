using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

// Slot클래스 : 마우스 이벤트들을 상속받음.
public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item; // 획득한 아이템
    public int itemCnt; // 아이템의 개수
    public Image itemImage; // 아이템의 이미지


    // UI 
    [SerializeField]
    Text textCnt;
    [SerializeField]
    GameObject cntImage;
    [SerializeField]
    InputNumber inputNumber;
    [SerializeField]
    ItemUseEffect itemUseEffect;

    [SerializeField]
    RectTransform quickSlotRect; // 퀵슬롯 영역

    [SerializeField]
    RectTransform baseRect; // InventoryBase의 Rect정보


    [SerializeField]
    bool isQuickSlot; // 퀵슬롯인지 판별
    [SerializeField]
    int quickSlotidx; // 퀵슬롯 인덱스


    // Inventory Manager
    [SerializeField]
    InventoryManager invenManager;
    

    // 이미지의 알파값 조절
    void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(Item _item, int cnt = 1)
    {
        item = _item;
        itemCnt = cnt;
        itemImage.sprite = item.itemImage;


        if(item.itemType != Item.ItemType.Equipment)
        {
            cntImage.SetActive(true);
            textCnt.text = itemCnt.ToString();
        }
        else
        {
            textCnt.text = "0";
            cntImage.SetActive(false);
        }

        SetColor(1);
    }

    // 아이템 개수 업데이트
    public void SetSlotCnt(int cnt)
    {
        itemCnt += cnt;
        textCnt.text = itemCnt.ToString();
        invenManager.SaveData();
        if (itemCnt <= 0)
            ClearSlot();
    }

    // 슬롯 하나 삭제
    void ClearSlot()
    {
        item = null;
        itemCnt = 0;
        itemImage.sprite = null;
        SetColor(0);

        textCnt.text = "0";
        cntImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) // 우클릭으로 아이템 장착or소비
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                itemUseEffect.UseItem(item);
                if (item.itemType != Item.ItemType.Equipment)
                    SetSlotCnt(-1);
                
            }
        }
       
    }

    // 마우스 드래그 이벤트

    // 마우스 드래그를 시작했을 때 발생
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    // 마우스 드래그중일 때

    public void OnDrag(PointerEventData eventData)
    { 
       if(item != null)
            DragSlot.instance.transform.position = eventData.position;
    }


    // 드래그가 끝났을 때
    public void OnEndDrag(PointerEventData eventData)
    {
      

        if (!((DragSlot.instance.transform.localPosition.x > baseRect.rect.xMin 
            && DragSlot.instance.transform.localPosition.x < baseRect.rect.xMax
          && DragSlot.instance.transform.localPosition.y > baseRect.rect.yMin 
          && DragSlot.instance.transform.localPosition.y < baseRect.rect.yMax)
          || (DragSlot.instance.transform.localPosition.x > quickSlotRect.rect.xMin 
          && DragSlot.instance.transform.localPosition.x < quickSlotRect.rect.xMax
          && DragSlot.instance.transform.localPosition.y + 
          baseRect.transform.localPosition.y > quickSlotRect.rect.yMin+ quickSlotRect.transform.localPosition.y 
          && DragSlot.instance.transform.localPosition.y + 
          baseRect.transform.localPosition.y < quickSlotRect.rect.yMax + quickSlotRect.transform.localPosition.y)))
        {
            if (DragSlot.instance.dragSlot != null)
                inputNumber.Call();
        }

        else
        {

            DragSlot.instance.SetColor(0);
            DragSlot.instance.dragSlot = null;
        }
    }


    // 마우스 드롭 이벤트

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
            ChangeSlot();
    }

    void ChangeSlot()
    {
        Item tmpItem = item;
        int tmpItemCnt = itemCnt;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCnt);

        if (tmpItem != null)
            DragSlot.instance.dragSlot.AddItem(tmpItem, tmpItemCnt);
        else
            DragSlot.instance.dragSlot.ClearSlot();

    }


}
