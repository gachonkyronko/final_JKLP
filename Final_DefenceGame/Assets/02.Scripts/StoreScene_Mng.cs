using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;
public class StoreScene_Mng : MonoBehaviour
{
    public Text MyMoneyTxt;
    public Text clickTxt;
    public int MyMoney = 0;
    public string[] unit_1 = new string [5]; //아이템 아이디를   배열로 가져오려고 만듬
    public string[] unitname = new string[5];
    public string[] itemname = new string[5];
    public int[] saveunitcost = new int[5];
    public int[] saveitemcost = new int[5];
    public string[] saveunitcost_1 = new string[5];
    ItemList AllitemID;
    public Button ClickUnit;
    public  Transform[] UnitButtons;
    int[] Itemnumber = new int[100];
    public Button[] unitPurchaseBtn = new Button[5];
    public Button[] itemPurchaseBtn = new Button[5];
    public Button[] myunit_invenBtn = new Button[6];
    public Button[] inven_myitem = new Button[6];
    public Text[] unitCost = new Text[5];
    public Text[] ItemCost = new Text[5];
    int[] item = new int[5];
    //유닛테스트
    public int[] Allunit = new int[100];
    public int[] Useunit = new int[100];
    public int[] Randomunit = new int[5];
    string[] a = new string[5] { "", "", "", "", "" };
    string[] catalog = new string[100];
    int[] b = new int[5] {0 ,0,0,0,0};
    public string[] myunit_inven = new string[6];
    public
    int o = 0;
    int g = 0;
    string[] myUnitInven = new string[10000];
    string[] myItemInven = new string[10000];
    MyUnit AllUnitList;
    
    MyunitList UseUnitList;

    

    void Start() //상점정보불러오기
    {

        myunit_invenBtn = GameObject.Find("Inventory").GetComponentsInChildren<Button>();
        inven_myitem = GameObject.Find("inven_myitem").GetComponentsInChildren<Button>();
        unitPurchaseBtn = GameObject.Find("PurchaseUnit").GetComponentsInChildren<Button>();
        itemPurchaseBtn = GameObject.Find("PurchaseItem").GetComponentsInChildren<Button>();
        unitCost = GameObject.Find("PurchaseUnitCost").GetComponentsInChildren<Text>();
        ItemCost = GameObject.Find("PurchaseItemCost").GetComponentsInChildren<Text>();
        AllitemID = GameObject.Find("StoreScene_Mng").GetComponent<ItemList>();
        AllUnitList = GameObject.Find("StoreScene_Mng").GetComponent<MyUnit>();
       
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
            
            itemPurchaseBtn[j].GetComponentInChildren<Text>().text = AllitemID.FindDic(item[j]).Name;
            ItemCost[j].text = (AllitemID.FindDic(item[j]).Grade * 100).ToString();
                  
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
            b[t] = AllUnitList.FindDic(Randomunit[t]).Cost;
          

        }
        Debug.Log("유닛아이디반환완료, 텍스트정보변환시작");
        for (int q = 0; q < 5; q++)
        {
            unitPurchaseBtn[q].GetComponentInChildren<Text>().text = a[q];
            unitCost[q].text = b[q].ToString();

        }

        Debug.Log("유닛아이디반환완료, 인벤토리내용반환중");
        
        
        var requset = new GetCatalogItemsRequest { CatalogVersion = "Main" };
        PlayFabClientAPI.GetCatalogItems(requset, GetSuccess, GetFail);
        MyMoneyTxt = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Text>();
        //  PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
        //  {

        //      for (int i = 0; i < result.Inventory.Count; i++)
        //      {
        //          var Inven = result.Inventory[i];
        //          myUnitInven[i] = Inven.DisplayName;

        //      }
        //      //for(int j=0;j<6;j++)
        //      //{
        //      //    int idx = UnityEngine.Random.Range(0, 6);
        //      //    myUnitInven[j] = myUnitInven[idx];
        //      //}
        //      for (int i = 0; i < 6; i++)
        //      {
        //          Debug.Log("출력값확인 : " + myUnitInven[i]);
        //          myunit_invenBtn[i].GetComponentInChildren<Text>().text = myUnitInven[i];

        //      }
        //  },

        //(error) => print("인벤토리 불러오기 실패"));
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
       {

           for (int i = 0; i < result.Inventory.Count; i++)
           {

               var Inven = result.Inventory[i];
               if(result.Inventory[i].ItemClass =="Unit")
               {
                   myUnitInven[i] = Inven.DisplayName;
               }
               else
               {
                   myUnitInven[i] = "0";
               }

           }
           for (int i = 0; i < 6; i++)
           {
               Debug.Log("체크0" + myUnitInven[i]);
                

           }
            
           Debug.Log("체크1");
           int f=0;
           for (int i = 0; i < 6; i++)
           {
               Debug.Log("체크2");
               for (int k = f;k< result.Inventory.Count;k++)
               {
                   Debug.Log("체크3");
                   Debug.Log(f);
                   Debug.Log(k);
                   if (myUnitInven[k] != "0")
                   {
                       Debug.Log(myUnitInven[k]);
                        
                       myunit_invenBtn[i].GetComponentInChildren<Text>().text = myUnitInven[k];
                       f = k+1;
                       Debug.Log("체크4");
                       break;

                   }
                    
               }
               

           }
           for (int i = 0; i < result.Inventory.Count; i++)
           {

               var Inven = result.Inventory[i];
               if (result.Inventory[i].ItemClass == "Item")
               {
                   myItemInven[i] = Inven.DisplayName;
               }
               else
               {
                   myItemInven[i] = "0";
               }

           }
           for (int i = 0; i < 6; i++)
           {
               Debug.Log("체크아이템리스트" + myItemInven[i]);


           }

           Debug.Log("체크아이템리스트1");
           int s = 0;
           for (int i = 0; i < 6; i++)
           {
               Debug.Log("체크아이템리스트2");
               for (int k = s; k < result.Inventory.Count; k++)
               {
                   Debug.Log("체크아이템리스트3");
                   Debug.Log(s);
                   Debug.Log(k);
                   if (myItemInven[k] != "0")
                   {


                       inven_myitem[i].GetComponentInChildren<Text>().text = myItemInven[k];
                       s = k + 1;
                       Debug.Log("체크아이템리스트4");
                       break;

                   }

               }


           }
       },

     (error) => print("인벤토리 불러오기 실패"));
        //인벤토리에서 클릭아이템업데이트
    //    PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
    //    {

    //        for (int i = 0; i < result.Inventory.Count; i++)
    //        {

    //            var Inven = result.Inventory[i];
    //            if (result.Inventory[i].ItemClass == "Item")
    //            {
    //                myItemInven[i] = Inven.DisplayName;
    //            }
    //            else
    //            {
    //                myItemInven[i] = "0";
    //            }

    //        }
    //        for (int i = 0; i < 6; i++)
    //        {
    //            Debug.Log("체크아이템리스트" + myItemInven[i]);


    //        }

    //        Debug.Log("체크아이템리스트1");
    //        int f = 0;
    //        for (int i = 0; i < 6; i++)
    //        {
    //            Debug.Log("체크아이템리스트2");
    //            for (int k = f; k < result.Inventory.Count; k++)
    //            {
    //                Debug.Log("체크아이템리스트3");
    //                Debug.Log(f);
    //                Debug.Log(k);
    //                if (myItemInven[k] != "0")
    //                {


    //                    inven_myitem[i].GetComponentInChildren<Text>().text = myItemInven[k];
    //                    f = k + 1;
    //                    Debug.Log("체크아이템리스트4");
    //                    break;

    //                }

    //            }


    //        }
    //    },

    //(error) => print("인벤토리 불러오기 실패"));

        Debug.Log("인벤토리반환성공");
    }
     
    private void GetFail(PlayFabError obj)
    {
        Debug.Log("카탈로그 불러오기 실패");
    }
    //카탈로그를 불러오는데 성공했다면 콜백이 된다.
    private void GetSuccess(GetCatalogItemsResult obj) //상점정보불러오면 보유골드량표시
    {
        Debug.Log("칼탈로그 불러오기 성공");
        
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
    public void updateInvenitem()
    {



        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
        {

            for (int i = 0; i < result.Inventory.Count; i++)
            {
                var Inven = result.Inventory[i];
                myUnitInven[i] = Inven.DisplayName;

            }
            for (int i = 0; i < 6; i++)
            {

                myunit_invenBtn[i].GetComponentInChildren<Text>().text = myUnitInven[i];

            }
        },

     (error) => print("인벤토리 불러오기 실패"));

    }
    public void updateClickInven()
    {
         
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
            {

                for (int i = 0; i < result.Inventory.Count; i++)
                {

                    var Inven = result.Inventory[i];
                    if (result.Inventory[i].ItemClass == "Item")
                    {
                        myItemInven[i] = Inven.DisplayName;
                    }
                    else
                    {
                        myItemInven[i] = "0";
                    }

                }
                for (int i = 0; i < 6; i++)
                {
                    Debug.Log("체크아이템리스트" + myItemInven[i]);


                }

                Debug.Log("체크아이템리스트1");
                int f = 0;
                for (int i = 0; i < 6; i++)
                {
                    Debug.Log("체크아이템리스트2");
                    for (int k = f; k < result.Inventory.Count; k++)
                    {
                        Debug.Log("체크아이템리스트3");
                        Debug.Log(f);
                        Debug.Log(k);
                        if (myItemInven[k] != "0")
                        {


                            inven_myitem[i].GetComponentInChildren<Text>().text = myItemInven[k];
                            f = k + 1;
                            Debug.Log("체크아이템리스트4");
                            break;

                        }

                    }


                }
            },

        (error) => print("인벤토리 불러오기 실패"));
    }
    public void updateInven()
    {
        //   PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
        //   {

        //       for (int i = 0; i < result.Inventory.Count; i++)
        //       {

        //           var Inven = result.Inventory[i];
        //           myUnitInven[i] = Inven.DisplayName;

        //       }
        //       for (int i = 0; i < 6; i++)
        //       {

        //           myunit_invenBtn[i].GetComponentInChildren<Text>().text = myUnitInven[i];

        //       }
        //   },

        //(error) => print("인벤토리 불러오기 실패"));
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
            for (int i = 0; i < 6; i++)
            {
                Debug.Log("체크0" + myUnitInven[i]);


            }

             
            int f = 0;
            for (int i = 0; i < 6; i++)
            {
                
                for (int k = f; k < result.Inventory.Count; k++)
                {
                     
                    if (myUnitInven[k] != "0")
                    {
                        Debug.Log(myUnitInven[k]);

                        myunit_invenBtn[i].GetComponentInChildren<Text>().text = myUnitInven[k];
                        f = k + 1;
                         
                        break;

                    }

                }


            }
        },

      (error) => print("인벤토리 불러오기 실패"));
        Debug.Log("인벤토리반환성공");
    }
    public void PurchaseUnit1()
    {
        unitname[0] = unitPurchaseBtn[0].GetComponentInChildren<Text>().text;
        saveunitcost[0] = int.Parse(unitCost[0].text);
        print(unitname[0]);
        print(saveunitcost[0]);
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unitname[0], VirtualCurrency = "GD", Price = saveunitcost[0] };
        PlayFabClientAPI.PurchaseItem(request, (result) => { print("유닛 구입 성공!"); updateInven(); SubtractMoney(saveunitcost[0]);
            var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unitname[0], unitname[0] } } };
            PlayFabClientAPI.UpdateUserData(request2, (result) => { print("성공"); }, (error) => print("실패"));
        }, (error) => print("유닛 구입 실패"));
         
         
    }
    public void PurchaseUnit2()
    {
        unitname[1] = unitPurchaseBtn[1].GetComponentInChildren<Text>().text;
        saveunitcost[1] = int.Parse(unitCost[1].text);
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unitname[1], VirtualCurrency = "GD", Price = saveunitcost[1] };
        PlayFabClientAPI.PurchaseItem(request, (result) => { print("유닛 구입 성공!"); updateInven(); SubtractMoney(saveunitcost[1]); var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unitname[1], unitname[1] } } };
            PlayFabClientAPI.UpdateUserData(request2, (result) => { print("성공"); }, (error) => print("실패"));
        }, (error) => print("유닛 구입 실패"));
         
    }
    public void PurchaseUnit3()
    {
        unitname[2] = unitPurchaseBtn[2].GetComponentInChildren<Text>().text;
        saveunitcost[2] = int.Parse(unitCost[2].text);
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unitname[2], VirtualCurrency = "GD", Price = saveunitcost[2] };
        PlayFabClientAPI.PurchaseItem(request, (result) => { print("유닛 구입 성공!"); updateInven(); SubtractMoney(saveunitcost[2]); var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unitname[2], unitname[2] } } };
            PlayFabClientAPI.UpdateUserData(request2, (result) => { print("성공"); }, (error) => print("실패"));
        }, (error) => print("유닛 구입 실패"));
      
    }
    public void PurchaseUnit4()
    {
        unitname[3] = unitPurchaseBtn[3].GetComponentInChildren<Text>().text;
        saveunitcost[3] = int.Parse(unitCost[3].text);
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unitname[3], VirtualCurrency = "GD", Price = saveunitcost[3] };
        PlayFabClientAPI.PurchaseItem(request, (result) => { print("유닛 구입 성공!"); updateInven(); SubtractMoney(saveunitcost[3]); var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unitname[3], unitname[3] } } };
            PlayFabClientAPI.UpdateUserData(request2, (result) => { print("성공"); }, (error) => print("실패"));
        }, (error) => print("유닛 구입 실패"));
       
    }
    public void PurchaseUnit5()
    {
        unitname[4] = unitPurchaseBtn[4].GetComponentInChildren<Text>().text;
        saveunitcost[4] = int.Parse(unitCost[4].text);
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unitname[4], VirtualCurrency = "GD", Price = saveunitcost[4] };
        PlayFabClientAPI.PurchaseItem(request, (result) => { print("유닛 구입 성공!"); updateInven(); SubtractMoney(saveunitcost[4]); var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unitname[4], unitname[4] } } };
            PlayFabClientAPI.UpdateUserData(request2, (result) => { print("성공"); }, (error) => print("실패"));
        }, (error) => print("유닛 구입 실패"));
       
    }
    public void PurchaseItem1()
    {
        itemname[0] = itemPurchaseBtn[0].GetComponentInChildren<Text>().text;
        saveitemcost[0] = int.Parse(ItemCost[0].text);
        Debug.Log(saveitemcost[0]);
        var request = new PurchaseItemRequest() { CatalogVersion = "Sub", ItemId = itemname[0], VirtualCurrency = "GD", Price = saveitemcost[0] };
        PlayFabClientAPI.PurchaseItem(request, (result) => {
            print("아이템 구입 성공!");
            SubtractMoney(saveitemcost[0]);
            updateClickInven();
        }, (error) => print("유닛 구입 실패"));


    }
    public void PurchaseItem2()
    {
        itemname[1] = itemPurchaseBtn[1].GetComponentInChildren<Text>().text;
        saveitemcost[1] = int.Parse(ItemCost[1].text) ;

        var request = new PurchaseItemRequest() { CatalogVersion = "Sub", ItemId = itemname[1], VirtualCurrency = "GD", Price = saveitemcost[1] };
        PlayFabClientAPI.PurchaseItem(request, (result) => {
            print("아이템 구입 성공!");
            SubtractMoney(saveitemcost[1]);
            updateClickInven();
        }, (error) => print("유닛 구입 실패"));


    }
    public void PurchaseItem3()
    {
        itemname[2] = itemPurchaseBtn[2].GetComponentInChildren<Text>().text;
        saveitemcost[2] = int.Parse(ItemCost[2].text) ;

        var request = new PurchaseItemRequest() { CatalogVersion = "Sub", ItemId = itemname[2], VirtualCurrency = "GD", Price = saveitemcost[2] };
        PlayFabClientAPI.PurchaseItem(request, (result) => {
            print("아이템 구입 성공!");
            SubtractMoney(saveitemcost[2]);
            updateClickInven();
        }, (error) => print("유닛 구입 실패"));


    }
    public void PurchaseItem4()
    {
        itemname[3] = itemPurchaseBtn[3].GetComponentInChildren<Text>().text;
        saveitemcost[3] = int.Parse(ItemCost[3].text) ;

        var request = new PurchaseItemRequest() { CatalogVersion = "Sub", ItemId = itemname[3], VirtualCurrency = "GD", Price = saveitemcost[3] };
        PlayFabClientAPI.PurchaseItem(request, (result) => {
            print("아이템 구입 성공!");
            SubtractMoney(saveitemcost[3]);
            updateClickInven();
        }, (error) => print("유닛 구입 실패"));


    }
    public void PurchaseItem5()
    {
        itemname[4] = itemPurchaseBtn[4].GetComponentInChildren<Text>().text;

        saveitemcost[4] = int.Parse(ItemCost[4].text) ;
        var request = new PurchaseItemRequest() { CatalogVersion = "Sub", ItemId = itemname[4], VirtualCurrency = "GD", Price = saveitemcost[4] };
        PlayFabClientAPI.PurchaseItem(request, (result) => {
            print("아이템 구입 성공!");
            SubtractMoney(saveitemcost[4]);
            updateClickInven();
        }, (error) => print("유닛 구입 실패"));


    }

    public void SubtractMoney(int money)
    {
        MyMoneyTxt = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Text>();
        var request = new SubtractUserVirtualCurrencyRequest() { VirtualCurrency = "GD", Amount = 50 };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, (result) =>{ print("돈 빼기 성공! 현재 돈 : " + result.Balance); MyMoneyTxt.text = "보유골드량 : " + result.Balance; }, (error) => print("돈 빼기 실패"));

    }

    public  void ClickUnit1()
    {
        ClickUnit.gameObject.GetComponentInChildren<Text>().text = myunit_invenBtn[0].GetComponentInChildren<Text>().text;
        string clickname = ClickUnit.gameObject.GetComponentInChildren<Text>().text;

        //string key = AllUnitList.FindFirstKeyByValue(clickname);
        //print("키값은 : " +key);
        var key = AllUnitList.FindFirstKeyByValue(clickname);

        //clickTxt.text
    }

    public void getItem1()
    {
        string getitemUnit = ClickUnit.gameObject.GetComponentInChildren<Text>().text;
        string getitemname = inven_myitem[0].GetComponentInChildren<Text>().text;
        var request1 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { getitemUnit, getitemname } } };
        PlayFabClientAPI.UpdateUserData(request1, (result) => { print("장착성공");  }, (error) => print("실패"));
    }
 //   public void updateGold()
 //   {
 //       PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
 //       {
 //           print("골드업데이트성공");
 //           MyMoneyTxt.text = "보유골드량 : " + result.VirtualCurrency["GD"];
 //       },
 ////(error) => print("골드업데이트실패"));
 //   }

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
