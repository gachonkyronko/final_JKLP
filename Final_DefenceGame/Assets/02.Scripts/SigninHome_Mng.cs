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
    public string myName = "";
    public string myStage = "";
    void Start()
    {
        print(Signin_Mng.myID);
        titleTxt = GameObject.Find("Canvas_Title").transform.GetChild(0).GetComponent<Text>();
        var request = new LoginWithEmailAddressRequest { Email = Signin_Mng.myEmail, Password = Signin_Mng.myPassword };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { print("로그인성공"); }, (error) => print("로그인실패"));
        var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
        PlayFabClientAPI.GetUserData(request1, (result) => { myName = result.Data["닉네임"].Value; nameText(); }, (error) => print("데이터못넘김"));
        PlayFabClientAPI.GetUserData(request1, (result) => { myStage = result.Data["스테이지"].Value;  }, (error) => print("데이터못넘김"));
    }


    // Update is called once per frame
    void Update()
    {
        

    }
    void nameText()
    {

        titleTxt.text = myName.ToString() + "님 어서오세요!";
    }


    public void OnnewgameButtonClick()
    {
        SceneManager.LoadScene("MainHome_Scene");
        //만약 저장된 데이터가 있다면 저장된 데이터가 있습니다. 새게임을 진행하시겠습니까? 를 출력하면 좋을드스.
    }
    public void OnloadGameButtonClick()
    {
        if (myStage == "0")
        {
            Debug.Log("저장된 데이터가 없습니다.");
        }
        else
            SceneManager.LoadScene("MainHome_Scene");
    }
}