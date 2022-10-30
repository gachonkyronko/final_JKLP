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

    public static string[] humanName = new string[50];
    public static string[] humanHP = new string[50];
    public static string[] humanDF = new string[50];
    public static string[] humanattack = new string[50];
    public static string[] humanattackspeed = new string[50];
    public static string[] humanattackrange = new string[50];
    public static string[] humanymovepseed = new string[50];
    public static string[] humanRace = new string[50];
    public static string[] humanAbility = new string[50];
    public static string[] humanAbilityDetail = new string[50];
    public static string[] humanCost = new string[50];



    public static string[] enemySaveName = new string[50];
    public static string[] enemyName = new string[50];
    public static string[] enemyitemekey = new string[50];
    public static string[] enemyitemAtt = new string[50];
    public static string[] unitstatsdata = new string[50];
    public static string[] unitstatsdatatag = new string[50];

    public static int[] sumDamage = new int[50];
    public static int[] humanSumDamage = new int[50];
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
    
    public static int humanlen = 0;
   
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


          
            int f = 0;
            for (int i = 0; i < 20; i++)
            {
                
                for (int k = f; k < result.Inventory.Count; k++)
                {
                   

                    if (myUnitInven[k] != "0")
                    {
 

                        enemyName[i] = myUnitInven[k];
                        f++;


                    
                        break;

                    }

                }


            }




        },

        (error) => print("인벤토리 불러오기 실패"));
       
        Invoke("upload", 1.0f);
        Invoke("matchitem", 1.5f);
    }

    // Update is called once per frame
    public void upload()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Main" }, (result) =>
        {
            int k = 0;
           
            for (int j = 0; j < unitstatsdata.Length; j++)
            {
                for (int i = 0; i < result.Catalog.Count; i++)
                {
                    if (result.Catalog[i].DisplayName == enemyName[j])
                    {

                        k = i;
                        

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
              
            }


        },
  (error) => print("실패"));

        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Enemy" }, (result) =>
        {
            humanlen = result.Catalog.Count;
            for (int j = 0; j< result.Catalog.Count; j++)
            {
                 
                 

                humanName[j] = result.Catalog[j].ItemId;
                
                humanHP[j] = result.Catalog[j].Tags[1];
                humanDF[j] = result.Catalog[j].Tags[2];
                humanattack[j] = result.Catalog[j].Tags[3];
                humanattackspeed[j] = result.Catalog[j].Tags[4];
                humanattackrange[j] = result.Catalog[j].Tags[5];
                humanymovepseed[j] = result.Catalog[j].Tags[6];
                humanRace[j] = result.Catalog[j].Tags[7];
                humanAbility[j] = result.Catalog[j].Tags[8];
                humanAbilityDetail[j] = result.Catalog[j].Tags[9];
                humanCost[j] = result.Catalog[j].Tags[10];

            }


        },
 (error) => print("실패"));

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

       
            var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
        PlayFabClientAPI.GetUserData(request1, (result) =>
        {
              int y = 0;
            for (int l = 0; l < realLen; l++)
            {
                y = l;

              
                enemyitemekey[y] = result.Data[enemyName[y]].Value;
                
            }
            for (int i = 0; i < realLen; i++)
            {
                for (int j = 0; j < itemlen; j++)
                {
                    if (enemyitemekey[i] == itemname[j])
                    {
                        enemyitemAtt[i] = itemAttack[j];
                        Debug.Log("오류검사" + enemyitemAtt[i]);
                    }
                    else
                    {
                        enemyitemAtt[i] = "0";
                    }
                }
                if(i==realLen-1)
                {
                    Debug.Log("오류검사" + realLen);
                    for (int z = 0; z < realLen; z++)
                    {
                        Debug.Log("오류검사중 : " + int.Parse(enemyattack[z]));
                        Debug.Log("오류검사중 : " + int.Parse(enemyitemAtt[z]));
                        sumDamage[z] = int.Parse(enemyattack[z]) + int.Parse(enemyitemAtt[z]);
                        Debug.Log("오류검사중 : " + sumDamage[z] + enemyName[z]);

                    }
                }

            }
          
            
            
        },
              (error) => print("아이템정보못가져옴"));
         
    }
     
}




 

