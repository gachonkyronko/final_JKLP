using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MyUnit : MonoBehaviour
{
    TextAsset textData;
    UnitData_Human human;
    UnitData_Elf elf;
    UnitData_Druid druid;
    UnitData_Undead undead;
    public string path;
    public int[] Array = new int[10];
 
    Dictionary<int, Unit> MyUnitDic = new Dictionary<int, Unit>();
    public static Dictionary<int, Unit> UnitDic = new Dictionary<int, Unit>();
    [System.Serializable]
    public class Unit : Stats
    {

    }


    [System.Serializable]
    public class UnitData_Human
    {
        public Unit[] Human;
    }
    [System.Serializable]
    public class UnitData_Elf
    {
        public Unit[] Elf;
    }
    [System.Serializable]
    public class UnitData_Druid
    {
        public Unit[] Druid;
    }
    [System.Serializable]
    public class UnitData_Undead
    {
        public Unit[] Undead;
    }
    public class test
    {
        public int[] Array = new int [100];
    }
    // Start is called before the first frame update
    void Awake()
    {
        
        int i = 0;
        int rage = Random.Range(1, 4);
        textData = Resources.Load("UnitData") as TextAsset;
        switch (rage)
        {

            case 1:

                human = JsonUtility.FromJson<UnitData_Human>(textData.ToString());

                foreach (Unit unit in human.Human)
                {
                    MyUnitDic.Add(unit.ID, unit);
                }
                foreach (int unitID in MyUnitDic.Keys)
                {

                    Array[i] = unitID;
                    i++;
                }

                foreach (KeyValuePair<int, Unit> unit in MyUnitDic)
                {

                }
                break;

            case 2:
                elf = JsonUtility.FromJson<UnitData_Elf>(textData.ToString());

                foreach (Unit unit in elf.Elf)
                {
                    MyUnitDic.Add(unit.ID, unit);
                }
                foreach (int unitID in MyUnitDic.Keys)
                {

                    Array[i] = unitID;
                    i++;
                }

                foreach (KeyValuePair<int, Unit> unit in MyUnitDic)
                {

                }
                break;

            case 3:
                druid = JsonUtility.FromJson<UnitData_Druid>(textData.ToString());

                foreach (Unit unit in druid.Druid)
                {
                    MyUnitDic.Add(unit.ID, unit);
                }
                foreach (int unitID in MyUnitDic.Keys)
                {

                    Array[i] = unitID;
                    i++;
                }

                foreach (KeyValuePair<int, Unit> unit in MyUnitDic)
                {

                }
                break;

            case 4:
                undead = JsonUtility.FromJson<UnitData_Undead>(textData.ToString());

                foreach (Unit unit in undead.Undead)
                {
                    MyUnitDic.Add(unit.ID, unit);
                }
                foreach (int unitID in MyUnitDic.Keys)
                {

                    Array[i] = unitID;
                    i++;
                }

                foreach (KeyValuePair<int, Unit> unit in MyUnitDic)
                {

                }
                break;

        }

        FileStream stream = new FileStream(Application.dataPath + "/Myunit.json", FileMode.OpenOrCreate);
        test jsontest = new test();
        jsontest.Array = Array;
        string strjson = JsonConvert.SerializeObject(jsontest);
        Debug.Log("-------------------");
        Debug.Log(strjson);
        Debug.Log("Json????");
        byte[] data = Encoding.UTF8.GetBytes(strjson);
        stream.Write(data, 0, data.Length);
        File.WriteAllText(Application.dataPath + "/test2.json", strjson);
        stream.Close();
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public Unit FindDic(int id)
    {
        Unit json;
        if(MyUnitDic.TryGetValue(id, out json))
        {
            return json;
        }
        return null;
    }
     

    public int [] GetKey()
    {
        return Array;
    }
    public Unit MyFindDic(int id)
    {
        Unit json;
        if (UnitDic.TryGetValue(id, out json))
        {
            return json;
        }
        return null;
    }

    internal string FindFirstKeyByValue(string clickname)
    {
        throw new System.NotImplementedException();
    }
}