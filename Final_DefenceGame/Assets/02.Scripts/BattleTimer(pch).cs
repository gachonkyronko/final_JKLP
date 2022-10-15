using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleTimer : MonoBehaviour
{
    public float setTime = 20.0f;
    public Text countdowntext;
    // Start is called before the first frame update
    void Start()
    {
        countdowntext.text = setTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        setTime -= Time.deltaTime;
        countdowntext.text = setTime.ToString();
        if (setTime <= 0)
            Debug.Log("ÆÐ¹è");
    }
}
