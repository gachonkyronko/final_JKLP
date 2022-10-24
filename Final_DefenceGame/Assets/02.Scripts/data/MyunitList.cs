using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class MyunitList : MonoBehaviour
{
    TextAsset textData;

    public int[] unitarray = new int[14];
    public int[] newunit = new int[100];
    Dictionary<int, Unit> MyUnitDic = new Dictionary<int, Unit>();
    Dictionary<string, Unit> MyUnitDic_Name = new Dictionary<string, Unit>();
    public string[] unitarray_name = new string[49];
    test test12;
    public int i = 0;
    public class Unit : Stats
    {

    }
    UnitList unitlist;
    [System.Serializable]
    public class test
    {
        public int[] Array = new int[100];

        public int[] Printnum()
        {
            for(int i = 0; i <Array.Length; i ++)
            {
                if (Array[i] == 0)
                {
                    break;
                }
            }
            return Array;
        }
    }



    // Start is called before the first frame update
    void Awake()
    {
        
        Unit units = new Unit();
        //myunit = GameObject.Find("StoreScene_Mng").GetComponent<MyUnit>();
        unitlist = GetComponent<UnitList>();
        //int[] array = myunit.GetKey();
        string path = Application.dataPath + "/Myunit2.json";
        string json = File.ReadAllText(path);
        test Test1 = JsonUtility.FromJson<test>(json);
        int[] arr = new int[100];
        newunit = Test1.Printnum();
        Debug.Log(unitlist.FindDic(newunit[0]).Name);
        Debug.Log(unitlist.FindDic(newunit[0]).ID);
        foreach (int number in newunit)
        {
            Debug.Log(number);
            if (number == 0)
            {
                break;
            }
            unitarray[i] = number;
            i++;
            units.ID = unitlist.FindDic(number).ID;
            units.Name = unitlist.FindDic(number).Name;
            units.Hp = unitlist.FindDic(number).Hp;
            units.Defence = unitlist.FindDic(number).Defence;
            units.Attack = unitlist.FindDic(number).Attack;
            units.AttackSpeed = unitlist.FindDic(number).AttackSpeed;
            units.Range = unitlist.FindDic(number).Range;
            units.MoveSpeed = unitlist.FindDic(number).MoveSpeed;
            units.Race = unitlist.FindDic(number).Race;
            units.Ability = unitlist.FindDic(number).Ability;
            units.AbilityDetail = unitlist.FindDic(number).AbilityDetail;
            units.Cost = unitlist.FindDic(number).Cost;
            units.Slot = unitlist.FindDic(number).Slot;
            units.Itemcode = unitlist.FindDic(number).Itemcode;
            MyUnitDic.Add(units.ID, units);
            MyUnitDic_Name.Add(units.Name, units);
        }

        FileStream stream = new FileStream(Application.dataPath + "/Myunit.json", FileMode.OpenOrCreate);
        test jsontest = new test();
        jsontest.Array = unitarray;
        string strjson = JsonConvert.SerializeObject(jsontest);
        byte[] data = Encoding.UTF8.GetBytes(strjson);
        stream.Write(data, 0, data.Length);
        File.WriteAllText(Application.dataPath + "/Myunit2.json", strjson);
        stream.Close();

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

    public Unit FindDic_name(string name)
    {
        Unit json;
        Debug.Log(name);
        if (MyUnitDic_Name.TryGetValue(name, out json))
        {
            return json;
        }
        Debug.Log("½ÇÆÐ");
        return null;
    }
    public int[] GetKey()
    {
        return unitarray;
    }
    public void AddUnit(int id)
    {
        Unit units = new Unit();
        unitarray[i] = id;
        i++;
        units.ID = unitlist.FindDic(id).ID;
        units.Name = unitlist.FindDic(id).Name;
        units.Hp = unitlist.FindDic(id).Hp;
        units.Defence = unitlist.FindDic(id).Defence;
        units.Attack = unitlist.FindDic(id).Attack;
        units.AttackSpeed = unitlist.FindDic(id).AttackSpeed;
        units.Range = unitlist.FindDic(id).Range;
        units.MoveSpeed = unitlist.FindDic(id).MoveSpeed;
        units.Race = unitlist.FindDic(id).Race;
        units.Ability = unitlist.FindDic(id).Ability;
        units.AbilityDetail = unitlist.FindDic(id).AbilityDetail;
        units.Cost = unitlist.FindDic(id).Cost;
        units.Slot = unitlist.FindDic(id).Slot;
        units.Itemcode = unitlist.FindDic(id).Itemcode;
        MyUnitDic.Add(units.ID, units);
        MyUnitDic_Name.Add(units.Name, units);


        FileStream stream = new FileStream(Application.dataPath + "/Myunit.json", FileMode.OpenOrCreate);
        test jsontest = new test();
        jsontest.Array = unitarray;
        string strjson = JsonConvert.SerializeObject(jsontest);
        byte[] data = Encoding.UTF8.GetBytes(strjson);
        stream.Write(data, 0, data.Length);
        File.WriteAllText(Application.dataPath + "/Myunit2.json", strjson);
        stream.Close();

    }
}
