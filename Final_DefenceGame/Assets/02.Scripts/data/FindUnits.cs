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
        // �ڷ��� �����̸� = myunit.FindDic(ID��).�����̸� 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
