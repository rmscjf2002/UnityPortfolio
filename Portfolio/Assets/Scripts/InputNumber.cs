using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InputNumber : MonoBehaviour
{

    bool isactive; // 실행중인지 확인

    // Input
    [SerializeField]
    Text text_Preview;
    [SerializeField]
    Text text_Input;
    [SerializeField]
    InputField inputField;
    [SerializeField]
    GameObject inputBase;
    
    // Player
    [SerializeField]
    Player player;

    // ObjectManager
    [SerializeField]
    ObjectManager objectManager;


    void Update()
    {
        if(isactive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                OK();
            else if (Input.GetKeyDown(KeyCode.Escape))
                Cancel();
        }
    }

    //  인풋필드를 활성화
    public void Call()
    {
        inputBase.SetActive(true);
        isactive = true;
        inputField.text = "";
        text_Preview.text = DragSlot.instance.dragSlot.itemCnt.ToString();
    }

    // 취소버튼
    public void Cancel()
    {
        isactive = false;
        DragSlot.instance.SetColor(0);
        inputBase.SetActive(false);
        DragSlot.instance.dragSlot = null;
    }

    // OK를 누르면 입력한 숫자만큼 Drop
    public void OK()
    {
        DragSlot.instance.SetColor(0);

        int n;

        if (text_Input.text != "")
        {
            if (CheckNumber(text_Input.text))
            {
                n = int.Parse(text_Input.text);
                if (n > DragSlot.instance.dragSlot.itemCnt)
                    n = DragSlot.instance.dragSlot.itemCnt;
            }
            else
                n = 1;
        }

        else
            n = int.Parse(text_Preview.text);

        StartCoroutine(DropItem(n));
    }

    // DropItem 아이템 버리기
    IEnumerator DropItem(int n)
    {
        for(int i= 0; i<n; i++)
        {
            GameObject itemObject = objectManager.MakeObj(DragSlot.instance.dragSlot.item.itemName);
            itemObject.transform.position = new Vector3(player.trans.position.x, 1, player.trans.position.z);
            itemObject.transform.rotation = Quaternion.identity;
            DragSlot.instance.dragSlot.SetSlotCnt(-1);
            yield return new WaitForSeconds(0.05f);
        }

        DragSlot.instance.dragSlot = null;
        inputBase.SetActive(false);
        isactive = false;
    }

    // 숫자확인
    bool CheckNumber(string s)
    {
        char[] tmp = s.ToCharArray();
        bool isNumber = true;

        for(int i=0; i<tmp.Length; i++)
        {
            if (tmp[i] >= 48 && tmp[i] <= 57) // 문자면 continue
                continue;
            isNumber = false;
        }
        return isNumber;
    }
}
