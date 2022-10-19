using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Signin_Mng : MonoBehaviour
{
    [SerializeField] InputField emailField;
    [SerializeField] InputField passField;
    
    public Button SigninButton;
    private UnityAction signinaction;
    public Button SignupButton;
    private UnityAction signupaction;
    public bool loginok = false;
   
    //public void login()
    //{
    //    // �����Ǵ� �Լ� : �̸��ϰ� ��й�ȣ�� �α��� ���� ��
       
    //        task => {
    //            if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
    //            {
                    
    //                Debug.Log(emailField.text + " �� �α��� �ϼ̽��ϴ�.");
    //                SigninButton.onClick.AddListener(signinaction);
    //                loginok = true;
    //            }
    //            else
    //            {
    //                Debug.Log("�α��ο� �����ϼ̽��ϴ�.");
    //            }
    //        }
    //    );
    //}
    private void Update()
    {
        if(loginok==true)
        {
            SceneManager.LoadScene("SigninHome_Scene_1");
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
        SceneManager.LoadScene("SigninHome_Scene_1");
    }
    public void OnSignupClick()
    {
        SceneManager.LoadScene("Signup_Scene");
    }
}
