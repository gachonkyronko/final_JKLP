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
    public static string myID = "";
    public static string myEmail = "";
    public static string myPassword = "";
    public static string myName = "";
    public static string myStage = "";


    private void Start()
    {
        for(int i =0; i<10;i++)
        {
            if(i==5)
            {
                i = 8;
            }
            print(i);
        }    
    }
    public void SigninBtn() //로그인버튼클릭이벤트 
    {
        myEmail = EmailInput.text;
        myPassword = PasswordInput.text;
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { loginok = true; myID = result.PlayFabId; }, (error) => print("로그인 실패"));
         
    }
   
    private void Update()  //로그인성공시 씬이동
    {
        if (loginok == true)
        {
            var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
            PlayFabClientAPI.GetUserData(request1, (result) => { myName = result.Data["닉네임"].Value; myStage = result.Data["스테이지"].Value; print(myName+myStage); }, (error) => print("데이터못넘김"));
            
            SceneManager.LoadScene("SigninHome_Scene");
        }
    }
   
    
    public void SignupBtn()  //회원가입씬으로 이동
    {
        SceneManager.LoadScene("Signup_Scene");
    }


}