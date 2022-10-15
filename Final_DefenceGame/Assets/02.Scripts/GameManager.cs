using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    //3초마다 10마리 
    private float timePrev;
    void Start()
    {            //자기자신 부터 하위 자식들 트랜스폼 컴퍼넌트 
        Points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
        //하이라키에서 SpawnPoint 라는 오브젝트 명을 찾고 그 하위 자식 트랜스폼 컴포넌트들을 
        //몽땅 Points라는 트랜스폼 배열에 넣는 다.    
        timePrev = Time.time; //현재시간을 대입
        firstSpawnaction = () => OnFirstSpawnButtonClick();
        firstSpawnButton.onClick.AddListener(firstSpawnaction);
        secondSpawnaction = () => OnSecondSpawnButtonClick();
        secondSpawnButton.onClick.AddListener(secondSpawnaction);
        firstunitaction = () => OnFirstUnitButtonClick();
        firstunitButton.onClick.AddListener(firstunitaction);
        menuaction = () => OnMenuButtonClick();
        menuButton.onClick.AddListener(menuaction);
        menubackaction = () => OnMenuBackButtonClick();
        menubackButton.onClick.AddListener(menubackaction);
        menuhomeaction = () => OnMenuHomeButtonClick();
        menuhomeButton.onClick.AddListener(menuhomeaction);
        countdowntext.text = setTime.ToString();
    }
    void Update()
    {      //현재시간 - 과거시 = 흘러간 시간 
           //if (Time.time - timePrev > 2.0f)
           //{                     //하이라키에서 "Zombie"태그를 가진 갯수를 넘긴다. 
           //    int zombieCount = GameObject.FindGameObjectsWithTag("Zombie").Length;
           //    if (zombieCount < 5)
           //        CreateZomBie();
           //    timePrev = Time.time;
           //}
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