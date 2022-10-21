using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
public class ChoiceStage_Mng : MonoBehaviour
{
    
    public Button stage1;
    public Button stage2;
    
    Transform[] Buttons;
    public string myStage = "";
    public int stage_num = 0;
    void Start()
    {
       
   
        Buttons = GameObject.Find("Canvas_Choicemenu").GetComponentsInChildren<Transform>();
        var request2 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
        PlayFabClientAPI.GetUserData(request2, (result) => { myStage = result.Data["스테이지"].Value; stageColor(); }, (error) => print("데이터못넘김"));
    }
     


    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("MainHome_Scene");
    }
    public void OnfirstStageButtonClick()
    {
        SceneManager.LoadScene("FirstStage");
    }
    public void stageColor()
    {

        stage_num = int.Parse(myStage);
        switch (stage_num)
        {
            case 0:
                stage1.image.color = Color.white;
                break;
            case 1:
                stage2.image.color = Color.white;
                break;
            default:
                
                break;
             
        }

    }
}
