using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
public class Signup_Mng : MonoBehaviour
{
    [SerializeField] InputField emailField;
    [SerializeField] InputField passField;
    public Button SignupButton;
    public Button BackButton;
    private UnityAction signupaction;
    private UnityAction backaction;
    public bool signupok = false;


    public InputField EmailInput, PasswordInput, UsernameInput;

    public void RegisterBtn()
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => signupok = true, (error) => print("회원가입 실패"));
    }


    private void Start()
    {
        signupaction = () => OnSignupClick();
        backaction = () => OnBackClick();
        BackButton.onClick.AddListener(backaction);
    }

    // Update is called once per frame
    void Update()
    {
        if(signupok==true)
            SceneManager.LoadScene("Signin_Scene");
    }
    public void OnSignupClick()
    {
        SceneManager.LoadScene("Signin_Scene");
    }
    public void OnBackClick()
    {
        SceneManager.LoadScene("Signin_Scene");
    }
}
