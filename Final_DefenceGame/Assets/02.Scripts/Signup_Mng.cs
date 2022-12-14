using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
 
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
public class Signup_Mng : MonoBehaviour
{
    
    public InputField EmailInput;
    public InputField PasswordInput;
    public InputField UsernameInput;
    public bool signupok = false;
    public bool signupfinish = false;
    public bool nicknamefinish = false;
    public string Username = "";
    public string stage = "0";

    public void RegisterBtn()  
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => { signupfinish = true; nickname(); }, (error) => print("실패"));
        Username = UsernameInput.text;
    }


    
    void Update()   //회원가입과정중 데이터가 DB서버로 넘어가는 것을 스킵하지않도록 변수지정
    {
         
            if (signupok == true)
                SceneManager.LoadScene("Signin_Scene");
    }
    void nickname()
    {
        if (signupfinish == true)
        {
            var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
            PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { nicknamefinish = true; print(" 성공"); nicknameok(); }, (error) => print("실패"));

        }
    }
    void nicknameok()
    {
        if (nicknamefinish == true)
        {

            var request1 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "닉네임", Username } } };
            PlayFabClientAPI.UpdateUserData(request1, (result) => { print("성공"); signupok = true; }, (error) => print("실패"));
            var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "스테이지", stage } } };
            PlayFabClientAPI.UpdateUserData(request2, (result) => { print("성공"); signupok = true; }, (error) => print("실패"));
            var request = new AddUserVirtualCurrencyRequest() { VirtualCurrency = "GD", Amount = 1000 };
            PlayFabClientAPI.AddUserVirtualCurrency(request, (result) => signupok = true, (error) => print("돈 얻기 실패"));
        }
    }

        public void BackBtn() //뒤로가기
    {
        SceneManager.LoadScene("Signin_Scene");
    }





}