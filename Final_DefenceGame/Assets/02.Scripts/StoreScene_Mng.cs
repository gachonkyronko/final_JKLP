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
    UnitList AllUnitList;
    MyunitList UseUnitList;
    void Start() //상점정보불러오기
    {
        int i = 0;
        int j = 0;
        AllitemID = GameObject.Find("StoreScene_Mng").GetComponent<ItemList>();
        Buttons = GameObject.Find("PurchaseItem").GetComponentsInChildren<Transform>();
        Itemnumber = AllitemID.GetKey();
        foreach (int number in Itemnumber)
        {
            if (number == 0)
                break;
            i++;
        }
        for( j=0;j<5;j++)
        {
            item[j] = UnityEngine.Random.Range(1, i); 
        }
        
        print("아이템닉네임 : " +AllitemID.FindDic(item[0]).Name);
        print("아이템코드:"+item[0]);


        var requset = new GetCatalogItemsRequest { CatalogVersion = "Main" };
        PlayFabClientAPI.GetCatalogItems(requset, GetSuccess, GetFail);
        MyMoneyTxt = GameObject.Find("Canvas").transform.GetChild(9).GetComponent<Text>();


        //유닛테스트
         
        //AllUnitList = GameObject.Find("StoreScene_Mng").GetComponent<UnitList>();
        //UseUnitList = GameObject.Find("StoreScene_Mng").GetComponent<MyunitList>();
        //Allunit = AllUnitList.GetKey();
        //Useunit = UseUnitList.GetKey();

        //foreach (int number in Allunit)
        //{
        //    if (number == 0)
        //    {
        //        i++;
        //        break;
                
        //    }
        //}

        //foreach (int number in Useunit)
        //{
        //    if (number == 0)
        //    {
        //        j++;
        //        break;
                
        //    }
        //}

        //for (int k = 0; k < 5; k++)
        //{
        //    while (true)
        //    {
        //        int l = UnityEngine.Random.Range(0, i);
        //        int number = Allunit[l];
        //        var check = Array.Exists(Useunit, x => x == number);
        //        if (check == true)
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            Randomunit[k] = number;
        //            Debug.Log("유닛id"+number);
        //            break;
        //        }
        //    }
        //}
        //for (int m = 0; m < 5; m++)
        //{
        //    string a = AllUnitList.FindDic(Randomunit[m]).Name;
        //    Debug.Log("유닛이름"+a);
        //}

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
