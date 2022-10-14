using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class ChoiceStage_Mng : MonoBehaviour
{
    public Button BackButton;
  
    private UnityAction backaction;
    void Start()
    {
        backaction = () => OnBackButtonClick();
       
        BackButton.onClick.AddListener(backaction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("MainHome_Scene");
    }
}
