using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class MainHome_Mng : MonoBehaviour
{
    public Text titleTxt;

    void Start() //Ÿ��Ʋ�̸�����
    {
        print(Signin_Mng.myID);
        titleTxt = GameObject.Find("Canvas_Title").transform.GetChild(1).GetComponent<Text>();
        

    }


    void Update()
    {
        titleTxt.text = Signin_Mng.myName.ToString() + "�� ������ �غ����ּ���!";
    }
    public void OnStageButtonClick() //����������ư�̺�Ʈ
    {

        SceneManager.LoadScene("ChoiceStage_Scene");
    }
    public void OnStoreButtonClick() //������ư�̺�Ʈ
    {
        SceneManager.LoadScene("Store_Scene");
    }
}
