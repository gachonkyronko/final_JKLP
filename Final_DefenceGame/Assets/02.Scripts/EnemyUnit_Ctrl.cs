using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyUnit_Ctrl : MonoBehaviour
{
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
    private float traceDist = 15.0f; //추적 거리 
    bool isDie = false;
    private float attackDist = 3.0f; //공격 거리 
    //ZombieDamage z_damage;
    // Start is called before the first frame update
    void Start()
    {
        humanTr = GameObject.FindWithTag("Human").GetComponent<Transform>();
        
        enemyTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        navi = GetComponent<NavMeshAgent>();
        //z_damage = GetComponent<ZombieDamage>();
        
        //추적 대상   = 플레이어 위치 
        StartCoroutine(CheckEnemyState()); //스타트 코루틴 
        StartCoroutine(EnemyAction());
    }
    IEnumerator CheckEnemyState() //Update() 함수 대신 무한 반복 하기 위해서 선언 
    {
        while (isDie == false)
        {
            yield return new WaitForSeconds(0.3f);

            float dist_human = Vector3.Distance(enemyTr.position, humanTr.position);
            if (dist_human <= attackDist)
            {
                state = State.ATTACK;
            }


            //else if ( dist_human <= traceDist)
            //{
            //    state = State.TRACE;
            //}
        }
    }
    IEnumerator EnemyAction()
    {
        while (!isDie)
        {
            switch (state)
            {

                case State.TRACE:

                    navi.SetDestination(humanTr.position);
                    enemyTr.LookAt(humanTr.transform);
                    navi.isStopped = false;
                    animator.SetBool("IsAttack", false);

                    break;

                case State.ATTACK:

                    enemyTr.LookAt(humanTr.transform);

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
}