using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
public class SigninHome_Mng : MonoBehaviour
{
    public static Text titleTxt;
    public string[] enemyattack = new string[50];
    public string[] enemyattackspeed = new string[50];
    public string[] enemyattackrange = new string[50];
    public string[] enemymovepseed = new string[50];
    public static string[] unitstatsdata = new string[50];
    public static string[] unitstatsdatatag = new string[50];
    void Start() //타이틀이름설정
    {
        print(Signin_Mng.myID);
        titleTxt = GameObject.Find("Canvas_Title").transform.GetChild(0).GetComponent<Text>();

        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Main" }, (result) =>
        {
            int k = 0;
            Debug.Log(unitstatsdata.Length);
            for (int j = 0; j < unitstatsdata.Length; j++)
            {
                Debug.Log(unitstatsdata.Length);
                for (int i = 0; i < result.Catalog.Count; i++)
                {
                    Debug.Log(unitstatsdata.Length);
                    if (result.Catalog[i].DisplayName == "Vampire")
                    {
                        k = i;
                        Debug.Log("------");
                        //unitstatsdatatag[j] = result.Catalog[k].Tags[0];
                        Debug.Log(j);
                        Debug.Log(k);
                        Debug.Log(result.Catalog[k].Tags[1]);
                        Debug.Log(enemyattack[0]);
                        enemyattack[j] = result.Catalog[k].Tags[1];
                        enemyattackspeed[j] = result.Catalog[k].Tags[2];
                        enemyattackrange[j] = result.Catalog[k].Tags[3];
                        enemymovepseed[j] = result.Catalog[k].Tags[4];
                        Debug.Log("------");
                        Debug.Log(enemyattack[j]);
                        Debug.Log(enemyattackspeed[j]);
                        Debug.Log(enemyattackrange[j]);
                        Debug.Log(enemymovepseed[j]);


                    }


                }
            }



        },
   (error) => print("실패"));
    }
    private void Update()
    {
        titleTxt.text = Signin_Mng.myName.ToString() + "님 어서오세요!";
    }

    public void OnnewgameButtonClick() //새게임 이벤트
    {
        SceneManager.LoadScene("MainHome_Scene");
        //만약 저장된 데이터가 있다면 저장된 데이터가 있습니다. 새게임을 진행하시겠습니까? 를 출력하면 좋을드스.
    }
    public void OnloadGameButtonClick() //기존게임불러오기 이벤트
    {
        if (Signin_Mng.myStage == "0")
        {
            Debug.Log("저장된 데이터가 없습니다.");
        }
        else
            SceneManager.LoadScene("MainHome_Scene");
    }
}