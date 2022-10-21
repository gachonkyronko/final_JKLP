using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    void Start()
    {             
        Points = GameObject.Find("Spqwn").GetComponentsInChildren<Transform>();
        
        timePrev = Time.time;  
        
        countdowntext.text = setTime.ToString();
        var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
        PlayFabClientAPI.GetUserData(request1, (result) => { unit[0] = result.Data["Vampire"].Value; Debug.Log("뱀파이어넘어옴"); saveunit++; unitCreate();   }, (error) => print("데이터못넘김"));
        PlayFabClientAPI.GetUserData(request1, (result) => { unit[1] = result.Data["Tiger"].Value; Debug.Log("타이거넘어옴"); saveunit++; unitCreate();  }, (error) => print("데이터못넘김"));
         
    }
    void unitCreate()
    {
        if (saveunit == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    int idx = Random.Range(0, 10);
                    if (spawnbutton[i].GetComponentInChildren<Text>().text == "Button" || spawnbutton[i].GetComponentInChildren<Text>().text == "")
                        spawnbutton[i].GetComponentInChildren<Text>().text = unit[idx];
                    else
                    {

                        obj[i] = Resources.Load(spawnbutton[i].GetComponentInChildren<Text>().text, typeof(GameObject)) as GameObject;

                        break;
                    }

                }
                Debug.Log(i + "/" + spawnbutton[i].GetComponentInChildren<Text>().text);

            }
        }
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
                    int idx = Random.Range(0, 10);
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