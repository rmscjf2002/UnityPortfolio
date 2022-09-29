using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


// 드래그 슬롯 함수
public class DragSlot : MonoBehaviour
{

    
    static public DragSlot instance; // 자기자신을 담는 변수, static으로 선언
    public Slot dragSlot; // Slot의 Sprite를 복사하기 위한 변수


    // ItemImage
    [SerializeField]
    Image itemImage; 

    void Start()
    {
        instance = this;
    }

 
    // 드래그 되는 슬롯의 이미지를 할당
    public void DragSetImage(Image _itemImage)
    {
        itemImage.sprite = _itemImage.sprite;
        SetColor(1);
    }

    // 이미지의 알파값 설정
    public void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
}
