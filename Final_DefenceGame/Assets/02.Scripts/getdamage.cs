using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
public class getdamage : MonoBehaviour
{
    public static string[] enemyHP = new string[50];
    public static string[] enemyDF = new string[50];
    public static string[] enemyattack = new string[50];
    public static string[] enemyattackspeed = new string[50];
    public static string[] enemyattackrange = new string[50];
    public static string[] enemymovepseed = new string[50];
    public static string[] enemyRace = new string[50];
    public static string[] enemyAbility = new string[50];
    public static string[] enemymAbilityDetail = new string[50];
    public static string[] enemyCost= new string[50];
     
     
     
    public static string[] enemySaveName = new string[50];
    public static string[] enemyName = new string[50];
    public static string[] enemyitemekey = new string[50];
    public static string[] enemyitemAtt = new string[50];
    public static string[] unitstatsdata = new string[50];
    public static string[] unitstatsdatatag = new string[50];

    public static int[] sumDamage = new int[50];
    string[] myUnitInven = new string[100];
    public static int realLen = 0;
    public static string[] itemname = new string[50];
    public static string[] itemHP = new string[50];
    public static string[] itemDF = new string[50];
    public static string[] itemAttack = new string[50];
    public static string[] itemAttspd = new string[50];
    public static string[] itemRange = new string[50];
    public static string[] itemMovespd = new string[50];
    public static string[] itemGrade = new string[50];
    public static string[] itemUseStack = new string[50];
    int itemlen = 0;
    int unitlen = 0;
   
    void Start()
    {

        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
        {
            for (int i = 0; i < result.Inventory.Count; i++)
            {

                var Inven = result.Inventory[i];
                if (result.Inventory[i].ItemClass == "Unit")
                {
                    myUnitInven[i] = Inven.DisplayName;
                }
                else
                {
                    myUnitInven[i] = "0";
                }

            }


            Debug.Log("체크1");
            int f = 0;
            for (int i = 0; i < 20; i++)
            {
                Debug.Log("체크2");
                for (int k = f; k < result.Inventory.Count; k++)
                {
                    Debug.Log("체크3");

                    Debug.Log(k);

                    if (myUnitInven[k] != "0")
                    {
                        Debug.Log(myUnitInven[k]);

                        enemyName[i] = myUnitInven[k];
                        f++;


                        Debug.Log("체크4");
                        break;

                    }

                }


            }




        },

        (error) => print("인벤토리 불러오기 실패"));
        Debug.Log("유닛정보받기완료, 적용시작");

    }

    // Update is called once per frame
    public void upload()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Main" }, (result) =>
        {
            int k = 0;
            Debug.Log(unitstatsdata.Length);
            for (int j = 0; j < unitstatsdata.Length; j++)
            {
                for (int i = 0; i < result.Catalog.Count; i++)
                {
                    if (result.Catalog[i].DisplayName == enemyName[j])
                    {

                        k = i;
                        Debug.Log("------");
                        Debug.Log(enemyName[j]);

                        enemySaveName[j] = result.Catalog[k].DisplayName;
                        unitstatsdatatag[j] = result.Catalog[k].Tags[0];
                         enemyHP[j]= result.Catalog[k].Tags[1];
                        enemyDF[j] = result.Catalog[k].Tags[2];
                        enemyattack[j] = result.Catalog[k].Tags[3];
                        enemyattackspeed[j] = result.Catalog[k].Tags[4];
                        enemyattackrange[j] = result.Catalog[k].Tags[5];
                        enemymovepseed[j] = result.Catalog[k].Tags[6];
                        enemyRace[j] = result.Catalog[k].Tags[7];
                        enemyAbility[j] = result.Catalog[k].Tags[8];
                        enemymAbilityDetail[j] = result.Catalog[k].Tags[9];
                        enemyCost[j] = result.Catalog[k].Tags[10];


                            
                        break;
                    }


                }
            }



        },
   (error) => print("실패"));
        unitlen = enemyName.Length;
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Sub" }, (result) =>
        {
            itemlen = result.Catalog.Count;
            for (int i = 0; i < result.Catalog.Count; i++)
            {

                itemname[i] = result.Catalog[i].Tags[0];
                itemHP[i] = result.Catalog[i].Tags[1];
                itemDF[i] = result.Catalog[i].Tags[2];
                itemAttack[i] = result.Catalog[i].Tags[3];
                itemAttspd[i] = result.Catalog[i].Tags[4];
                itemRange[i] = result.Catalog[i].Tags[5];
                itemMovespd[i] = result.Catalog[i].Tags[6];
                itemGrade[i] = result.Catalog[i].Tags[7];
                itemUseStack[i] = result.Catalog[i].Tags[8];
                Debug.Log("아이템정보받아옴");
                Debug.Log("아이템키값" + itemname[i]);
            }


        },
  (error) => print("실패"));
        matchitem();
    }

    public void matchitem()
    {
         
        for (int l = 0; l < unitlen; l++)
        {
            if (enemyName[l] != null)
            {
                realLen++;
            }
            else
                break;
        }

        Debug.Log("길이 : " + realLen);
            var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
        PlayFabClientAPI.GetUserData(request1, (result) =>
        {
            Debug.Log("로그인성공"); int y = 0;
            for (int l = 0; l < realLen; l++)
            {
                y = l;

                Debug.Log("값부터확인" + enemyName[y]);
                Debug.Log("값부터확인" + result.Data[enemyName[y]].Value);
                enemyitemekey[y] = result.Data[enemyName[y]].Value;
                Debug.Log("값부터확인" + enemyitemekey[y]);
            }
            for (int i = 0; i < realLen; i++)
            {
                for (int j = 0; j < itemlen; j++)
                {
                    if (enemyitemekey[i] == itemname[j])
                    {
                        enemyitemAtt[i] = itemAttack[j];
                    }
                }

            }
            Debug.Log("아이템공격력 적용");
            for (int i = 0; i < realLen; i++)
            {
                sumDamage[i] = int.Parse(enemyattack[i]) + int.Parse(enemyitemAtt[i]);
                Debug.Log("총 데미지" + sumDamage[i]);
            }
            
        },
              (error) => print("아이템정보못가져옴"));
         
    }
     
}




 

