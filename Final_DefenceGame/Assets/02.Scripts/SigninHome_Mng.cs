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
    public Text titleTxt;
     
    void Start() //타이틀이름설정
    {
        print(Signin_Mng.myID);
        titleTxt = GameObject.Find("Canvas_Title").transform.GetChild(0).GetComponent<Text>();
         

    }
    private void Update()
    {
        titleTxt.text = Signin_Mng.myName.ToString() + "님 어서오세요!";
    }

    public void OnnewgameButtonClick() //새게임 이벤트
    {
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