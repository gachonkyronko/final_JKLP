using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindUnits : MonoBehaviour
{
    MyUnit myunit;
    // Start is called before the first frame update
    void Start()
    {
        int number=0;
        myunit = GameObject.Find("GameObject").GetComponent<MyUnit>();
        myunit.FindDic(number).Print();
        // 자료형 변수이름 = myunit.FindDic(ID값).변수이름 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
