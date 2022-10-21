using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class UnitList : MonoBehaviour
{
    TextAsset textData;
    UnitData unit;
    public static Dictionary<int, Unit> UnitDic = new Dictionary<int, Unit>();
    public int [] unitid = new int [100];
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
        int i = 0;
        foreach(Unit unit in unit.UNITS)
        {
            UnitDic.Add(unit.ID, unit);
        }    

        foreach(int unitID in UnitDic.Keys)
        {
            unitid[i] = unitID;
            
            i++;
        }

        foreach(KeyValuePair<int, Unit> unit in UnitDic)
        {
            
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Unit FindDic(int id)
    {
        Unit json;
        if (UnitDic.TryGetValue(id, out json))
        {
            return json;
        }
        return null;
    }
    public int[] GetKey()
    {
        return unitid;
    }
}
