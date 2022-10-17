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
    public string Username="";
    public void RegisterBtn() //회원가입버튼
    {
         
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text};
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => { signupfinish = true; nickname(); }, (error) => print("회원가입 실패")) ;
        Username = UsernameInput.text;
        
         
    }
    void Update() //회원가입성공시 화면이동
    {
        if (signupok == true)
            SceneManager.LoadScene("Signin_Scene");
         
    }
    void nickname()
    {
        if (signupfinish == true)
        {
            var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
            PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { nicknamefinish = true; print("로그인성공");nicknameok();  }, (error) => print("로그인 실패"));
             
        }
    }
    void nicknameok()
    {
        if(nicknamefinish == true)
        {
             
            var request1 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "닉네임", Username } } };
            PlayFabClientAPI.UpdateUserData(request1, (result) => { print("데이터 저장 성공");signupok = true; }, (error) => print("데이터 저장 실패"));
        }
    }
    public void BackBtn() //뒤로가기
    {
        SceneManager.LoadScene("Signin_Scene");
    }





}
