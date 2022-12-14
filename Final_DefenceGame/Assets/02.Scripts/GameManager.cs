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
    [SerializeField]
    Transform[] Points_1;
    public GameObject HumanPrefab; 
    public Button firstSpawnButton;
    public Button ThirdSpawnButton;
    public Button FourSpawnButton;
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
    public int mycost = 20;
    public GameObject[] spawnbutton = new GameObject[10];
    public Button[] spawnBtn = new Button[10];
    public GameObject[] obj = new GameObject[5];
    public GameObject[] obj_1 = new GameObject[5];
    public int memeber = 0;
    //3초마다 10마리 
    public string load_unit = "/Unit/";
    private float timePrev;
    public int saveunit = 0;
    public string[] unit = new string[10];
    string[] myUnitInven = new string[100];
    public Button[] myunit_invenBtn = new Button[6];
    public Text[] unitcostTxt = new Text[5];
    public int[] Allunit = new int[100];
    public int[] Useunit = new int[100];
    public int[] Randomunit = new int[5];
    public string[] unitidkey = new string[5];
    public string[] EnemyUnit = new string[5];
    public string[] saveenemy = new string[5];
    public Text FinishTxt;
    public Canvas Finishcanvas;
    string[] a = new string[5] { "", "", "", "", "" };
    //데이터들
    public static bool gamestrat = true;
    UnitList AllUnitList;
    MyunitList UseUnitList;
    public static bool win = false;
    void Start()
    {
        win = false;
        Time.timeScale = 1.0f;
        spawnBtn = GameObject.Find("Panel_ChoiceUnit").GetComponentsInChildren<Button>();
        //myunit_invenBtn = GameObject.Find("Inventory").GetComponentsInChildren<Button>();
        Points = GameObject.Find("Spqwn").GetComponentsInChildren<Transform>();
        Points_1 = GameObject.Find("Enemy_Spqwn").GetComponentsInChildren<Transform>();
        unitcostTxt = GameObject.Find("costbox").GetComponentsInChildren<Text>();
        timePrev = Time.time;
        AllUnitList =  GetComponent<UnitList>();
        UseUnitList =  GetComponent<MyunitList>();
        countdowntext.text = setTime.ToString();
        var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
        unitcostTxt[0].text="하이"; 
        var requset = new GetCatalogItemsRequest { CatalogVersion = "Enemy" };
       
        
        

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
            for (int i = 0; i < 5; i++)
            {
                 
                for (int k = f; k < result.Inventory.Count; k++)
                {
                   
                    k = UnityEngine.Random.Range(0, result.Inventory.Count);
                    if (myUnitInven[k] != "0")
                    {
                     
                         
                        spawnbutton[i].GetComponentInChildren<Text>().text = myUnitInven[k];
                        spawnBtn[i].GetComponentInChildren<Text>().text = myUnitInven[k];
                        string imagename = "Sprites/" + spawnBtn[i].GetComponentInChildren<Text>().text;

                        spawnBtn[i].image.sprite = Resources.Load(imagename, typeof(Sprite)) as Sprite;

                        string unitname = "Unit/" + spawnbutton[i].GetComponentInChildren<Text>().text;
                        //프리팹다넣게되면 
                         
                        //텍스트자리에 spawnbutton[i].GetComponentInChildren<Text>().text 넣기
                        obj[i] = Resources.Load(unitname, typeof(GameObject)) as GameObject;
                         
                         
                       
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
                          


                            unitidkey[j] = result.Catalog[k].Tags[10];
                            
                            unitcostTxt[j].text = unitidkey[j];



                        }


                    }
                }



            },
      (error) => print("실패"));

        },

      (error) => print("인벤토리 불러오기 실패"));
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Enemy" }, (result) =>
        {
            int k = 0;
            for (int j = 0; j < 5; j++)
            {
               
                for (int i = 0; i < result.Catalog.Count; i++)
                {

                    k = UnityEngine.Random.Range(0, 8);
                   
                    EnemyUnit[j] = result.Catalog[k].ItemId;


                   
                    string unitname = "Unit/" + EnemyUnit[j];
                  

                    obj_1[j] = Resources.Load(unitname, typeof(GameObject)) as GameObject;








                }
                int t = UnityEngine.Random.Range(1, 4);
                Instantiate(obj_1[j], Points_1[t].position, Points[spawnidx].rotation);
            }



        },
   (error) => print("실패"));
        
        
    }

     
    void Update()
    {

        if( Gamestart5.countexit == true)
            setTime -= Time.deltaTime;


        countdowntext.text = "남은 시간 : " + Mathf.Round(setTime).ToString();
        costtext.text = "보유코스트 : " + mycost.ToString();
        if(setTime<=0)
        {
            Time.timeScale = 0f;
            Debug.Log("패배");
            win = true;
            Finishcanvas.gameObject.SetActive(true);
            FinishTxt.text = "패배";

        }
        if (Time.time - timePrev > 5.0f)
        {
            mycost += 2;
            timePrev = Time.time;
        }
       if(setTime==90)
        {
            PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Enemy" }, (result) =>
            {
                int k = 0;
                for (int j = 0; j < 5; j++)
                {

                    for (int i = 0; i < result.Catalog.Count; i++)
                    {

                        k = UnityEngine.Random.Range(0, 8);

                        EnemyUnit[j] = result.Catalog[k].ItemId;



                        string unitname = "Unit/" + EnemyUnit[j];


                        obj_1[j] = Resources.Load(unitname, typeof(GameObject)) as GameObject;








                    }
                    int t = UnityEngine.Random.Range(1, 4);
                    Instantiate(obj_1[j], Points_1[t].position, Points[spawnidx].rotation);
                }



            },
   (error) => print("실패"));
        }
       if(setTime==30)
        {
            PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Enemy" }, (result) =>
            {
                int k = 0;
                for (int j = 0; j < 5; j++)
                {

                    for (int i = 0; i < result.Catalog.Count; i++)
                    {

                        k = UnityEngine.Random.Range(0, 8);

                        EnemyUnit[j] = result.Catalog[k].ItemId;



                        string unitname = "Unit/" + EnemyUnit[j];


                        obj_1[j] = Resources.Load(unitname, typeof(GameObject)) as GameObject;








                    }
                    int t = UnityEngine.Random.Range(1, 4);
                    Instantiate(obj_1[j], Points_1[t].position, Points[spawnidx].rotation);
                }



            },
   (error) => print("실패"));
        }

    }

   
    public void OnFirstSpawnButtonClick()
    {
        spawnidx = 1;
        firstSpawnButton.image.color = Color.gray;
        secondSpawnButton.image.color = Color.white;
        ThirdSpawnButton.image.color = Color.white;
        FourSpawnButton.image.color = Color.white;
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
        FourSpawnButton.image.color = Color.white;
    }
    public void OnThirdSpawnButtonClick()
    {
        spawnidx = 3;
        firstSpawnButton.image.color = Color.white;
        secondSpawnButton.image.color = Color.white;
        ThirdSpawnButton.image.color = Color.gray;
        FourSpawnButton.image.color = Color.white;
    }
    public void OnFourSpawnButtonClick()
    {
        spawnidx = 4;
        firstSpawnButton.image.color = Color.white;
        secondSpawnButton.image.color = Color.white;
        ThirdSpawnButton.image.color = Color.white;
       FourSpawnButton.image.color = Color.gray;
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
           

            
            int f = 0;
            for (int i = M; i <M+1; i++)
            {
                
                for (int k = f; k < result.Inventory.Count; k++)
                {
                    
                    k = UnityEngine.Random.Range(0, result.Inventory.Count);
                    if (myUnitInven[k] != "0")
                    {
                        

                        spawnbutton[i].GetComponentInChildren<Text>().text = myUnitInven[k];
                        string unitname = "Unit/" + spawnbutton[i].GetComponentInChildren<Text>().text;
                        //프리팹다넣게되면 
                        spawnBtn[i].GetComponentInChildren<Text>().text = myUnitInven[k];
                        string imagename = "Sprites/" + spawnBtn[i].GetComponentInChildren<Text>().text;

                        spawnBtn[i].image.sprite = Resources.Load(imagename, typeof(Sprite)) as Sprite;
                        //텍스트자리에 spawnbutton[i].GetComponentInChildren<Text>().text 넣기
                        obj[i] = Resources.Load(unitname, typeof(GameObject)) as GameObject;
                        f = k + 1;
                        String test_123 = "Unit/" + spawnbutton[i].GetComponentInChildren<Text>().text;
                        
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
    public void OnThirdtUnitButtonClick()
    {

        if (mycost > cost)
        {

            Instantiate(obj[2], Points[spawnidx].position,
            Points[spawnidx].rotation);
            mycost -= cost;
            ChangeUnit(1);

        }
        else
        {
            Debug.Log("코스트가 부족합니다.");
        }


    }
    public void OnFourUnitButtonClick()
    {

        if (mycost > cost)
        {

            Instantiate(obj[3], Points[spawnidx].position,
            Points[spawnidx].rotation);
            mycost -= cost;
            ChangeUnit(1);

        }
        else
        {
            Debug.Log("코스트가 부족합니다.");
        }


    }
    public void OnFiveUnitButtonClick()
    {

        if (mycost > cost)
        {

            Instantiate(obj[4], Points[spawnidx].position,
            Points[spawnidx].rotation);
            mycost -= cost;
            ChangeUnit(1);

        }
        else
        {
            Debug.Log("코스트가 부족합니다.");
        }


    }

    public void finishBtn()
    {
        SceneManager.LoadScene("ChoiceStage_Scene");
    }
}