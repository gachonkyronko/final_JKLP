using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Signin_Mng : MonoBehaviour
{
    [SerializeField] InputField emailField;
    [SerializeField] InputField passField;
    Firebase.Auth.FirebaseAuth auth;
    public Button SigninButton;
    private UnityAction signinaction;
    public Button SignupButton;
    private UnityAction signupaction;
    void Awake()
    {
        // ��ü �ʱ�ȭ
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    public void login()
    {
        // �����Ǵ� �Լ� : �̸��ϰ� ��й�ȣ�� �α��� ���� ��
        auth.SignInWithEmailAndPasswordAsync(emailField.text, passField.text).ContinueWith(
            task => {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    SigninButton.onClick.AddListener(signinaction);
                    Debug.Log(emailField.text + " �� �α��� �ϼ̽��ϴ�.");
                     
                }
                else
                {
                    Debug.Log("�α��ο� �����ϼ̽��ϴ�.");
                }
            }
        );
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
