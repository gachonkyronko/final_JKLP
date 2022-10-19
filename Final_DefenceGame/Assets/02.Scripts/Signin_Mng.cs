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
    //    // 제공되는 함수 : 이메일과 비밀번호로 로그인 시켜 줌
       
    //        task => {
    //            if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
    //            {
                    
    //                Debug.Log(emailField.text + " 로 로그인 하셨습니다.");
    //                SigninButton.onClick.AddListener(signinaction);
    //                loginok = true;
    //            }
    //            else
    //            {
    //                Debug.Log("로그인에 실패하셨습니다.");
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
