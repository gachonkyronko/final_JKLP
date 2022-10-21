using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demage : MonoBehaviour
{
    public int[] unitarray = new int[14];
    public int[] itemarray = new int[11];
    Dictionary<int, Unit> MyUnitDic = new Dictionary<int, Unit>();
    public class Unit : Stats
    {

    }
    public class Item : Items
    {

    }
    MyunitList myunitList;
    MyItem myItem;
    // Start is called before the first frame update
    void Start()
    {
        Unit units = new Unit();
        myunitList = GameObject.Find("GameObject").GetComponent<MyunitList>();

        Item items = new Item();
        myItem = GameObject.Find("GameObject").GetComponent<MyItem>();

        int[] unitarray = myunitList.GetKey();
        int[] itemarray = myItem.GetKey();

        foreach (int number in unitarray)
        {
            if (number == 0)
            {
                break;
            }
            int checkitem = myunitList.FindDic(number).Itemcode;
            if (checkitem == 0)
            {
                units.ID = myunitList.FindDic(number).ID;
                units.Name = myunitList.FindDic(number).Name;
                units.Hp = myunitList.FindDic(number).Hp;
                units.Defence = myunitList.FindDic(number).Defence;
                units.Attack = myunitList.FindDic(number).Attack;
                units.AttackSpeed = myunitList.FindDic(number).AttackSpeed;
                units.Range = myunitList.FindDic(number).Range;
                units.MoveSpeed = myunitList.FindDic(number).MoveSpeed;
                units.Race = myunitList.FindDic(number).Race;
                units.Ability = myunitList.FindDic(number).Ability;
                units.AbilityDetail = myunitList.FindDic(number).AbilityDetail;
                units.Cost = myunitList.FindDic(number).Cost;
                units.Slot = myunitList.FindDic(number).Slot;
                units.Itemcode = myunitList.FindDic(number).Itemcode;
                MyUnitDic.Add(units.ID, units);
                MyUnitDic[number].Print();
            }
            else
            {
                units.ID = myunitList.FindDic(number).ID;
                units.Name = myunitList.FindDic(number).Name;
                units.Hp = myunitList.FindDic(number).Hp + myItem.FindDic(checkitem).Hp;
                units.Defence = myunitList.FindDic(number).Defence + myItem.FindDic(checkitem).Defence;
                units.Attack = myunitList.FindDic(number).Attack + myItem.FindDic(checkitem).Attack;
                units.AttackSpeed = myunitList.FindDic(number).AttackSpeed + myItem.FindDic(checkitem).AttackSpeed;
                units.Range = myunitList.FindDic(number).Range + myItem.FindDic(checkitem).Range;
                units.MoveSpeed = myunitList.FindDic(number).MoveSpeed + myItem.FindDic(checkitem).MoveSpeed;
                units.Race = myunitList.FindDic(number).Race;
                units.Ability = myunitList.FindDic(number).Ability;
                units.AbilityDetail = myunitList.FindDic(number).AbilityDetail;
                units.Cost = myunitList.FindDic(number).Cost;
                units.Slot = myunitList.FindDic(number).Slot;
                units.Itemcode = myunitList.FindDic(number).Itemcode;
                MyUnitDic.Add(units.ID, units);
                MyUnitDic[number].Print();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
