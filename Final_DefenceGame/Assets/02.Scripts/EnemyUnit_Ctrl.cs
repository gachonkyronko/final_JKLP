using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyUnit_Ctrl : MonoBehaviour
{
    public int RANGE = 0;
    public double MOVESPD = 0;
    public string myName = "";
    public double attackrange = 0;
    public enum State //열거형 상수 
    {
        TRACE, ATTACK, DIE, IDLE
    }
    public State state = State.IDLE;

    private string ttag = "Human"; 
    private Transform target;
    private Transform closestEnemy = null; 
    private float dist =20.0f;
     
 

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
    public float attackDist = 0.0f; //공격 거리 
    //ZombieDamage z_damage;
    // Start is called before the first frame update
    void Start()
    {
       

        animator = GetComponent<Animator>();
        navi = GetComponent<NavMeshAgent>();
        myName = gameObject.name;
        Invoke("mystat", 5.0f);
       
        InvokeRepeating("colstart", 11.0f, 3.0f);






        //z_damage = GetComponent<ZombieDamage>();

        //추적 대상   = 플레이어 위치 

    }
   

    IEnumerator CheckHumanState_1() //Update() 함수 대신 무한 반복 하기 위해서 선언 
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
               
                if (dist < attackDist +1 )
                {
                    state = State.ATTACK;
                    closestEnemy = taggedEnemy.transform;
                }
                else if(dist < traceDist+50)
                {

                    state = State.TRACE;
                    closestEnemy = taggedEnemy.transform;


                }
                else
                    state = State.IDLE;
            }
            target = closestEnemy;

            yield return new WaitForSeconds(0.3f);

            //float dist_Enemy = Vector3.Distance(enemyTr.position, humanTr.position);
            //if (dist_Enemy <= attackDist)
            //{
            //    state = State.ATTACK;
            //}


            //else if (dist_Enemy <= traceDist)
            //{
            //    state = State.TRACE;
            //}

            //else
            //{
            //    state = State.IDLE;
            //}
        }
    }
    IEnumerator HumanAction_1()
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
    //void getClosestEnemy()
    //{        //비용이 큼 - 특정 태그의 오브젝트를 모두 찾음        
    //    GameObject[] taggedEnemys = GameObject.FindGameObjectsWithTag(ttag);
    //    float closestDistSqr = Mathf.Infinity; //infinity 실제값?        
    //    Transform closestEnemy = null;
    //    foreach (GameObject taggedEnemy in taggedEnemys)
    //    {
    //        Vector3 objectPos = taggedEnemy.transform.position; 
    //        dist = (objectPos - transform.position).sqrMagnitude;
    //        //원주민이 특정 거리 안으로 들어올때            
    //        if (dist <attackDist)
    //        {                // 그 거리가 제곱한 최단 거리보다 작으면            
    //            if (dist < closestDistSqr)
    //            {
    //                closestDistSqr = dist;
    //                closestEnemy = taggedEnemy.transform;
    //            }
    //        }
    //    }
    //    target = closestEnemy;
    //}

    void colstart()
    {
        if (HumanUnit_Ctrl.unitspawn == true)
        {
            humanTr = GetComponent<Transform>();
            enemyTr = GameObject.FindWithTag("Human").GetComponent<Transform>();
            StartCoroutine(CheckHumanState_1()); //스타트 코루틴 
            StartCoroutine(HumanAction_1());
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
        
        for (int j = 0; j < getdamage.humanlen; j++)
        {


            if (getdamage.humanName[j] == Cutname)
            {
                
                RANGE = int.Parse(getdamage.humanattackrange[j]);
                MOVESPD = double.Parse(getdamage.humanymovepseed[j])*3;
                attackrange = double.Parse(getdamage.humanattackrange[j]);
                attackDist = ((float)attackrange);
                navi.speed = ((float)MOVESPD);

            }


        }
        attackDist = RANGE;
         

    }
    private void Update()
    {
        
      
    }
}

