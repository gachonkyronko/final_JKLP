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
    public int[] Allunit = new int[100];
    public int[] Useunit = new int[100];
    public int[] Randomunit = new int[5];
    string[] a = new string[5] { "", "", "", "", "" };
    UnitList AllUnitList;
    MyunitList UseUnitList;
    
    void Start()
    {
        //myunit_invenBtn = GameObject.Find("Inventory").GetComponentsInChildren<Button>();
        Points = GameObject.Find("Spqwn").GetComponentsInChildren<Transform>();
        int n = 0;
        int m = 0;
        timePrev = Time.time;
        AllUnitList =  GetComponent<UnitList>();
        UseUnitList =  GetComponent<MyunitList>();
        countdowntext.text = setTime.ToString();
        var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
        //PlayFabClientAPI.GetUserData(request1, (result) => { unit[1] = result.Data["Tiger"].Value; Debug.Log("타이거넘어옴"); saveunit++; unitCreate();  }, (error) => print("데이터못넘김"));
        //foreach (int number in Allunit)
        //{
        //    if (number == 0)
        //    {

        //        break;

        //    }
        //    n++;
        //}

        //foreach (int number in Useunit)
        //{
        //    if (number == 0)
        //    {

        //        break;

        //    }
        //    m++;
        //}

        //for (int k = 0; k < 5; k++)
        //{

        //    int rootnum = 0;
        //    int p;
        //    while (true)
        //    {
        //        rootnum++;

        //        p = UnityEngine.Random.Range(0, n);
        //        int number = Allunit[p];
        //        var check = Array.Exists(Useunit, x => x.Equals(number));

        //        if (check == false)
        //        {
        //            Randomunit[k] = number;

        //            break;

        //        }
        //        else
        //        {
        //            if (rootnum > 10000)

        //                break;
        //        }
        //    }
        //}

        //for (int t = 0; t < 5; t++)
        //{

        //    a[t] = AllUnitList.FindDic(Randomunit[t]).Name;

        //    PlayFabClientAPI.GetUserData(request1, (result) => { unit[t] = result.Data[a[t]].Value; Debug.Log("유닛가져옴");   }, (error) => print("유닛못가져옴"));


        //}
        //unitCreate();
        //Debug.Log("유닛정보받음, 텍스트변환시작");
        var requset = new GetCatalogItemsRequest { CatalogVersion = "Main" };
        Debug.Log("유닛정보받기, 적용시작");
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
        {

            for (int i = 0; i < result.Inventory.Count; i++)
            {
                int p;
                p = UnityEngine.Random.Range(0, result.Inventory.Count);
                var Inven = result.Inventory[p];
                myUnitInven[i] = Inven.DisplayName;

            }
            //for(int j=0;j<6;j++)
            //{
            //    int idx = UnityEngine.Random.Range(0, 6);
            //    myUnitInven[j] = myUnitInven[idx];
            //}
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("출력값확인 : " + myUnitInven[i]);
                 
                spawnbutton[i].GetComponentInChildren<Text>().text = myUnitInven[i];
            }
        },

      (error) => print("인벤토리 불러오기 실패"));
        Debug.Log("유닛정보받기완료, 적용시작");
    }
     
    //void unitCreate()
    //{
    //    //if (saveunit == 2)
    //    //{
    //    //    for (int i = 0; i < 5; i++)
    //    //    {
    //    //        for (int j = 0; j < 10000; j++)
    //    //        {
    //    //            int idx = UnityEngine.Random.Range(0, 10);
    //    //            if (spawnbutton[i].GetComponentInChildren<Text>().text == "Button" || spawnbutton[i].GetComponentInChildren<Text>().text == "")
    //    //                spawnbutton[i].GetComponentInChildren<Text>().text = unit[idx];
    //    //            else
    //    //            {

    //    //                obj[i] = Resources.Load(spawnbutton[i].GetComponentInChildren<Text>().text, typeof(GameObject)) as GameObject;

    //    //                break;
    //    //            }

    //    //        }
    //    //        Debug.Log(i + "/" + spawnbutton[i].GetComponentInChildren<Text>().text);

    //    //    }
    //    //}
    //    //for(int i = 0; i<5;i++)
    //    //{
    //    //    spawnbutton[i].GetComponentInChildren<Text>().text = unit[i];
    //    //}
    //}
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
    }
    public void OnThirdSpawnButtonClick()
    {
        spawnidx = 3;
        firstSpawnButton.image.color = Color.white;
        secondSpawnButton.image.color = Color.gray;
    }
    void ChangeUnit(int k)
    {
        
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    int idx = UnityEngine.Random.Range(0, 10);
                    if (spawnbutton[k].GetComponentInChildren<Text>().text == "Button" || spawnbutton[i].GetComponentInChildren<Text>().text == "")
                        spawnbutton[k].GetComponentInChildren<Text>().text = unit[idx];
                    else
                    {

                        obj[k] = Resources.Load(spawnbutton[k].GetComponentInChildren<Text>().text, typeof(GameObject)) as GameObject;

                        break;
                    }

                }
                Debug.Log("체인지완료");

            }
         
    }
    public void OnFirstUnitButtonClick()
    {
        if(mycost>cost)
        {
            Instantiate(obj[0], Points[spawnidx].position,Points[spawnidx].rotation);
            mycost -= cost;

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