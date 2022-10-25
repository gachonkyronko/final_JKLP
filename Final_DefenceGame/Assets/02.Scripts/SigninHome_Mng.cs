using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
public class SigninHome_Mng : MonoBehaviour
{
    public static Text titleTxt;
    public string[] enemyattack = new string[50];
    public string[] enemyattackspeed = new string[50];
    public string[] enemyattackrange = new string[50];
    public string[] enemymovepseed = new string[50];
    public static string[] unitstatsdata = new string[50];
    public static string[] unitstatsdatatag = new string[50];
    public string testTxt = "";
    public Canvas newgame;
    void Start() //타이틀이름설정
    {
        print(Signin_Mng.myID);
        titleTxt = GameObject.Find("Canvas_Title").transform.GetChild(1).GetComponent<Text>();
         
        
    }
    private void Update()
    {
        testTxt = Signin_Mng.myName;
        titleTxt.text = Signin_Mng.myName.ToString() + "님 어서오세요!";
    }

    public void OnnewgameButtonClick() //새게임 이벤트
    {
        if (Signin_Mng.myStage == "0")
        {

            SceneManager.LoadScene("MainHome_Scene");
        }
        else
        {
            newgame.gameObject.SetActive(true);
        }
         
    }
    public void startnewgame()
    {
        var request1 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "스테이지", "0" } } };
        PlayFabClientAPI.UpdateUserData(request1, (result) => { print("스테이지초기화"); }, (error) => print("실패"));
        SceneManager.LoadScene("MainHome_Scene");
        //만약 저장된 데이터가 있다면 저장된 데이터가 있습니다. 새게임을 진행하시겠습니까? 를 출력하면 좋을드스.
    }
    public void OnloadGameButtonClick() //기존게임불러오기 이벤트
    {
        if (Signin_Mng.myStage == "0")
        {

            Debug.Log("저장된 데이터가 없습니다.");
        }
        else
            SceneManager.LoadScene("MainHome_Scene");
    }
}