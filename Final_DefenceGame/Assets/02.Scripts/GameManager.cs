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
    public int mycost = 8;
    public GameObject[] spawnbutton = new GameObject[10];
    //3초마다 10마리 
    private float timePrev;
    public string[] unit = new string[10];
    void Start()
    {             
        Points = GameObject.Find("Spqwn").GetComponentsInChildren<Transform>();
          
        timePrev = Time.time;  
        
        countdowntext.text = setTime.ToString();
        var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
        PlayFabClientAPI.GetUserData(request1, (result) => { unit[0] = result.Data["Vampire"].Value; Debug.Log("뱀파이어넘어옴");  }, (error) => print("데이터못넘김"));
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
        //for ( int i = 0; i<10;i++)
        //{
        //    if( unit[i] == "1")
        //    {
        //        spawnbutton[i].GetComponentInChildren<Text>().text = unit[i];
        //    }
        //    //else
        //    //{

        //    //}
        //}
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
    public void OnFirstUnitButtonClick()
    {
        if(mycost>cost)
        {
            Instantiate(HumanPrefab, Points[spawnidx].position,
            Points[spawnidx].rotation);
            mycost -= cost;
        }
        else
        {
            Debug.Log("코스트가 부족합니다.");
        }
        
         
    }


}