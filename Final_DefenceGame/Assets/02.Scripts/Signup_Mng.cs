using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Signup_Mng : MonoBehaviour
{
    [SerializeField] InputField emailField;
    [SerializeField] InputField passField;
    public Button SignupButton;
    public Button BackButton;
    private UnityAction signupaction;
    private UnityAction backaction;
    // 인증을 관리할 객체
    Firebase.Auth.FirebaseAuth auth;

    void Awake()
    {
        // 객체 초기화
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    
    public void register()
    {
        // 제공되는 함수 : 이메일과 비밀번호로 회원가입 시켜 줌
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passField.text).ContinueWith(
            task => {
                if (!task.IsCanceled && !task.IsFaulted)
                {

                    Debug.Log(emailField.text + "로 회원가입\n");
                    SignupButton.onClick.AddListener(signupaction);
                }
                else
                    Debug.Log("회원가입 실패\n");
            }
            );
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
