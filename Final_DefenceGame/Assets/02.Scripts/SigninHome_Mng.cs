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
    public string[] enemyattack = new string[50];
    public string[] enemyattackspeed = new string[50];
    public string[] enemyattackrange = new string[50];
    public string[] enemymovepseed = new string[50];
    public static string[] unitstatsdata = new string[50];
    public static string[] unitstatsdatatag = new string[50];
    void Start() //Ÿ��Ʋ�̸�����
    {
        print(Signin_Mng.myID);
        titleTxt = GameObject.Find("Canvas_Title").transform.GetChild(0).GetComponent<Text>();

        
    }
    private void Update()
    {
        titleTxt.text = Signin_Mng.myName.ToString() + "�� �������!";
    }

    public void OnnewgameButtonClick() //������ �̺�Ʈ
    {
        SceneManager.LoadScene("MainHome_Scene");
        //���� ����� �����Ͱ� �ִٸ� ����� �����Ͱ� �ֽ��ϴ�. �������� �����Ͻðڽ��ϱ�? �� ����ϸ� �����彺.
    }
    public void OnloadGameButtonClick() //�������Ӻҷ����� �̺�Ʈ
    {
        if (Signin_Mng.myStage == "0")
        {
            Debug.Log("����� �����Ͱ� �����ϴ�.");
        }
        else
            SceneManager.LoadScene("MainHome_Scene");
    }
}