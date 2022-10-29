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
    public State state = State.IDLE;

    [SerializeField]
    private NavMeshAgent navi;
    [SerializeField]
    private Transform humanTr;
    [SerializeField]
    private Transform enemyTr;
    [SerializeField]
    private Animator animator;
    private float traceDist = 10.0f; //추적 거리 
    bool isDie = false;
    public  float attackDist = 0.0f; //공격 거리 
    Vector3 destination = new Vector3(-0.15f, 0.58f, -1.07f);

    private string ttag = "Enemy";
    private Transform target;
    private Transform closestEnemy = null;
    private float dist = 20.0f;
    //ZombieDamage z_damage;
    // Start is called before the first frame update
    void Start()
    {
        enemyTr = GameObject.FindWithTag("Enemy").GetComponent<Transform>();
        unitspawn = true;
        humanTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        navi = GetComponent<NavMeshAgent>();
        myName = gameObject.name;
        Invoke("mystat", 0.5f);
        //z_damage = GetComponent<ZombieDamage>();
        InvokeRepeating("CheckHumanState", 11.0f, 1.0f);
        InvokeRepeating("HumanAction", 11.0f, 1.0f);
        //추적 대상   = 플레이어 위치 
        

    }
    

    IEnumerator CheckHumanState() //Update() 함수 대신 무한 반복 하기 위해서 선언 
    {
        while (isDie == false)
        {

            GameObject[] taggedEnemys = GameObject.FindGameObjectsWithTag(ttag);

            Transform closestEnemy = null;
            foreach (GameObject taggedEnemy in taggedEnemys)
            {
                Vector3 objectPos = taggedEnemy.transform.position;
                dist = (objectPos - transform.position).sqrMagnitude;
                //원주민이 특정 거리 안으로 들어올때         
              
                if (dist < attackDist + 1)
                {
                    state = State.ATTACK;
                    closestEnemy = taggedEnemy.transform;
                }
                else if (dist < traceDist + 50)
                {

                    state = State.TRACE;
                    closestEnemy = taggedEnemy.transform;


                }
                else
                    state = State.IDLE;
            }
            target = closestEnemy;

            yield return new WaitForSeconds(0.3f);

           
        }
    }
    IEnumerator HumanAction()
    {
        while (!isDie)
        {
            switch (state)
            {

                case State.TRACE:

                    navi.SetDestination(target.position);
                    humanTr.LookAt(target.transform);
                    //navi.SetDestination(enemyTr.position);
                    //humanTr.LookAt(enemyTr.transform);
                    navi.isStopped = false;
                    animator.SetBool("IsTrace", true);
                    animator.SetBool("IsAttack", false);

                    break;

                case State.ATTACK:

                    humanTr.LookAt(target.transform);
                    //humanTr.LookAt(enemyTr.transform);

                    navi.isStopped = true;
                    animator.SetBool("IsAttack", true);

                    break;
                case State.IDLE:
                    animator.SetBool("IsTrace", false);
                    break;

                case State.DIE:
                    // z_damage.Die();
                    break;
            }
            yield return new WaitForSeconds(0.3f);

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
                    MOVESPD = double.Parse(getdamage.enemymovepseed[i])*3;
                    navi.speed = ((float)MOVESPD);
                    
                    break;
                      
   
}
            }
        attackDist = RANGE;
        StartCoroutine(CheckHumanState()); //스타트 코루틴 
        StartCoroutine(HumanAction());
        navi.SetDestination(destination);
        humanTr.LookAt(destination);
       

    }
}

 