using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class SigninHome_Mng : MonoBehaviour
{
    public Button newgameButton;

    public UnityAction newgameaction;

    void Start()
    {
        newgameaction = () => OnnewgameButtonClick();
        newgameButton.onClick.AddListener(newgameaction);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnnewgameButtonClick()
    {
        SceneManager.LoadScene("MainHome_Scene");
    }
}