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
    public void RegisterBtn() //ȸ�����Թ�ư
    {
         
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text};
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => { signupfinish = true; nickname(); }, (error) => print("ȸ������ ����")) ;
        Username = UsernameInput.text;
        
         
    }
    void Update() //ȸ�����Լ����� ȭ���̵�
    {
        if (signupok == true)
            SceneManager.LoadScene("Signin_Scene");
         
    }
    void nickname()
    {
        if (signupfinish == true)
        {
            var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
            PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { nicknamefinish = true; print("�α��μ���");nicknameok();  }, (error) => print("�α��� ����"));
             
        }
    }
    void nicknameok()
    {
        if(nicknamefinish == true)
        {
             
            var request1 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "�г���", Username } } };
            PlayFabClientAPI.UpdateUserData(request1, (result) => { print("������ ���� ����");signupok = true; }, (error) => print("������ ���� ����"));
        }
    }
    public void BackBtn() //�ڷΰ���
    {
        SceneManager.LoadScene("Signin_Scene");
    }





}
