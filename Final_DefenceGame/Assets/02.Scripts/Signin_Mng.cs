using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class Signin_Mng : MonoBehaviour
{
    public InputField EmailInput, PasswordInput;
    [SerializeField] InputField emailField;
    [SerializeField] InputField passField;
   
    public Button SigninButton;
    private UnityAction signinaction;
    public Button SignupButton;
    private UnityAction signupaction;
    public bool loginok = false;
 
    public void LoginBtn()
    {
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => loginok = true , (error) => print("로그인 실패"));
    }
    private void Update()
    {
        if(loginok==true)
        {
            SceneManager.LoadScene("SigninHome_Scene");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        signinaction = () => OnSigninClick();
        signupaction = () => OnSignupClick();
         
        SignupButton.onClick.AddListener(signupaction);
    }

    public void OnSigninClick()
    {
        SceneManager.LoadScene("SigninHome_Scene");
    }
    public void OnSignupClick()
    {
        SceneManager.LoadScene("Signup_Scene");
    }
}
