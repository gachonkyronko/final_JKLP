using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
public class BossDamage : MonoBehaviour
{
    public Image Hpbar;
    private int CurHp = 0;
    private int MaxHp = 50;
    public static bool dieWin = false;
    internal float damageDelay = 1f;
    private float initialDamageDelay;
    [SerializeField] protected bool isDamage = false;
    private Text hpTxt;
     UnitList AllUnitList;
    ItemList AllitemID;
    MyunitList UseUnitList;
    private int EnemyUnitDamage = 0;
   
    private int EneymySumDagame = 0;
    public Text FinishTxt;
    public Canvas Finishcanvas;
     
     
    void Start()
    {
        initialDamageDelay = damageDelay;
        AllitemID = GetComponent<ItemList>();
        AllUnitList = GetComponent<UnitList>();
        UseUnitList = GetComponent<MyunitList>();
        Hpbar = GameObject.Find("Panel_Boss").transform.GetChild(1).GetComponent<Image>();
        hpTxt = GameObject.Find("Panel_Boss").transform.GetChild(0).GetComponent<Text>();
        Hpbar.color = Color.green;
        hpTxt.color = Color.black;
        CurHp = MaxHp;
        hpTxt.text = " Hp : " + CurHp.ToString();
      
        
    }
    //isTrigger 체크시 충돌 감지하는 콜백 함수  //통과 하면서 충돌 감지 한다.
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
        if (other.gameObject.tag == "Human" && !isDamage)
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


        CurHp -= EneymySumDagame;
            hpTxt.text = " Hp : " + CurHp.ToString();
            Hpbar.fillAmount = (float)CurHp / (float)MaxHp;

            if (Hpbar.fillAmount <= 0.0f)
                Hpbar.color = Color.clear;
            else if (Hpbar.fillAmount <= 0.3f)
                Hpbar.color = Color.red;
            else if (Hpbar.fillAmount <= 0.5f)
                Hpbar.color = Color.yellow;
            if (CurHp <= 0)
            {
                hpTxt.text = " Hp : 0";
                PlayerDie();
            }
                
        }

    }
    
    void PlayerDie()
    {
        dieWin = true;
        Debug.Log("승리!");
        Time.timeScale = 0.0f;
        Finishcanvas.gameObject.SetActive(true);
        FinishTxt.text = "승리!";
    }
   
}