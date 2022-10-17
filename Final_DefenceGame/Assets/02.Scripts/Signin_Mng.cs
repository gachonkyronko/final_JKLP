using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class Signin_Mng : MonoBehaviour
{
     
    public InputField EmailInput;
    public InputField PasswordInput;
    public bool loginok = false;
    public static string myID="";
    public static string myEmail = "";
    public static string myPassword = "";

    public void SigninBtn() //로그인버튼
    {
        myEmail = EmailInput.text;
        myPassword = PasswordInput.text;
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { loginok = true; myID = result.PlayFabId;  }, (error) => print("로그인 실패"));
        
       
    }
    

     


        private void Update() //로그인버튼활성화시 바로 씬 넘어가지도록
    {
        if(loginok==true)
        {
            SceneManager.LoadScene("SigninHome_Scene");
        }
    }
    public void SignupBtn() //회원가입화면이동
    {
        SceneManager.LoadScene("Signup_Scene");
    }

    
}
