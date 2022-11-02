using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HumanUnit_Ctrl : MonoBehaviour
{
    public static bool unitspawn = false;
    public int RANGE = 0;
    public double MOVESPD = 0;
    public string myName = "";
    public double attackrange = 0;
    public enum State //열거형 상수 
    {
        TRACE, ATTACK, DIE, IDLE
    }
    public State state = State.TRACE;
    public List<GameObject> FoundObjects;
    public GameObject enemy;
    
    public float shortDis;
    [SerializeField]
    private NavMeshAgent navi;
    [SerializeField]
    private Transform humanTr;
    [SerializeField]
    private Transform enemyTr;
    [SerializeField]
    private Animator animator;
    private float traceDist =5.0f; //추적 거리 
    bool isDie = false;
    public  float attackDist = 0.0f; //공격 거리 
    Vector3 destination = new Vector3(-5.7f, 5.04f, -1.07f);

  
    private float dist = 20.0f;
   
    void Start()
    {
        enemyTr = GameObject.FindWithTag("Enemy").GetComponent<Transform>();
        unitspawn = true;
        humanTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        navi = GetComponent<NavMeshAgent>();
        myName = gameObject.name;
        navi.SetDestination(destination);
         
        Invoke("mystat", 0.5f);
     


    }


    IEnumerator CheckHumanState() //Update() 함수 대신 무한 반복 하기 위해서 선언 
    {
        while (isDie == false)
        {
            yield return new WaitForSeconds(0.3f);
           
            dist = Vector3.Distance(humanTr.position, enemy.transform.position);
            if (dist < attackDist )
            {
                state = State.ATTACK;

            }
            else if (dist < traceDist)
            {

                state = State.TRACE;



            }
            else

                state = State.IDLE;
        }
       
        yield return new WaitForSeconds(0.3f);

   
        }
    
    IEnumerator HumanAction()
    {
        while (!isDie)
        {
            switch (state)
            {

                case State.TRACE:
                    navi.SetDestination(enemy.transform.position);
                    humanTr.LookAt(enemy.transform);
                    
                    navi.isStopped = false;
                    animator.SetBool("IsTrace", true);
                    animator.SetBool("IsAttack", false);

                    break;

                case State.ATTACK:

                    humanTr.LookAt(enemy.transform);
                   

                    navi.isStopped = true;
                    animator.SetBool("IsAttack", true);

                    break;
                case State.IDLE:
                    animator.SetBool("IsTrace", false);
                    break;

                case State.DIE:
                   
                    break;
            }
            yield return new WaitForSeconds(0.3f);

        }
    }
    private void Update()
    {
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position);
        enemy = FoundObjects[0]; 

        foreach (GameObject found in FoundObjects)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < shortDis)  
            {
                enemy = found;
                shortDis = Distance;
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        if (state == State.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);

        }
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }



    }
    public void mystat()
    {

        int cutClone = name.IndexOf("(Clone)");
        string Cutname = name.Substring(0, cutClone);

        for (int i = 0; i < getdamage.realLen; i++)
        {
            if (getdamage.enemyName[i] == Cutname)
            {
                attackrange = double.Parse(getdamage.enemyattackrange[i]);
                attackDist = ((float)attackrange);
                RANGE = int.Parse(getdamage.enemyattackrange[i]);
                MOVESPD = double.Parse(getdamage.enemymovepseed[i]) * 10;
                navi.speed = ((float)MOVESPD);

                break;


            }
        }
        attackDist = RANGE;
        StartCoroutine(CheckHumanState());  
        StartCoroutine(HumanAction());



    }
}

 