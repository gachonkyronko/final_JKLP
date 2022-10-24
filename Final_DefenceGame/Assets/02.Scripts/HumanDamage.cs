using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDamage : MonoBehaviour
{
    public int myCurHp = 0;
    public int myMaxHp = 0;
    internal float damageDelay = 5f;
    private float initialDamageDelay;
    UnitList AllUnitList;
    MyunitList UseUnitList;
    [SerializeField] protected bool isDamage = false;
    void Start()
    {
        initialDamageDelay = damageDelay;
         
        AllUnitList = GetComponent<UnitList>();
        UseUnitList = GetComponent<MyunitList>();

        //MyFindDic(Randomunit[t]).Name;
        myMaxHp = UseUnitList.FindDic_name("Vampire").ID;

        Debug.Log("소환됐음 내 hp는 ? " + myMaxHp);
        myCurHp = myMaxHp;
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
        if (other.gameObject.tag == "Humanweapon" && !isDamage)
        {
            isDamage = true;
            string name = other.gameObject.name;
            Debug.Log(name + "와 충돌중입니다");
            //CurHp -= damage;
            //hpTxt.text = " Hp : " + CurHp.ToString();
            //Hpbar.fillAmount = (float)CurHp / (float)MaxHp;

            //if (Hpbar.fillAmount <= 0.0f)
            //    Hpbar.color = Color.clear;
            //else if (Hpbar.fillAmount <= 0.3f)
            //    Hpbar.color = Color.red;
            //else if (Hpbar.fillAmount <= 0.5f)
            //    Hpbar.color = Color.yellow;
            //if (CurHp <= 0)
            //    hpTxt.text = " Hp : 0";
            //PlayerDie();
        }

    }
     
   
}
