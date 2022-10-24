using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    TextAsset textData;
    ItemData Items;
    public int[] itemID = new int[100];
    public static Dictionary<int, Item> ItemDic = new Dictionary<int, Item>();
    public static Dictionary<string, Item> ItemList_Name = new Dictionary<string, Item>();
    [System.Serializable]
    public class Item : Items
    {

    }

    [System.Serializable]
    public class ItemData
    {
        public Item[] ITEM;
    }
    // Start is called before the first frame update
    void Awake()
    {
        int i = 0;
        textData = Resources.Load("UnitData") as TextAsset;

        Items = JsonUtility.FromJson<ItemData>(textData.ToString());
        foreach (Item item in Items.ITEM)
        {
            Item items = new Item();
            items.ID = item.ID;
            items.Name = item.Name;
            items.Hp = item.Hp;
            items.Defence = item.Defence;
            items.Attack = item.Attack;
            items.AttackSpeed = item.AttackSpeed;
            items.Range = item.Range;
            items.MoveSpeed = item.MoveSpeed;
            items.Grade = item.Grade;
            items.UseStack = item.UseStack;
            ItemList_Name.Add(items.Name, items);
            ItemDic.Add(item.ID, item);
        }

        foreach (int itemiD in ItemDic.Keys)
        {

             
            itemID[i] = itemiD;
            i++;
        }

        foreach (KeyValuePair<int, Item> item in ItemDic)
        {
             
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Item FindDic(int id)
    {
        Item json;
        if (ItemDic.TryGetValue(id, out json))
        {
            return json;
        }
        return null;
    }
    public Item FindDic_Name(string name)
    {
        Item json;
        if (ItemList_Name.TryGetValue(name, out json))
        {
            return json;
        }
        return null;
    }

    public int[] GetKey()
    {
        
        return itemID;
    }
}

