using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HumanUnit_Ctrl : MonoBehaviour
{
    
    public int RANGE = 0;
    public double MOVESPD = 0;
    public string myName = "";
    public double attackrange = 0;
    public enum State //열거형 상수 
    {
        TRACE, ATTACK, DIE, IDLE
    }
    public State state = State.TRACE;

    [SerializeField]
    private NavMeshAgent navi;
    [SerializeField]
    private Transform humanTr;
    [SerializeField]
    private Transform enemyTr;
    [SerializeField]
    private Animator animator;
    private float traceDist = 0.0f; //추적 거리 
    bool isDie = false;
    public  float attackDist = 0.0f; //공격 거리 
    //ZombieDamage z_damage;
    // Start is called before the first frame update
    void Start()
    {
        enemyTr = GameObject.FindWithTag("Enemy").GetComponent<Transform>();

        humanTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        navi = GetComponent<NavMeshAgent>();
        myName = gameObject.name;
        Invoke("mystat", 0.5f);
        //z_damage = GetComponent<ZombieDamage>();
        
        //추적 대상   = 플레이어 위치 
            
    }
    

    IEnumerator CheckHumanState() //Update() 함수 대신 무한 반복 하기 위해서 선언 
    {
        while (isDie == false)
        {
            yield return new WaitForSeconds(0.3f);

            float dist_Enemy = Vector3.Distance(enemyTr.position, humanTr.position);
            if (dist_Enemy <= attackDist)
            {
                state = State.ATTACK;
            }


            //else if ( dist_human <= traceDist)
            //{
            //    state = State.TRACE;
            //}
        }
    }
    IEnumerator HumanAction()
    {
        while (!isDie)
        {
            switch (state)
            {

                case State.TRACE:

                    navi.SetDestination(enemyTr.position);
                    humanTr.LookAt(enemyTr.transform);
                    navi.isStopped = false;
                    animator.SetBool("IsAttack", false);

                    break;

                case State.ATTACK:

                    humanTr.LookAt(enemyTr.transform);

                    navi.isStopped = true;
                    animator.SetBool("IsAttack", true);

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
            Debug.Log("이 유닛의 이름 : " + Cutname);
            for (int i = 0; i < getdamage.realLen; i++)
            {
                if (getdamage.enemyName[i] == Cutname)
                {
                    attackrange = double.Parse(getdamage.enemyattackrange[i]);
                attackDist = ((float)attackrange);
                RANGE = int.Parse(getdamage.enemyattackrange[i]);
                    MOVESPD = double.Parse(getdamage.enemymovepseed[i]);
                    navi.speed = ((float)MOVESPD);
                    
                    break;
                      
   
}
            }
        attackDist = RANGE;
        StartCoroutine(CheckHumanState()); //스타트 코루틴 
        StartCoroutine(HumanAction());

    }
}

 