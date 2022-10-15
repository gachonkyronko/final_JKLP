using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    [SerializeField] //�¾ ��ġ ���� 
    Transform[] Points;
    public GameObject HumanPrefab; 
    public Button firstSpawnButton;
    public UnityAction firstSpawnaction;
    private int spawnidx = 0;
    //3�ʸ��� 10���� 
    private float timePrev;
    void Start()
    {            //�ڱ��ڽ� ���� ���� �ڽĵ� Ʈ������ ���۳�Ʈ 
        Points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
        //���̶�Ű���� SpawnPoint ��� ������Ʈ ���� ã�� �� ���� �ڽ� Ʈ������ ������Ʈ���� 
        //���� Points��� Ʈ������ �迭�� �ִ� ��.    
        timePrev = Time.time; //����ð��� ����
        firstSpawnaction = () => OnFirstSpawnButtonClick();
        firstSpawnButton.onClick.AddListener(firstSpawnaction);

    }
    void Update()
    {      //����ð� - ���Ž� = �귯�� �ð� 
        //if (Time.time - timePrev > 2.0f)
        //{                     //���̶�Ű���� "Zombie"�±׸� ���� ������ �ѱ��. 
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