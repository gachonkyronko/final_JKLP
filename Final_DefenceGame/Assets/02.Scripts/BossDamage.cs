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
    private int MaxHp = 1000;
    private int damage = 15;
    internal float damageDelay = 5f;
    private float initialDamageDelay;
    [SerializeField] protected bool isDamage = false;
    private Text hpTxt;
     UnitList AllUnitList;
    ItemList AllitemID;
    MyunitList UseUnitList;
    private int EnemyUnitDamage = 0;
    private int EnemyItemDamage = 0;
    private int EneymySumDagame = 0;
    private string EnemeyItemName = "";
    string enemyid = "";
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
        EnemyUnitDamage = UseUnitList.FindDic_name("Vampire").Attack;
        Debug.Log("������ �ް��ֽ��ϴ�." + EnemyUnitDamage);
    }
    //isTrigger üũ�� �浹 �����ϴ� �ݹ� �Լ�  //��� �ϸ鼭 �浹 ���� �Ѵ�.
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
            var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
            string name = other.transform.root.name;
            int cutClone = name.IndexOf("(Clone)");
            string Cutname = name.Substring(0,cutClone);
             
            PlayFabClientAPI.GetUserData(request1, (result) => { EnemeyItemName= result.Data[Cutname].Value;
                 EnemyDamage(Cutname); },
                (error) => print("������������������"));
            int k = 0;
            k = int.Parse(EnemeyItemName);
            EnemyItemDamage = AllitemID.FindDic(k).Attack;
            EneymySumDagame = EnemyUnitDamage + EnemyItemDamage;
            Debug.Log("���յ�����, �����۵�����, ���ֵ�����" + EneymySumDagame + "," + EnemyItemDamage + "," + EnemyUnitDamage);
            CurHp -= damage;
            hpTxt.text = " Hp : " + CurHp.ToString();
            Hpbar.fillAmount = (float)CurHp / (float)MaxHp;

            if (Hpbar.fillAmount <= 0.0f)
                Hpbar.color = Color.clear;
            else if (Hpbar.fillAmount <= 0.3f)
                Hpbar.color = Color.red;
            else if (Hpbar.fillAmount <= 0.5f)
                Hpbar.color = Color.yellow;
            if (CurHp <= 0)
                hpTxt.text = " Hp : 0";
                PlayerDie();
        }

    }
    void EnemyDamage(string enemyname)
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Main" }, (result) =>
        {
            int k = 0;
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < result.Catalog.Count; i++)
                {
                    if (result.Catalog[i].DisplayName == enemyname)
                    {
                        k = i;
                        Debug.Log("k������!" + k);


                        enemyid = result.Catalog[k].Tags[0];
                        Debug.Log(enemyid);
                        EnemyUnitDamage =   AllUnitList.FindDic(int.Parse(enemyid)).Attack;
                        Debug.Log(EnemyUnitDamage);


                    }


                }
            }



        },
    (error) => print("����"));
    }
    void PlayerDie()
    {
        Debug.Log("�¸�!");
    }
}