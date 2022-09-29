﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]

// ScriptableObject로 생성 (에셋)
public class Item : ScriptableObject
{

    public enum ItemType // 아이템 유형
    {
        Equipment, // 장비
        Used, // 소모품 
        ETC, // 기타아이템
    }

    public string itemName; // 아이템 이름
    public ItemType itemType; // 아이템 타입
    public Sprite itemImage; // 아이템 이미지
    public GameObject itemPrefab; // 아이템 프리펩


}
