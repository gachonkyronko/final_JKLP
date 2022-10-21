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
    public Text titleTxt;
     
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