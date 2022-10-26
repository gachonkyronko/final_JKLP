using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class FristStage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.win == true)
        {
            var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "스테이지", "1" } } };
            PlayFabClientAPI.UpdateUserData(request2, (result) => { print("성공"); }, (error) => print("실패"));
        }
    }
}
