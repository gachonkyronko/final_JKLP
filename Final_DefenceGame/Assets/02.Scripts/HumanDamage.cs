using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
public class HumanDamage : MonoBehaviour
{
    public int HP = 0;
    public int DF = 0;
    public int ATT = 0;
    public double ATTSPD = 0;
    public int RANGE = 0;
    public double MOVESPD = 0;
    public int RACE = 0;
    public int ABILITY = 0;
    public int ABILITYDETAILL = 0;
    public int COST = 0;
   
    private int EneymySumDagame = 0;
    internal float damageDelay = 5f;
    private float initialDamageDelay;
    UnitList AllUnitList;
    MyunitList UseUnitList;
    [SerializeField] protected bool isDamage = false;
    public static bool myunitdie = false;

    
    void Start()
    {
        initialDamageDelay = damageDelay;
         
        AllUnitList = GetComponent<UnitList>();
        UseUnitList = GetComponent<MyunitList>();
        Invoke("mystat",0.5f);

    }
    private void Update()
    {
        DamageDelay();
    }
    protected void DamageDelay()
    {
        if (isDamage && damageDelay > 0)
        {
            damageDelay -= Time.deltaTime;
            if (damageDelay <= 0)
            {
                isDamage = false;
                damageDelay = initialDamageDelay;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !isDamage)
        {
            isDamage = true;
            var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
            string name = other.transform.root.name;

            int cutClone = name.IndexOf("(Clone)");
            string Cutname = name.Substring(0, cutClone);
            Debug.Log("충돌유닛이름 : " + Cutname);

            for (int i = 0; i < getdamage.realLen; i++)
            {
                Debug.Log("대조하는유닛이름 : " + getdamage.enemyName[i]);
                int j = 0;
                if (getdamage.enemyName[i] == Cutname)

                {
                    EneymySumDagame = getdamage.sumDamage[i];
                    Debug.Log("총합데미지,  유닛데미지" + EneymySumDagame + "," + getdamage.enemyattack[i]);
                    break;

                }

            }

            if (DF > EneymySumDagame)
                DF = DF - EneymySumDagame;
            else
            {
                EneymySumDagame = EneymySumDagame - DF;
                HP = HP - EneymySumDagame;

            }
            if (HP < 0)
            {
                Debug.Log("유닛사망");
                Destroy(gameObject);
            }
        }

    }

   

    public void mystat()
    {

        int cutClone = name.IndexOf("(Clone)");
        string Cutname = name.Substring(0, cutClone);
        Debug.Log("이 유닛의 이름 : " + Cutname);
        for (int i = 0; i < getdamage.realLen; i++)
        {
            if (getdamage.enemyName[i] == Cutname)
            {
                Debug.Log("테스트" + i);
                Debug.Log("테스트" + getdamage.enemyName[i]);
                Debug.Log("테스트" + getdamage.enemyattack[i]);
                HP = int.Parse(getdamage.enemyHP[i]);
                DF = int.Parse(getdamage.enemyDF[i]);
                ATT = int.Parse(getdamage.enemyattack[i]);
                ATTSPD = double.Parse(getdamage.enemyattackspeed[i]);
                RANGE = int.Parse(getdamage.enemyattackrange[i]);
                MOVESPD = double.Parse(getdamage.enemymovepseed[i]);
                
                break;


            }
        }
       

    }
}
