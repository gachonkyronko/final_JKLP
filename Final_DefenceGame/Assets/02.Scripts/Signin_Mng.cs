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

    public void SigninBtn() //�α��ι�ư
    {
        myEmail = EmailInput.text;
        myPassword = PasswordInput.text;
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { loginok = true; myID = result.PlayFabId;  }, (error) => print("�α��� ����"));
        
       
    }
    

     


        private void Update() //�α��ι�ưȰ��ȭ�� �ٷ� �� �Ѿ������
    {
        if(loginok==true)
        {
            SceneManager.LoadScene("SigninHome_Scene");
        }
    }
    public void SignupBtn() //ȸ������ȭ���̵�
    {
        SceneManager.LoadScene("Signup_Scene");
    }

    
}
