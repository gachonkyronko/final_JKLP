using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItem : MonoBehaviour
{
    TextAsset textData;
    ItemData Items;
    public int[] array = new int[10];
    public static Dictionary<int, Item> ItemDic = new Dictionary<int, Item>();

    [System.Serializable]
    public class Item : Items
    {

    }

    [System.Serializable]
    public class ItemData
    {
        public Item[] MyItem;
    }
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        textData = Resources.Load("UnitData") as TextAsset;

        Items = JsonUtility.FromJson<ItemData>(textData.ToString());
        foreach (Item item in Items.MyItem)
        {
            Debug.Log(item.ID);
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
            Debug.Log(item.Key);
            item.Value.Print();
            Debug.Log("=====");
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
}