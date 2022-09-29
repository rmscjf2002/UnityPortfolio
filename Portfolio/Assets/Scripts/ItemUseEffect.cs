using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] // 인스펙터에 노출되어 값 지정가능
public class ItemEffect
{
    public string itemName;  // 아이템의 이름(Key값으로 사용할 것)
    [Tooltip("HP , Point 아이템만 사용 가능합니다.")]
    public string[] part; 
    public int[] num;  // 수치. 포션 하나당 미치는 효과가 여러개일 수 있어 배열. 그에 따른 수치.
}

public class ItemUseEffect : MonoBehaviour
{
    // 배열로 아이템이펙트할당
    [SerializeField]
    ItemEffect[] itemEffects;

    // GameManager
    [SerializeField]
    GameManager manager;

    // 상수설정
    const string HP = "HP";
    const string POINT = "POINT";
    const string COIN = "COIN";

    
    // 아이템 사용
    // 맞는 정보를 찾아서 아이템 Use
   public void UseItem(Item _item)
    {
        if(_item.itemType == Item.ItemType.Used)
        {
            for(int i=0; i<itemEffects.Length; i++)
            {
                if(itemEffects[i].itemName == _item.itemName)
                {
                   for(int j=0; j<itemEffects[i].part.Length; j++)
                    {
                        switch(itemEffects[i].part[j])
                        {
                            case HP:
                                manager.PortionUse(itemEffects[i].num[j]);
                                break;
                            case POINT:
                                manager.PointPortionUse(itemEffects[i].num[j]);
                                break;

                            case COIN:
                                manager.CoinUse(itemEffects[i].num[j]);
                                break;
                          

                        }
                    }
                    return;
                }
            }
        }
    }
    
}
