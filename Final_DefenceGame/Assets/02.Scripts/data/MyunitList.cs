using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyunitList : MonoBehaviour
{
    public int[] unitarray = new int[14];
    Dictionary<int, Unit> MyUnitDic = new Dictionary<int, Unit>();
    public class Unit : Stats
    {

    }
    MyUnit myunit;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        Unit units = new Unit();
        //myunit = GameObject.Find("StoreScene_Mng").GetComponent<MyUnit>();
        myunit =  GetComponent<MyUnit>();
        int[] array = myunit.GetKey();
        foreach (int number in array)
        {
            if (number == 0)
            {
                break;
            }
            unitarray[i] = number;
            i++;
            units.ID = myunit.FindDic(number).ID;
            units.Name = myunit.FindDic(number).Name;
            units.Hp = myunit.FindDic(number).Hp;
            units.Defence = myunit.FindDic(number).Defence;
            units.Attack = myunit.FindDic(number).Attack;
            units.AttackSpeed = myunit.FindDic(number).AttackSpeed;
            units.Range = myunit.FindDic(number).Range;
            units.MoveSpeed = myunit.FindDic(number).MoveSpeed;
            units.Race = myunit.FindDic(number).Race;
            units.Ability = myunit.FindDic(number).Ability;
            units.AbilityDetail = myunit.FindDic(number).AbilityDetail;
            units.Cost = myunit.FindDic(number).Cost;
            units.Slot = myunit.FindDic(number).Slot;
            units.Itemcode = myunit.FindDic(number).Itemcode;
            MyUnitDic.Add(units.ID, units);
         
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Unit FindDic(int id)
    {
        Unit json;
        if (MyUnitDic.TryGetValue(id, out json))
        {
            return json;
        }
        return null;
    }
    public int[] GetKey()
    {
        return unitarray;
    }
    public void AddUnit(int id)
    {
        Unit units = new Unit();
        units.ID = myunit.MyFindDic(id).ID;
        units.Name = myunit.MyFindDic(id).Name;
        units.Hp = myunit.MyFindDic(id).Hp;
        units.Defence = myunit.MyFindDic(id).Defence;
        units.Attack = myunit.MyFindDic(id).Attack;
        units.AttackSpeed = myunit.MyFindDic(id).AttackSpeed;
        units.Range = myunit.MyFindDic(id).Range;
        units.MoveSpeed = myunit.MyFindDic(id).MoveSpeed;
        units.Race = myunit.MyFindDic(id).Race;
        units.Ability = myunit.MyFindDic(id).Ability;
        units.AbilityDetail = myunit.MyFindDic(id).AbilityDetail;
        units.Cost = myunit.MyFindDic(id).Cost;
        units.Slot = myunit.MyFindDic(id).Slot;
        units.Itemcode = myunit.MyFindDic(id).Itemcode;
        MyUnitDic.Add(units.ID, units);
         

    }
}
