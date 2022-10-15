using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    [SerializeField] //태어날 위치 지정 
    Transform[] Points;
    public GameObject HumanPrefab; 
    public Button firstSpawnButton;
    public UnityAction firstSpawnaction;
    private int spawnidx = 0;
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
         


    }
    public void OnFirstSpawnButtonClick()
    {
        spawnidx = 1;
       
        Instantiate(HumanPrefab, Points[spawnidx].position,
            Points[spawnidx].rotation);
    }
     
    
}