using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
public class getdamage : MonoBehaviour
{
    public string[] enemyattack = new string[50];
    public string[] enemyattackspeed = new string[50];
    public string[] enemyattackrange = new string[50];
    public string[] enemymovepseed = new string[50];
    public static string[] unitstatsdata = new string[50];
    public static string[] unitstatsdatatag = new string[50];
    string[] myUnitInven = new string[100];
    string[] savedata = new string[50];
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

                       savedata[i] = myUnitInven[k];
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
                    if (result.Catalog[i].DisplayName == savedata[j])
                    {

                        k = i;
                        Debug.Log("------");
                        Debug.Log(savedata[j]);
                        unitstatsdatatag[j] = result.Catalog[k].Tags[0];
                        Debug.Log(j);
                        Debug.Log(k);
                        Debug.Log(result.Catalog[k].Tags[1]);
                        Debug.Log(enemyattack[0]);
                        enemyattack[j] = result.Catalog[k].Tags[1];
                        enemyattackspeed[j] = result.Catalog[k].Tags[2];
                        enemyattackrange[j] = result.Catalog[k].Tags[3];
                        enemymovepseed[j] = result.Catalog[k].Tags[4];
                        Debug.Log("------");
                        Debug.Log(unitstatsdatatag[j]);
                        Debug.Log(enemyattack[j]);
                        Debug.Log(enemyattackspeed[j]);
                        Debug.Log(enemyattackrange[j]);
                        Debug.Log(enemymovepseed[j]);

                        break;
                    }


                }
            }



        },
   (error) => print("실패"));
    }
}
