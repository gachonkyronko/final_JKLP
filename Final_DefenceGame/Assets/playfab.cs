using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class playfab : MonoBehaviour
{
    public InputField EmailInput, PasswordInput, UsernameInput;


    


    public void RegisterBtn()
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => print("회원가입 성공"), (error) => print("회원가입 실패"));
    }
}