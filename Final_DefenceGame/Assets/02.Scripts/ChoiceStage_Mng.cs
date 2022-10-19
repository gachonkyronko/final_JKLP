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
    public Button BackButton;
    private UnityAction backaction;
    public Button stage1;
    public Button stage2;
    public Button firstStageButton;
    private UnityAction firstStageaction;
    Transform[] Buttons;
    public string myStage = "";
    public int stage_num = 0;
    void Start()
    {
        backaction = () => OnBackButtonClick();
       
        BackButton.onClick.AddListener(backaction);

        firstStageaction = () => OnfirstStageButtonClick();

        firstStageButton.onClick.AddListener(firstStageaction);
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
        SceneManager.LoadScene("Battle_Scene");
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
