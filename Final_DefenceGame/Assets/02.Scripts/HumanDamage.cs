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
    internal float damageDelay = 2f;
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
            string Cutname;
            if (name=="fancy_tower")
            {
                Cutname = "fancy_tower";
            }
            else
            {
                int cutClone = name.IndexOf("(Clone)");
                Cutname = name.Substring(0, cutClone);
             

                for (int i = 0; i < getdamage.realLen; i++)
                {
                   
                    
                    if (getdamage.humanName[i] == Cutname)

                    {
                        EneymySumDagame = int.Parse(getdamage.humanattack[i]);
                        
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
                    Debug.Log("????????");
                    Destroy(gameObject);
                }
            }
            
            
        }

    }

   

    public void mystat()
    {

        int cutClone = name.IndexOf("(Clone)");
        string Cutname = name.Substring(0, cutClone);
       
        for (int i = 0; i < getdamage.realLen; i++)
        {
            if (getdamage.enemyName[i] == Cutname)
            {
               
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
