using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItem : MonoBehaviour
{
    TextAsset textData;
    ItemData Items;
    ItemDatas itemDatas;
    public int[] array = new int[10];
    public static Dictionary<int, Item> ItemDic = new Dictionary<int, Item>();
    public static Dictionary<int, Item> ItemList = new Dictionary<int, Item>();

    [System.Serializable]
    public class Item : Items
    {

    }

    [System.Serializable]
    public class ItemData
    {
        public Item[] MyItem;
    }

    [System.Serializable]
    public class ItemDatas
    {
        public Item[] ITEM;
    }
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        textData = Resources.Load("UnitData") as TextAsset;

        Items = JsonUtility.FromJson<ItemData>(textData.ToString());
        foreach (Item item in Items.MyItem)
        {
            
            ItemDic.Add(item.ID, item);

        }

        foreach (int itemID in ItemDic.Keys)
        {
            ItemDic[itemID].Print();
            array[i] = itemID;
            i++;
        }

        foreach (KeyValuePair<int, Item> item in ItemDic)
        {
            
            item.Value.Print();
            Debug.Log("=====");
        }

        itemDatas = JsonUtility.FromJson<ItemDatas>(textData.ToString());
        foreach (Item item in Items.MyItem)
        {
            ItemList.Add(item.ID, item);

        }

        foreach (int itemID in ItemList.Keys)
        {
            array[i] = itemID;
            i++;
        }

        foreach (KeyValuePair<int, Item> item in ItemList)
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

    public int[] GetKey()
    {
        return array;
    }

    public void AddItem(int id)
    {
        Item items = new Item();
        items.ID = ItemList[id].ID;
        items.Name = ItemList[id].Name;
        items.Hp = ItemList[id].Hp;
        items.Defence = ItemList[id].Defence;
        items.Attack = ItemList[id].Attack;
        items.AttackSpeed = ItemList[id].AttackSpeed;
        items.Range = ItemList[id].Range;
        items.MoveSpeed = ItemList[id].MoveSpeed;
        items.Grade = ItemList[id].Grade;
        items.UseStack = ItemList[id].UseStack;
        ItemDic.Add(items.ID, items);
        
    }
}
