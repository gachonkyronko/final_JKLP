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
    public Sprite test;
    public Text MyMoneyTxt;
    public Text clickTxt;
    public int MyMoney = 0;
    public string[] unit_1 = new string [5]; //������ ���̵�   �迭�� ���������� ����
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
    public GameObject[] unitPurchaseBtn_1 = new GameObject[5];
    public Button[] itemPurchaseBtn = new Button[5];
    public Button[] myunit_invenBtn = new Button[6];
    public Button[] inven_myitem = new Button[6];
    public Text[] unitCost = new Text[5];
    public Text[] ItemCost = new Text[5];
    int[] item = new int[5];
    //�����׽�Ʈ
    public int[] Allunit = new int[100];
    public int[] Useunit = new int[100];
    public int[] Randomunit = new int[5];
    string[] a = new string[5] { "", "", "", "", "" };
    string[] catalog = new string[100];
    int[] b = new int[5] {0 ,0,0,0,0};
    public string[] myunit_inven = new string[6];
    public
    int o = 0;
    
    string[] myUnitInven = new string[10000];
    string[] myItemInven = new string[10000];
    UnitList AllUnitList;
    MyunitList UseUnitList;
    string itemid = "";
    bool haveunit = false;
  
    void Start() //���������ҷ�����
    {

        myunit_invenBtn = GameObject.Find("Inventory").GetComponentsInChildren<Button>();
        inven_myitem = GameObject.Find("inven_myitem").GetComponentsInChildren<Button>();
        unitPurchaseBtn = GameObject.Find("PurchaseUnit").GetComponentsInChildren<Button>();
     
        itemPurchaseBtn = GameObject.Find("PurchaseItem").GetComponentsInChildren<Button>();
        unitCost = GameObject.Find("PurchaseUnitCost").GetComponentsInChildren<Text>();
        ItemCost = GameObject.Find("PurchaseItemCost").GetComponentsInChildren<Text>();
        AllitemID = GameObject.Find("StoreScene_Mng").GetComponent<ItemList>();
        AllUnitList = GameObject.Find("StoreScene_Mng").GetComponent<UnitList>();
       
        UseUnitList = GameObject.Find("StoreScene_Mng").GetComponent<MyunitList>();
         
        int i = 0;
        int j = 0;  
        int n = 0;
        int m = 0;
        Itemnumber = AllitemID.GetKey();
        Allunit = AllUnitList.GetKey();
        //Allunit = AllUnitList.GetKey();
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
            
            b[t] = AllUnitList.FindDic(Randomunit[t]).Cost *100;
            

        }
        Debug.Log("���־��̵��ȯ�Ϸ�, �ؽ�Ʈ������ȯ����");
        for (int q = 0; q < 5; q++)
        {
            unitPurchaseBtn[q].GetComponentInChildren<Text>().text = a[q];
            unitCost[q].text = b[q].ToString();

        }

        Debug.Log("���־��̵��ȯ�Ϸ�, �κ��丮�����ȯ��");
        
        
        var requset = new GetCatalogItemsRequest { CatalogVersion = "Main" };
        PlayFabClientAPI.GetCatalogItems(requset, GetSuccess, GetFail);
        MyMoneyTxt = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Text>();
         
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
       {
           int unitcount = 0;
           for (int i = 0; i < result.Inventory.Count; i++)
           {

               var Inven = result.Inventory[i];
               if(result.Inventory[i].ItemClass =="Unit")
               {
                   myUnitInven[i] = Inven.DisplayName;
                   unitcount++;
               }
               else
               {
                   myUnitInven[i] = "0";
               }

           }
           for (int i = 0; i < 6; i++)
           {
               Debug.Log("üũ0" + myUnitInven[i]);
                

           }
            
           Debug.Log("üũ1");
           int f=0;
           //�κ��丮�� ���ָ� �ִ´�.
           for (int i = 0; i < 6; i++)
           {
               Debug.Log("üũ2");
               for (int k = f;k< result.Inventory.Count;k++)
               {
                   Debug.Log("üũ3");
                   Debug.Log(f);
                   Debug.Log(k);
                   if (myUnitInven[k] != "0")
                   {
                       Debug.Log(myUnitInven[k]);
                        
                       myunit_invenBtn[i].GetComponentInChildren<Text>().text = myUnitInven[k];
                       if(myunit_invenBtn[i].GetComponentInChildren<Text>().text != "�� ����")
                       {
                           string imagename = "Sprites/" + myunit_invenBtn[i].GetComponentInChildren<Text>().text;
                           Debug.Log("�̸�" + myunit_invenBtn[i].GetComponentInChildren<Text>().text);
                           Debug.Log("�̹������" + imagename);

                           myunit_invenBtn[i].image.sprite = Resources.Load(imagename, typeof(Sprite)) as Sprite;
                       }
                      


                       f = k+1;
                       Debug.Log("üũ4");
                       break;

                   }
                    
               }
               

           }
           //for (int i = 0; i < unitcount; i++)
           //{
              
           //    for (int k = f; k < result.Inventory.Count; k++)
           //    {
                   
           //        if (myUnitInven[k] != "0")
           //        {

           //            UseUnitList.AddUnit(UseUnitList.FindDic_name(myUnitInven[k]).ID);
                        
           //            f = k + 1;
                       
           //            break;

           //        }

           //    }


           //}

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
               Debug.Log("üũ�����۸���Ʈ" + myItemInven[i]);


           }

           Debug.Log("üũ�����۸���Ʈ1");
           int s = 0;
           for (int i = 0; i < 6; i++)
           {
               Debug.Log("üũ�����۸���Ʈ2");
               for (int k = s; k < result.Inventory.Count; k++)
               {
                   Debug.Log("üũ�����۸���Ʈ3");
                   Debug.Log(s);
                   Debug.Log(k);
                   if (myItemInven[k] != "0")
                   {


                       inven_myitem[i].GetComponentInChildren<Text>().text = myItemInven[k];
                      
                       s = k + 1;
                       Debug.Log("üũ�����۸���Ʈ4");
                       break;

                   }

               }


           }
       },

     (error) => print("�κ��丮 �ҷ����� ����"));
        
        Debug.Log("�κ��丮��ȯ����");
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
        for (int i = 0; i < items.Count; i++)
        {
            
             
            //Debug.Log("Ŀ���� ������ =" + dic["key1"]);
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

     (error) => print("�κ��丮 �ҷ����� ����"));

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
                    Debug.Log("üũ�����۸���Ʈ" + myItemInven[i]);


                }

                Debug.Log("üũ�����۸���Ʈ1");
                int f = 0;
                for (int i = 0; i < 6; i++)
                {
                    Debug.Log("üũ�����۸���Ʈ2");
                    for (int k = f; k < result.Inventory.Count; k++)
                    {
                        Debug.Log("üũ�����۸���Ʈ3");
                        Debug.Log(f);
                        Debug.Log(k);
                        if (myItemInven[k] != "0")
                        {


                            inven_myitem[i].GetComponentInChildren<Text>().text = myItemInven[k];
                            f = k + 1;
                            Debug.Log("üũ�����۸���Ʈ4");
                            break;

                        }

                    }


                }
            },

        (error) => print("�κ��丮 �ҷ����� ����"));
    }
    public void changeImage()
    {


        for (int i = 0; i < 6; i++)
        {

            string imagename = "Sprites/" + unitPurchaseBtn[i].GetComponentInChildren<Text>().text;
            Debug.Log("�̸�" + unitPurchaseBtn[i].GetComponentInChildren<Text>().text);
            Debug.Log("�̹������" + imagename);

            unitPurchaseBtn[i].image.sprite = Resources.Load(imagename, typeof(Sprite)) as Sprite;
        }

    }
        public void updateInven()
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
            for (int i = 0; i < 6; i++)
            {
                
                for (int k = f; k < result.Inventory.Count; k++)
                {
                     
                    if (myUnitInven[k] != "0")
                    {
                        Debug.Log(myUnitInven[k]);
                       
                        myunit_invenBtn[i].GetComponentInChildren<Text>().text = myUnitInven[k];
                        string imagename = "Sprites/" + myunit_invenBtn[i].GetComponentInChildren<Text>().text;
                        Debug.Log("�̸�" + myunit_invenBtn[i].GetComponentInChildren<Text>().text);
                        Debug.Log("�̹������" + imagename);

                        myunit_invenBtn[i].image.sprite = Resources.Load(imagename, typeof(Sprite)) as Sprite;
                        f = k + 1;
                         
                        break;

                    }

                }


            }
            
        },

      (error) => print("�κ��丮 �ҷ����� ����"));
        Debug.Log("�κ��丮��ȯ����");
    }
    
    public void checkhaveunit(int u)
    {
        u = u - 1;
        unitname[u] = unitPurchaseBtn[u].GetComponentInChildren<Text>().text;
        saveunitcost[u] = int.Parse(unitCost[u].text);
        Debug.Log("�����Ϸ�������" + unitname[u]);
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
        {

            for (int i = 0; i < result.Inventory.Count; i++)
            {
                var Inven = result.Inventory[i];
                if (result.Inventory[i].ItemId == unitname[u])
                {
                    haveunit = true;
                    Debug.Log("������ : " + haveunit);
                    break;
                }
            }
            print("�ߺ�üũ�Ϸ� : " + haveunit);

            if (haveunit == false)
            {
                var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unitname[u], VirtualCurrency = "GD", Price = saveunitcost[u] };
                PlayFabClientAPI.PurchaseItem(request, (result) => {
                    print("���� ���� ����!"); updateInven(); SubtractMoney(saveunitcost[u]); UseUnitList.AddUnit(Randomunit[u]);
                    var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unitname[u], "1234" } } };
                    PlayFabClientAPI.UpdateUserData(request2, (result) => { print("����"); }, (error) => print("����"));
                }, (error) => print("���� ���� ����"));
                haveunit = false;
            }
            else
            {
                Debug.Log("�̹� �������� �����Դϴ�.");
                haveunit = false;

            }
            print("�������� : " + haveunit);

        },

    (error) => print("�Ϸ�"));
    }
public void PurchaseUnit1()
    {
        checkhaveunit(1);


    }
    public void PurchaseUnit2()
    {
        checkhaveunit(2);

    }
    public void PurchaseUnit3()
    {
        checkhaveunit(3);
    }
    public void PurchaseUnit4()
    {
        checkhaveunit(4);

    }
    public void PurchaseUnit5()
    {
        checkhaveunit(5);

    }
    public void purchaseitems(int u)
    {
        u = u - 1;
        itemname[u] = itemPurchaseBtn[u].GetComponentInChildren<Text>().text;
        saveitemcost[u] = int.Parse(ItemCost[u].text);
        Debug.Log(saveitemcost[0]);
        var request = new PurchaseItemRequest() { CatalogVersion = "Sub", ItemId = itemname[u], VirtualCurrency = "GD", Price = saveitemcost[u] };
        PlayFabClientAPI.PurchaseItem(request, (result) => {
            print("������ ���� ����!");
            SubtractMoney(saveitemcost[u]);
            updateClickInven(); 
            var request1 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { itemname[u], "1234" } } };
            PlayFabClientAPI.UpdateUserData(request1, (result) => { print("null�����۳���"); }, (error) => print("����"));
        }, (error) => print("���� ���� ����"));
    }
    public void PurchaseItem1()
    {
        purchaseitems(1);


    }
    public void PurchaseItem2()
    {
        purchaseitems(2);


    }

    public void PurchaseItem3()
    {
        purchaseitems(3);


    }
    public void PurchaseItem4()
    {
        purchaseitems(4);

    }
    public void PurchaseItem5()
    {
        purchaseitems(5);


    }

    public void SubtractMoney(int money)
    {
        MyMoneyTxt = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Text>();
        var request = new SubtractUserVirtualCurrencyRequest() { VirtualCurrency = "GD", Amount = 50 };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, (result) =>{ print("�� ���� ����! ���� �� : " + result.Balance); MyMoneyTxt.text = "������差 : " + result.Balance; }, (error) => print("�� ���� ����"));

    }
    public void ClickLogic(int r)
    {
        ClickUnit.gameObject.GetComponentInChildren<Text>().text = myunit_invenBtn[r].GetComponentInChildren<Text>().text;
        string clickname = ClickUnit.gameObject.GetComponentInChildren<Text>().text;
        //int test = UseUnitList.FindDic_name(clickname).Attack;
        string imagename = "Sprites/" + clickname;


        ClickUnit.image.sprite = Resources.Load(imagename, typeof(Sprite)) as Sprite;

        int k = 0;
        string UnitDescription = "";
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Main" }, (result) =>
        {
            for (int i = 0; i < result.Catalog.Count; i++)
            {
                if (result.Catalog[i].DisplayName == clickname)
                {
                    k = i;
                    Debug.Log("k������!" + k);
                    Debug.Log(result.Catalog[i].DisplayName);

                    UnitDescription = result.Catalog[k].Description;
                    Debug.Log(UnitDescription);
                    clickTxt.text = UnitDescription;

                }


            }


        },
        (error) => print("����"));

    }
    public  void ClickUnit1()
    {
        ClickLogic(0);
        
       
    }
    public void ClickUnit2()
    {
        ClickLogic(1);


    }
    public void ClickUnit3()
    {
        ClickLogic(2);


    }
    public void ClickUnit4()
    {
        ClickLogic(3);


    }
    public void ClickUnit5()
    {
        ClickLogic(4);


    }
    public void ClickUnit6()
    {
        ClickLogic(5);


    }

    public void getItem1()
    {
        string getitemUnit = ClickUnit.gameObject.GetComponentInChildren<Text>().text;
        string getitemname = inven_myitem[0].GetComponentInChildren<Text>().text;
        getItemlogic(getitemUnit, getitemname);
        
    }
    public void getItemlogic(string z, string y)
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Sub" }, (result) =>
        {
            int k = 0;
            
                for (int i = 0; i < result.Catalog.Count; i++)
                {
                    if (result.Catalog[i].DisplayName == y)
                    {
                        k = i;
                        Debug.Log("k������!" + k);
                        
                      
                        itemid = result.Catalog[k].Tags[0];
                        Debug.Log(itemid);
                        break;


                    }


                }
            var request1 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { z,  itemid} } };
            PlayFabClientAPI.UpdateUserData(request1, (result) => { print("��������"); }, (error) => print("����"));



        },
   (error) => print("����"));
        
    }
 
    //}
    public void OnBackbuttonClick() //�ڷΰ��� �̺�Ʈ
    {
        SceneManager.LoadScene("MainHome_Scene");
    }

    
   
void Update()
    {

       

    }
}
