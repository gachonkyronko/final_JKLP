using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StoreScene_Mng : MonoBehaviour
{
    public Button ChargegoldButton;
    public Button UnitButton1;
    public Button UnitButton2;
    public Button UnitButton3;
    public Button UnitButton4;
    public Button reinforceButton1;
    public Button reinforceButton2;
    public Button reinforceButton3;
    public Button reinforceButton4;
    private UnityAction backaction;
    public Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GetInventory() => PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
    {
        print("����ݾ� : " + result.VirtualCurrency["GD"]);
        for (int i = 0; i < result.Inventory.Count; i++)
        {
            var Inven = result.Inventory[i];
            print(Inven.DisplayName + "/" + Inven.UnitCurrency + "/" + Inven.UnitPrice + "/" + Inven.ItemInstanceId + "/" + Inven.RemainingUses);
        }
    }, (error) => print("�κ��丮 ����"));

    public void AddMoney()
    {
        var request = new AddUserVirtualCurrencyRequest() { VirtualCurrency = "GD", Amount = 50 };
        PlayFabClientAPI.AddUserVirtualCurrency(request, (result) => print("�� ��� ����! ���� �� : " + result.Balance), (error) => print("�� ��� ����"));
    }
    public void PurchaseUnit1()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = "1", VirtualCurrency = "GD", Price = 1000 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("���� ���� ����!"), (error) => print("���� ���� ����"));
    }
    public void PurchaseUnit2()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = "weapon", VirtualCurrency = "GD", Price = 5000 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("���� ���� ����!"), (error) => print("���� ���� ����"));
    }
    public void PurchaseUnit3()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = "unit3", VirtualCurrency = "GD", Price = 6000 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("���� ���� ����!"), (error) => print("���� ���� ����"));
    }
    public void PurchaseUnit4()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = "unit4", VirtualCurrency = "GD", Price = 7000 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("���� ���� ����!"), (error) => print("���� ���� ����"));
    }
    public void ConsumeItem()
    {
        var request = new ConsumeItemRequest() { ConsumeCount = 1, ItemInstanceId = "E01A5C5EDD04BE06" };
        PlayFabClientAPI.ConsumeItem(request, (result) => print("���� ��� ����!"), (error) => print("���� ��� ����"));
    }
    public void OnBackbuttonClick()
    {
        SceneManager.LoadScene("MainHome_Scene");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
