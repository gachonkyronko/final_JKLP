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
    public Button secondSpawnButton;
    public UnityAction secondSpawnaction;
    public Button firstunitButton;
    public UnityAction firstunitaction;
    private int spawnidx = 1;

    public float setTime = 120.0f;
    public Text countdowntext;
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
        secondSpawnaction = () => OnSecondSpawnButtonClick();
        secondSpawnButton.onClick.AddListener(secondSpawnaction);
        firstunitaction = () => OnFirstUnitButtonClick();
        firstunitButton.onClick.AddListener(firstunitaction);
        countdowntext.text = setTime.ToString();
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
        setTime -= Time.deltaTime;
        countdowntext.text = "���� �ð� : " + Mathf.Round(setTime).ToString();


    }
    public void OnFirstSpawnButtonClick()
    {
        spawnidx = 1;
        firstSpawnButton.image.color = Color.gray;
        secondSpawnButton.image.color = Color.white;
    }
    public void OnSecondSpawnButtonClick()
    {
        spawnidx = 2;
        firstSpawnButton.image.color = Color.white;
        secondSpawnButton.image.color = Color.gray;
    }
    public void OnFirstUnitButtonClick()
    {
        

        Instantiate(HumanPrefab, Points[spawnidx].position,
            Points[spawnidx].rotation);
    }


}