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

    public string[] unit_1 = new string [5];
    // Start is called before the first frame update
    void Start()
    {
        var requset = new GetCatalogItemsRequest { CatalogVersion = "Main" };
        PlayFabClientAPI.GetCatalogItems(requset, GetSuccess, GetFail);
    }
    private void GetFail(PlayFabError obj)
    {
        Debug.Log("카탈로그 불러오기 실패");
    }
    //카탈로그를 불러오는데 성공했다면 콜백이 된다.
    private void GetSuccess(GetCatalogItemsResult obj)
    {
        Debug.Log("칼탈로그 불러오기 성공");
        var items = obj.Catalog;
        for (int i = 2; i < items.Count; i++)
        {
            Debug.Log("아이템 아이디 =" + items[i].ItemId);
            unit_1[i] = items[i].ItemId;
        }
    }
    public void GetInventory() => PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
    {
        print("현재금액 : " + result.VirtualCurrency["GD"]);
        for (int i = 0; i < result.Inventory.Count; i++)
        {
            var Inven = result.Inventory[i];
            print(Inven.DisplayName + "/" + Inven.UnitCurrency + "/" + Inven.UnitPrice + "/" + Inven.ItemInstanceId + "/" + Inven.RemainingUses);
        }
    }, (error) => print("인벤토리 실패"));

    public void AddMoney()
    {
        var request = new AddUserVirtualCurrencyRequest() { VirtualCurrency = "GD", Amount = 50 };
        PlayFabClientAPI.AddUserVirtualCurrency(request, (result) => print("돈 얻기 성공! 현재 돈 : " + result.Balance), (error) => print("돈 얻기 실패"));
    }
    public void PurchaseUnit1()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = "1", VirtualCurrency = "GD", Price = 1000 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("유닛 구입 성공!"), (error) => print("유닛 구입 실패"));
    }
    public void PurchaseUnit2()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = "weapon", VirtualCurrency = "GD", Price = 5000 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("유닛 구입 성공!"), (error) => print("유닛 구입 실패"));
    }
    public void PurchaseUnit3()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = "unit3", VirtualCurrency = "GD", Price = 6000 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("유닛 구입 성공!"), (error) => print("유닛 구입 실패"));
    }
    public void PurchaseUnit4()
    {
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = unit_1[2], VirtualCurrency = "GD", Price = 100 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("유닛 구입 성공!"), (error) => print("유닛 구입 실패"));
        var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { unit_1[2], "0"} } };
        PlayFabClientAPI.UpdateUserData(request2, (result) => { print("성공"); }, (error) => print("실패"));
    }
    public void ConsumeItem()
    {
        var request = new ConsumeItemRequest() { ConsumeCount = 1, ItemInstanceId = "E01A5C5EDD04BE06" };
        PlayFabClientAPI.ConsumeItem(request, (result) => print("유닛 사용 성공!"), (error) => print("유닛 사용 실패"));
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
