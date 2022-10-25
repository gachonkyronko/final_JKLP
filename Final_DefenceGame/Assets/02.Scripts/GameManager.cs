using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
public class GameManager : MonoBehaviour
{
    [SerializeField] //태어날 위치 지정 
    Transform[] Points;
    public GameObject HumanPrefab; 
    public Button firstSpawnButton;
    public Button ThirdSpawnButton;
    public UnityAction firstSpawnaction;
    public Button secondSpawnButton;
    public UnityAction secondSpawnaction;
    public Button firstunitButton;
    public UnityAction firstunitaction;
    public Button menuButton;
    public UnityAction menuaction;
    public Button menubackButton;
    public UnityAction menubackaction;
    public Button menuhomeButton;
    public UnityAction menuhomeaction;
    private int spawnidx = 1;
    
    public float setTime = 120.0f;
    public Text countdowntext;
    public Text costtext;
    public int cost = 3;
    public int mycost = 1000;
    public GameObject[] spawnbutton = new GameObject[10];
    public GameObject[] obj = new GameObject[5];
    public int memeber = 0;
    //3초마다 10마리 
    public string load_unit = "/Unit/";
    private float timePrev;
    public int saveunit = 0;
    public string[] unit = new string[10];
    string[] myUnitInven = new string[10000];
    public Button[] myunit_invenBtn = new Button[6];
    public Text[] unitcostTxt = new Text[5];
    public int[] Allunit = new int[100];
    public int[] Useunit = new int[100];
    public int[] Randomunit = new int[5];
    public string[] unitidkey = new string[5];
    string[] a = new string[5] { "", "", "", "", "" };
    //데이터들
   
    UnitList AllUnitList;
    MyunitList UseUnitList;
    
    void Start()
    {
        
        //myunit_invenBtn = GameObject.Find("Inventory").GetComponentsInChildren<Button>();
        Points = GameObject.Find("Spqwn").GetComponentsInChildren<Transform>();
        unitcostTxt = GameObject.Find("costbox").GetComponentsInChildren<Text>();
        timePrev = Time.time;
        AllUnitList =  GetComponent<UnitList>();
        UseUnitList =  GetComponent<MyunitList>();
        countdowntext.text = setTime.ToString();
        var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
        unitcostTxt[0].text="하이"; 
        var requset = new GetCatalogItemsRequest { CatalogVersion = "Main" };
        Debug.Log("유닛정보받기, 적용시작");
         
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
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("체크2");
                for (int k = f; k < result.Inventory.Count; k++)
                {
                    Debug.Log("체크3");
                    Debug.Log(f);
                    Debug.Log(k);
                    k = UnityEngine.Random.Range(0, result.Inventory.Count);
                    if (myUnitInven[k] != "0")
                    {
                        Debug.Log(myUnitInven[k]);
                         
                        spawnbutton[i].GetComponentInChildren<Text>().text = myUnitInven[k];
                        string unitname = "Unit/" + spawnbutton[i].GetComponentInChildren<Text>().text;
                        //프리팹다넣게되면 
                         
                        //텍스트자리에 spawnbutton[i].GetComponentInChildren<Text>().text 넣기
                        obj[i] = Resources.Load(unitname, typeof(GameObject)) as GameObject;
                         
                         
                        Debug.Log("체크4");
                        break;

                    }

                }


            }
            PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Main" }, (result) =>
            {
                int k = 0;
                for (int j = 0; j < 5; j++)
                {
                    for (int i = 0; i < result.Catalog.Count; i++)
                    {
                        if (result.Catalog[i].DisplayName == spawnbutton[j].GetComponentInChildren<Text>().text)
                        {
                            k = i;
                            Debug.Log("k값받음!" + k);


                            unitidkey[j] = result.Catalog[k].Tags[10];
                            Debug.Log(unitidkey[j]);
                            unitcostTxt[j].text = unitidkey[j];



                        }


                    }
                }



            },
      (error) => print("실패"));

        },

      (error) => print("인벤토리 불러오기 실패"));
        Debug.Log("유닛정보받기완료, 적용시작");
        
    }

     
    void Update()
    {
        
        
        setTime -= Time.deltaTime;
        countdowntext.text = "남은 시간 : " + Mathf.Round(setTime).ToString();
        costtext.text = "보유코스트 : " + mycost.ToString();
        if(setTime<=0)
        {
            Time.timeScale = 0f;
            Debug.Log("패배");
        }
        if (Time.time - timePrev > 5.0f)
        {
            mycost += 2;
            timePrev = Time.time;
        }
       

    }
    
   
    public void OnFirstSpawnButtonClick()
    {
        spawnidx = 1;
        firstSpawnButton.image.color = Color.gray;
        secondSpawnButton.image.color = Color.white;
        ThirdSpawnButton.image.color = Color.white;
    }
    public void OnMenuButtonClick()
    {
        Time.timeScale = 0f;

    }
    public void OnMenuBackButtonClick()
    {
        Time.timeScale = 1.0f;

    }
    public void OnMenuHomeButtonClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainHome_Scene");
    }
    public void OnSecondSpawnButtonClick()
    {
        spawnidx = 2;
        firstSpawnButton.image.color = Color.white;
        secondSpawnButton.image.color = Color.gray;
        ThirdSpawnButton.image.color = Color.white;
    }
    public void OnThirdSpawnButtonClick()
    {
        spawnidx = 3;
        firstSpawnButton.image.color = Color.white;
        secondSpawnButton.image.color = Color.white;
        ThirdSpawnButton.image.color = Color.gray;
    }
    void ChangeUnit(int M)
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
                else if(result.Inventory[i].ItemClass == myUnitInven[M])
                {
                    myUnitInven[i] = "0";
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
            int f = 0;
            for (int i = M; i <M+1; i++)
            {
                Debug.Log("체크2");
                for (int k = f; k < result.Inventory.Count; k++)
                {
                    Debug.Log("체크3");
                    Debug.Log(f);
                    Debug.Log(k);
                    k = UnityEngine.Random.Range(0, result.Inventory.Count);
                    if (myUnitInven[k] != "0")
                    {
                        Debug.Log(myUnitInven[k]);

                        spawnbutton[i].GetComponentInChildren<Text>().text = myUnitInven[k];
                        string unitname = "Unit/" + spawnbutton[i].GetComponentInChildren<Text>().text;
                        //프리팹다넣게되면 
                        
                        //텍스트자리에 spawnbutton[i].GetComponentInChildren<Text>().text 넣기
                        obj[i] = Resources.Load(unitname, typeof(GameObject)) as GameObject;
                        f = k + 1;
                        String test_123 = "Unit/" + spawnbutton[i].GetComponentInChildren<Text>().text;
                        Debug.Log(test_123);
                        Debug.Log("체크4");
                        break;

                    }

                }


            }
            Debug.Log("체인지완료");
        },

 (error) => print("인벤토리 불러오기 실패"));
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Main" }, (result) =>
        {
            int k = 0;
            for (int j = M; j < M+1; j++)
            {
                for (int i = 0; i < result.Catalog.Count; i++)
                {
                    if (result.Catalog[i].DisplayName == spawnbutton[M].GetComponentInChildren<Text>().text)
                    {
                        k = i;
                        Debug.Log("k값받음!" + k);


                        unitidkey[M] = result.Catalog[k].Tags[10];
                        Debug.Log(unitidkey[M]);
                        unitcostTxt[M].text = unitidkey[M];



                    }


                }
            }



        },
     (error) => print("실패"));


    }
    public void OnFirstUnitButtonClick()
    {
        int unitcost = 0;
        
        unitcost = int.Parse(unitcostTxt[0].text);
        if (mycost> unitcost)
        {
            Instantiate(obj[0], Points[spawnidx].position,Points[spawnidx].rotation);
            mycost -= unitcost;

            ChangeUnit(0);
                

            
        }
        else
        {
            Debug.Log("코스트가 부족합니다.");
        }
        
         
    }
    public void OnSecondtUnitButtonClick()
    {
         
        if (mycost > cost)
        {
             
            Instantiate(obj[1], Points[spawnidx].position,
            Points[spawnidx].rotation);
            mycost -= cost;
            ChangeUnit(1);

        }
        else
        {
            Debug.Log("코스트가 부족합니다.");
        }


    }


}