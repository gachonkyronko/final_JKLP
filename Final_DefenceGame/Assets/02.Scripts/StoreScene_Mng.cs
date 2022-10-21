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
    public string[] unit_1 = new string [5]; //������ ���̵�   �迭�� ���������� ����
    ItemList AllitemID;
    Transform[] Buttons;
    int[] Itemnumber = new int[100];
     
    int[] item = new int[5];
    //�����׽�Ʈ
    public int[] Allunit = new int[100];
    public int[] Useunit = new int[100];
    public int[] Randomunit = new int[5];
    string[] a = new string[5];
    UnitList AllUnitList;
    MyunitList UseUnitList;
    void Start() //���������ҷ�����
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
            print("�����۾��̵� : "+item[j]);
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
            print("���� ���̵� : " + Randomunit[t]);
             
        }

        
        Buttons = GameObject.Find("PurchaseItem").GetComponentsInChildren<Transform>();
        var requset = new GetCatalogItemsRequest { CatalogVersion = "Main" };
        PlayFabClientAPI.GetCatalogItems(requset, GetSuccess, GetFail);
        MyMoneyTxt = GameObject.Find("Canvas").transform.GetChild(9).GetComponent<Text>();
    }
    
    private void GetFail(PlayFabError obj)
    {
        Debug.Log("īŻ�α� �ҷ����� ����");
    }
    //īŻ�α׸� �ҷ����µ� �����ߴٸ� �ݹ��� �ȴ�.
    private void GetSuccess(GetCatalogItemsResult obj) //���������ҷ����� ������差ǥ��
    {
        Debug.Log("ĮŻ�α� �ҷ����� ����");
        var items = obj.Catalog;
        //�迭�� �����۾��̵��������� ����
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log("������ ���̵� =" + items[i].ItemId);
            unit_1[i] = items[i].ItemId;
        }
        //��差ǥ��
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
        {
            print("����ݾ� : " + result.VirtualCurrency["GD"]);
            MyMoneyTxt.text = "������差 : " + result.VirtualCurrency["GD"];
        },
  (error) => print("�����差 �ҷ����� ����"));
    }
    

    public void AddMoney() //���߰��� ȹ�� + �����ؽ�Ʈ���뺯��
    {
        var request = new AddUserVirtualCurrencyRequest() { VirtualCurrency = "GD", Amount = 100 };
        PlayFabClientAPI.AddUserVirtualCurrency(request, (result) => { print("�� ��� ����! ���� �� : " + result.Balance);  MyMoneyTxt.text = "������差 : " + result.Balance; }, (error) => print("�� ��� ����"));
    }
    public void PurchaseUnit1()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unit_1[0], VirtualCurrency = "GD", Price = 100 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("���� ���� ����!"), (error) => print("���� ���� ����"));
        var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unit_1[0], "Tiger" } } };
        PlayFabClientAPI.UpdateUserData(request2, (result) => { print("����"); }, (error) => print("����"));
    }
    public void PurchaseUnit2()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unit_1[1], VirtualCurrency = "GD", Price = 100 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("���� ���� ����!"), (error) => print("���� ���� ����"));
        var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unit_1[1], "Vampire" } } };
        PlayFabClientAPI.UpdateUserData(request2, (result) => { print("����"); }, (error) => print("����"));
    }
    public void PurchaseUnit3()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unit_1[2], VirtualCurrency = "GD", Price = 100 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("���� ���� ����!"), (error) => print("���� ���� ����"));
        var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unit_1[2], "Tiger" } } };
        PlayFabClientAPI.UpdateUserData(request2, (result) => { print("����"); }, (error) => print("����"));
    }
    public void PurchaseUnit4()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unit_1[3], VirtualCurrency = "GD", Price = 100 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("���� ���� ����!"), (error) => print("���� ���� ����"));
        var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unit_1[3], "Vampire"} } };
        PlayFabClientAPI.UpdateUserData(request2, (result) => { print("����"); }, (error) => print("����"));
    }
    //���뵵��� �ϴ� �ּ�
    //public void ConsumeItem()
    //{
    //    var request = new ConsumeItemRequest() { ConsumeCount = 1, ItemInstanceId = "E01A5C5EDD04BE06" };
    //    PlayFabClientAPI.ConsumeItem(request, (result) => print("���� ��� ����!"), (error) => print("���� ��� ����"));
    //}
    public void OnBackbuttonClick() //�ڷΰ��� �̺�Ʈ
    {
        SceneManager.LoadScene("MainHome_Scene");
    }
   
void Update()
    {
    
     
        
    }
}
