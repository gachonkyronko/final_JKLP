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

    void Start()
    {
        print(Signin_Mng.myID);
        titleTxt = GameObject.Find("Canvas_Title").transform.GetChild(0).GetComponent<Text>();
        var request = new LoginWithEmailAddressRequest { Email = Signin_Mng.myEmail, Password = Signin_Mng.myPassword };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { print("로그인성공"); }, (error) => print("로그인실패"));
        var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
        PlayFabClientAPI.GetUserData(request1, (result) => { myName = result.Data["닉네임"].Value; nameText(); }, (error) => print("데이터못넘김"));
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
    }
}