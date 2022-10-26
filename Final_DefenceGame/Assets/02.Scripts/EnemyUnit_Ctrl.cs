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
    public enum State //������ ��� 
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
    private float traceDist = 0.0f; //���� �Ÿ� 
    bool isDie = false;
    public float attackDist = 0.0f; //���� �Ÿ� 
    //ZombieDamage z_damage;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("������Ȯ��@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

        //humanTr = GetComponent<Transform>();
        //enemyTr = GameObject.FindWithTag("Human").GetComponent<Transform>();

        animator = GetComponent<Animator>();
        navi = GetComponent<NavMeshAgent>();
        myName = gameObject.name;
        Invoke("mystat", 5.0f);




        //    StartCoroutine(CheckHumanState_1()); //��ŸƮ �ڷ�ƾ 
        //    StartCoroutine(HumanAction_1());


        //    //z_damage = GetComponent<ZombieDamage>();

        //    //���� ���   = �÷��̾� ��ġ 

        //}


        //IEnumerator CheckHumanState_1() //Update() �Լ� ��� ���� �ݺ� �ϱ� ���ؼ� ���� 
        //{
        //    while (isDie == false)
        //    {
        //        yield return new WaitForSeconds(0.3f);

        //        float dist_Enemy = Vector3.Distance(enemyTr.position, humanTr.position);
        //        if (dist_Enemy <= attackDist)
        //        {
        //            state = State.ATTACK;
        //        }


        //        //else if ( dist_human <= traceDist)
        //        //{
        //        //    state = State.TRACE;
        //        //}
        //    }
        //}
        //IEnumerator HumanAction_1()
        //{
        //    while (!isDie)
        //    {
        //        switch (state)
        //        {

        //            case State.TRACE:

        //                navi.SetDestination(enemyTr.position);
        //                humanTr.LookAt(enemyTr.transform);
        //                navi.isStopped = false;
        //                animator.SetBool("IsAttack", false);

        //                break;

        //            case State.ATTACK:

        //                humanTr.LookAt(enemyTr.transform);

        //                navi.isStopped = true;
        //                animator.SetBool("IsAttack", true);

        //                break;

        //            case State.DIE:
        //                // z_damage.Die();
        //                break;
        //        }
        //        yield return new WaitForSeconds(0.3f);

        //    }
        //}


        //private void OnDrawGizmos()
        //{
        //    if (state == State.TRACE)
        //    {
        //        Gizmos.color = Color.blue;
        //        Gizmos.DrawWireSphere(transform.position, traceDist);

        //    }
        //    if (state == State.ATTACK)
        //    {
        //        Gizmos.color = Color.red;
        //        Gizmos.DrawWireSphere(transform.position, attackDist);
        //    }



        //}
    }
    public void mystat()
    {
        Debug.Log("����Ȯ��");
       
        int cutClone = name.IndexOf("(Clone)");
        string Cutname = name.Substring(0, cutClone);
        Debug.Log("�� ������ �̸� : " + Cutname);
        for (int j = 0; j < getdamage.humanlen; j++)
        {


            if (getdamage.humanName[j] == Cutname)
            {
                Debug.Log("�׽�Ʈ123 : " + getdamage.humanName[j]);
                RANGE = int.Parse(getdamage.humanattackrange[j]);
                MOVESPD = double.Parse(getdamage.humanymovepseed[j]);
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

