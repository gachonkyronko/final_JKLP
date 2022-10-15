using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StoreScene_Mng : MonoBehaviour
{
    public Button UnitButton1;
    public Button UnitButton2;
    public Button UnitButton3;
    public Button UnitButton4;
    public Button reinforceButton1;
    public Button reinforceButton2;
    public Button reinforceButton3;
    public Button reinforceButton4;
    private UnityAction backaction;
    public Button backButton;
    // Start is called before the first frame update
    void Start()
    {
        backaction = () => OnBackbuttonClick();
        backButton.onClick.AddListener(backaction);
    }

    public void OnBackbuttonClick()
    {
        SceneManager.LoadScene("MainHome_Scene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
