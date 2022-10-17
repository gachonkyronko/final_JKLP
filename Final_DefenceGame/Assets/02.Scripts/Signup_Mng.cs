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

    public void RegisterBtn() //ȸ�����Թ�ư
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => signupok = true, (error) => print("ȸ������ ����"));
    }
    void Update() //ȸ�����Լ����� ȭ���̵�
    {
        if (signupok == true)
            SceneManager.LoadScene("Signin_Scene");
    }
    public void BackBtn() //�ڷΰ���
    {
        SceneManager.LoadScene("Signin_Scene");
    }





}
