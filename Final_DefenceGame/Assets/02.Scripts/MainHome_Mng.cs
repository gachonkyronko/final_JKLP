using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class MainHome_Mng : MonoBehaviour
{
    public Button StageButton;
    public Button StoreButton;
    public UnityAction storeaction;
    private UnityAction stageaction;
    void Start()
    {
        stageaction = () => OnStageButtonClick();
        StageButton.onClick.AddListener(stageaction);
        storeaction = () => OnStoreButtonClick();
        StoreButton.onClick.AddListener(storeaction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStageButtonClick()
    {
        SceneManager.LoadScene("ChoiceStage_Scene");
    }
    public void OnStoreButtonClick()
    {
        SceneManager.LoadScene("Store_Scene");
    }
}
