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
    // ������ ������ ��ü
    Firebase.Auth.FirebaseAuth auth;

    void Awake()
    {
        // ��ü �ʱ�ȭ
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    
    public void register()
    {
        // �����Ǵ� �Լ� : �̸��ϰ� ��й�ȣ�� ȸ������ ���� ��
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passField.text).ContinueWith(
            task => {
                if (!task.IsCanceled && !task.IsFaulted)
                {

                    Debug.Log(emailField.text + "�� ȸ������\n");
                    SignupButton.onClick.AddListener(signupaction);
                }
                else
                    Debug.Log("ȸ������ ����\n");
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
