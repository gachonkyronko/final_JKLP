using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
public class SigninHome_Mng : MonoBehaviour
{
    public static Text titleTxt;
    public Canvas newgame;
    public string[] enemyattack = new string[50];
    public string[] enemyattackspeed = new string[50];
    public string[] enemyattackrange = new string[50];
    public string[] enemymovepseed = new string[50];
    public static string[] unitstatsdata = new string[50];
    public static string[] unitstatsdatatag = new string[50];
    public string testTxt = "";
    
    void Start() //Ÿ��Ʋ�̸�����
    {
         
        titleTxt = GameObject.Find("Canvas_Title").transform.GetChild(1).GetComponent<Text>();
         
        
    }
    private void Update() //Ÿ��Ʋ�̸�����
    {
        testTxt = Signin_Mng.myName;
        titleTxt.text = Signin_Mng.myName.ToString() + "�� �������!";
    }

    public void OnnewgameButtonClick() //������ �̺�Ʈ
    {
        if (Signin_Mng.myStage == "0")
        {

            SceneManager.LoadScene("MainHome_Scene");
        }
        else
        {
            newgame.gameObject.SetActive(true);
        }
         
    }
    public void startnewgame() //������ �̺�Ʈ, ���������� 1�� �ʱ�ȭ �ȴ�.
    {
        var request1 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "��������", "0" } } };
        PlayFabClientAPI.UpdateUserData(request1, (result) => { print("���������ʱ�ȭ"); }, (error) => print("����"));
        SceneManager.LoadScene("MainHome_Scene");
         
    }
    public void OnloadGameButtonClick() //����� �����Ͱ� ������ ������ ������ä�� �̵�.
    {
        if (Signin_Mng.myStage == "0")
        {

            Debug.Log("����� �����Ͱ� �����ϴ�.");
        }
        else
            SceneManager.LoadScene("MainHome_Scene");
    }
}