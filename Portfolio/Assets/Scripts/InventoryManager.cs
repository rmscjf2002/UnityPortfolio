using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;



public class InventoryManager : MonoBehaviour
{
    public Item[] items;
    public Slot[] slots;


    
    public void SaveData()
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement xmlEl = xmlDoc.CreateElement("ItemDB");
        xmlDoc.AppendChild(xmlEl);

       for(int i=0; i<slots.Length; i++)
        {

            if (slots[i].item == null)
                continue;

            XmlElement elementSet = xmlDoc.CreateElement("Item");

            elementSet.SetAttribute("SlotNum", i.ToString()); 
            elementSet.SetAttribute("ItemName", slots[i].item.itemName);
            elementSet.SetAttribute("ItemCnt", slots[i].itemCnt.ToString());
            xmlEl.AppendChild(elementSet);

        }

        xmlDoc.Save("Assets/Resources/InventoryData.xml");
    }

    public void Load()
    {
        if (!System.IO.File.Exists("Assets/Resources/InventoryData.xml"))
            return;

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("Assets/Resources/InventoryData.xml");
        XmlElement xmlEl = xmlDoc["ItemDB"];

        foreach(XmlElement itemElement in xmlEl.ChildNodes)
        {

            Item item = ScriptableObject.CreateInstance<Item>();
            int idx = System.Convert.ToInt32(itemElement.GetAttribute("SlotNum"));
            string name = itemElement.GetAttribute("ItemName");
            int cnt = System.Convert.ToInt32(itemElement.GetAttribute("ItemCnt"));

            if (name == "RedPotion")
                item = items[0];


            else if (name == "BluePotion")
                item = items[1];


            else if (name == "GreenPotion")
                item = items[2];

            else if (name == "Coins")
                item = items[3];

            if (cnt >= 1)
                slots[idx].AddItem(item, cnt);

        }

        return;
    }
    
}
