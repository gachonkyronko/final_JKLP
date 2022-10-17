using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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