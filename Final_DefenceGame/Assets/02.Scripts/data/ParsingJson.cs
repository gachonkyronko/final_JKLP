using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
public class ParsingJson : MonoBehaviour
{
    [Serializable]
    public class Stats
    {
        public int ID;
        public string name;
        public int Hp;
        public int Defence;
        public int Attack;
        public double AttackSpeed;
        public double Range;
        public double MoveSpeed;
        public int Race;
        public int Ability;
        public int AbilityDetail;
        public int Cost;

        public void Print()
        {
            Debug.Log(ID);
            Debug.Log(name);
            Debug.Log(Hp);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Assets/Resources/Json/UnitData.json");
        Debug.Log(textAsset.text);

        Stats Unit = JsonUtility.FromJson<Stats>(textAsset.text);
        Unit.Print();

        string classToJson = JsonUtility.ToJson(Unit);
        Debug.Log(classToJson);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
