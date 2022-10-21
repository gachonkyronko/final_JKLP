using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class StoreScene_Mng : MonoBehaviour
{
    public Text MyMoneyTxt;
    public int MyMoney = 0;
    public string[] unit_1 = new string [5]; //아이템 아이디를   배열로 가져오려고 만듬
    ItemList AllitemID;
    Transform[] Buttons;
    int[] Itemnumber = new int[100];
     
    int[] item = new int[5];
    //유닛테스트
    public int[] Allunit = new int[100];
    public int[] Useunit = new int[100];
    public int[] Randomunit = new int[5];
    string[] a = new string[5];
    UnitList AllUnitList;
    MyunitList UseUnitList;
    void Start() //상점정보불러오기
    {
        
        AllitemID = GameObject.Find("StoreScene_Mng").GetComponent<ItemList>();
        AllUnitList = GameObject.Find("StoreScene_Mng").GetComponent<UnitList>();
        UseUnitList = GameObject.Find("StoreScene_Mng").GetComponent<MyunitList>();
        int i = 0;
        int j = 0;  
        int n = 0;
        int m = 0;
        Itemnumber = AllitemID.GetKey();
        Allunit = AllUnitList.GetKey();
        Useunit = UseUnitList.GetKey();
        foreach (int number in Itemnumber)
        {
            
            if (number == 0)
            {
                
                break;
            }
            i++;

        }
        for( j=0;j<5;j++)
        {
            item[j] = UnityEngine.Random.Range(1, i);
            print("아이템아이디 : "+item[j]);
        }

        foreach (int number in Allunit)
        {
            if (number == 0)
            {
                 
                break;

            }
            n++;
        }

        foreach (int number in Useunit)
        {
            if (number == 0)
            {
                
                break;

            }
            m++;
        }
         
        for (int k = 0; k < 5; k++)
        {
            
            int rootnum = 0;
            int p;
            while (true)
            {
                rootnum++;
                 
                p = UnityEngine.Random.Range(0, n);
                int number = Allunit[p];
                var check = Array.Exists(Useunit, x => x.Equals(number));

                if (check == false)
                {
                    Randomunit[k] = number;
                     
                        break;

                }
                else
                {
                    if (rootnum > 10000)

                        break;
                }
            }
        }
        
        for (int t = 0; t < 5; t++)
        {

            a[t] = AllUnitList.FindDic(Randomunit[t]).Name;
            print("유닛 아이디 : " + Randomunit[t]);
             
        }

        
        Buttons = GameObject.Find("PurchaseItem").GetComponentsInChildren<Transform>();
        var requset = new GetCatalogItemsRequest { CatalogVersion = "Main" };
        PlayFabClientAPI.GetCatalogItems(requset, GetSuccess, GetFail);
        MyMoneyTxt = GameObject.Find("Canvas").transform.GetChild(9).GetComponent<Text>();
    }
    
    private void GetFail(PlayFabError obj)
    {
        Debug.Log("카탈로그 불러오기 실패");
    }
    //카탈로그를 불러오는데 성공했다면 콜백이 된다.
    private void GetSuccess(GetCatalogItemsResult obj) //상점정보불러오면 보유골드량표시
    {
        Debug.Log("칼탈로그 불러오기 성공");
        var items = obj.Catalog;
        //배열로 아이템아이디가져오려고 만듬
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log("아이템 아이디 =" + items[i].ItemId);
            unit_1[i] = items[i].ItemId;
        }
        //골드량표시
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
        {
            print("현재금액 : " + result.VirtualCurrency["GD"]);
            MyMoneyTxt.text = "보유골드량 : " + result.VirtualCurrency["GD"];
        },
  (error) => print("현재골드량 불러오기 실패"));
    }
    

    public void AddMoney() //돈추가시 획득 + 보유텍스트내용변경
    {
        var request = new AddUserVirtualCurrencyRequest() { VirtualCurrency = "GD", Amount = 100 };
        PlayFabClientAPI.AddUserVirtualCurrency(request, (result) => { print("돈 얻기 성공! 현재 돈 : " + result.Balance);  MyMoneyTxt.text = "보유골드량 : " + result.Balance; }, (error) => print("돈 얻기 실패"));
    }
    public void PurchaseUnit1()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unit_1[0], VirtualCurrency = "GD", Price = 100 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("유닛 구입 성공!"), (error) => print("유닛 구입 실패"));
        var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unit_1[0], "Tiger" } } };
        PlayFabClientAPI.UpdateUserData(request2, (result) => { print("성공"); }, (error) => print("실패"));
    }
    public void PurchaseUnit2()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unit_1[1], VirtualCurrency = "GD", Price = 100 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("유닛 구입 성공!"), (error) => print("유닛 구입 실패"));
        var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unit_1[1], "Vampire" } } };
        PlayFabClientAPI.UpdateUserData(request2, (result) => { print("성공"); }, (error) => print("실패"));
    }
    public void PurchaseUnit3()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unit_1[2], VirtualCurrency = "GD", Price = 100 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("유닛 구입 성공!"), (error) => print("유닛 구입 실패"));
        var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unit_1[2], "Tiger" } } };
        PlayFabClientAPI.UpdateUserData(request2, (result) => { print("성공"); }, (error) => print("실패"));
    }
    public void PurchaseUnit4()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unit_1[3], VirtualCurrency = "GD", Price = 100 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("유닛 구입 성공!"), (error) => print("유닛 구입 실패"));
        var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unit_1[3], "Vampire"} } };
        PlayFabClientAPI.UpdateUserData(request2, (result) => { print("성공"); }, (error) => print("실패"));
    }
    //사용용도없어서 일단 주석
    //public void ConsumeItem()
    //{
    //    var request = new ConsumeItemRequest() { ConsumeCount = 1, ItemInstanceId = "E01A5C5EDD04BE06" };
    //    PlayFabClientAPI.ConsumeItem(request, (result) => print("유닛 사용 성공!"), (error) => print("유닛 사용 실패"));
    //}
    public void OnBackbuttonClick() //뒤로가기 이벤트
    {
        SceneManager.LoadScene("MainHome_Scene");
    }
   
void Update()
    {
    
     
        
    }
}
