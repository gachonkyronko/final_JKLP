using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossDamage : MonoBehaviour
{
    public Image Hpbar;
    private int CurHp = 0;
    private int MaxHp = 1000;
    private int damage = 15;
    private Text hpTxt;
    void Start()
    {
        Hpbar = GameObject.Find("Panel_Boss").transform.GetChild(1).GetComponent<Image>();
        hpTxt = GameObject.Find("Panel_Boss").transform.GetChild(0).GetComponent<Text>();
        Hpbar.color = Color.green;
        hpTxt.color = Color.black;
        CurHp = MaxHp;
        hpTxt.text = " Hp : " + CurHp.ToString();
    }
    //isTrigger 체크시 충돌 감지하는 콜백 함수  //통과 하면서 충돌 감지 한다.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Humanweapon")
        {
            CurHp -= damage;
            hpTxt.text = " Hp : " + CurHp.ToString();
            Hpbar.fillAmount = (float)CurHp / (float)MaxHp;

            if (Hpbar.fillAmount <= 0.0f)
                Hpbar.color = Color.clear;
            else if (Hpbar.fillAmount <= 0.3f)
                Hpbar.color = Color.red;
            else if (Hpbar.fillAmount <= 0.5f)
                Hpbar.color = Color.yellow;
            if (CurHp <= 0)
                PlayerDie();
        }

    }
    void PlayerDie()
    {
        Debug.Log("승리!!!");
    }
}