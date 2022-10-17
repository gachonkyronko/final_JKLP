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

    public void RegisterBtn() //회원가입버튼
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => signupok = true, (error) => print("회원가입 실패"));
    }
    void Update() //회원가입성공시 화면이동
    {
        if (signupok == true)
            SceneManager.LoadScene("Signin_Scene");
    }
    public void BackBtn() //뒤로가기
    {
        SceneManager.LoadScene("Signin_Scene");
    }





}
