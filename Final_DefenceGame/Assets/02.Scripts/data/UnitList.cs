using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class UnitList : MonoBehaviour
{
    TextAsset textData;
    UnitData unit;
    public static Dictionary<int, Unit> UnitDic = new Dictionary<int, Unit>();

    [System.Serializable]
    public class Unit : Stats
    {

    }

    [System.Serializable]
    public class UnitData
    {
        public Unit[] UNITS;
    }


    // Start is called before the first frame update
    void Start()
    {
        textData = Resources.Load("UnitData") as TextAsset;
        unit = JsonUtility.FromJson<UnitData>(textData.ToString());

        foreach(Unit unit in unit.UNITS)
        {
            UnitDic.Add(unit.ID, unit);
        }    

        foreach(int unitID in UnitDic.Keys)
        {
            UnitDic[unitID].Print();
        }

        foreach(KeyValuePair<int, Unit> unit in UnitDic)
        {
            Debug.Log(unit.Key);
            unit.Value.Print();
            Debug.Log("=====");
        }
        Debug.Log(UnitDic[1001].Attack);
        int id = 1001;
        
        UnitDic.Add(id+10000, UnitDic[id]);
        UnitDic[id + 10000].Print();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
