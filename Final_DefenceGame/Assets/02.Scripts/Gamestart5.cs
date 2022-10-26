using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gamestart5 : MonoBehaviour
{
    public Text count5;
    public Canvas count5_canvas;
    private float count = 10.0f;
    public static bool countexit = false;
    void Start()
    {
        count5 = GetComponent<Text>();
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;

        count5.text = "게임시작 " + Mathf.Round(count) + "초 전";
        if (count <= 0)
        {
            Time.timeScale = 1.0f;
            count5_canvas.gameObject.SetActive(false);
            countexit = true;
        }
            

    }
}
