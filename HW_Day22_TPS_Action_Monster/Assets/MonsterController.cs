using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {

    public Transform wayPointsRoot;
    public int currentPoint = 0;
    public float moveSpeed = 2;
    public Transform player;

    /*private*/
    List<Transform> wayPoints;
    Vector3 nextPoint;

    Animator anim;
    NavMeshAgent agent;
    //Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        anim.SetBool("isWalking", true);

        wayPoints = new List<Transform>();
        foreach (Transform t in wayPointsRoot)      //자식들에 대하여 Enumerable 함 IEnumerable
            wayPoints.Add(t);
        if (currentPoint >= wayPoints.Count)
            currentPoint = 0;
        nextPoint = wayPoints[currentPoint].transform.position;

        // for gizmo

        for (int i = 0; i < wayPoints.Count; ++i)
            wayPoints[i].GetComponent<MeshRenderer>().material.color = Color.magenta;
        wayPoints[currentPoint].GetComponent<MeshRenderer>().material.color = Color.yellow;

        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        if (direction.magnitude < 15 && angle < 60)
        {
            direction.y = 0;
            anim.SetBool("isIdle", false);
            if (direction.magnitude < 3)
            {
                // attack
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
                agent.destination = transform.position;
            }
            else
            {
                // Chase

                if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.Walk"))   //현재 애니메이션이 Base Layer의 Walk 일때만
                {
                    //transform.Translate(0, 0, 0.05f);
                    //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                    agent.destination = player.position;
                }

                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", true);
            }
        }
        else
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);

            if (wayPoints.Count > 1)
            {
                float dist = Vector3.Distance(transform.position, nextPoint);
                if (dist < 0.5f)
                {
                    currentPoint++;
                    if (currentPoint >= wayPoints.Count)
                        currentPoint = 0;
                    nextPoint = wayPoints[currentPoint].transform.position;

                    //for gizmo
                    wayPoints[currentPoint].GetComponent<MeshRenderer>().material.color = Color.yellow;
                    if (currentPoint == 0)
                        wayPoints[wayPoints.Count - 1].GetComponent<MeshRenderer>().material.color = Color.magenta;
                    else
                        wayPoints[currentPoint - 1].GetComponent<MeshRenderer>().material.color = Color.magenta;
                }

                //        transform.position = Vector3.MoveTowards(transform.position, nextPoint, moveSpeed * Time.deltaTime);      //겹치도록 보장해줌
                Vector3 dir = nextPoint - transform.position;
                dir.y = 0;
                float distance = dir.magnitude;

                agent.destination = nextPoint;

                //transform.position = Vector3.Lerp(transform.position, nextPoint, moveSpeed * Time.deltaTime / distance /*한 프레임당 이동할 거리의 % distance(총 거리)를 나눠줌으로써 등속이동 가능*/ /* 0.1f /*0~1 사이 범위 % 둘사이의 x%만큼 이동함*/  /*moveSpeed * Time.deltaTime*/);
                //transform.position = Vector3.Slerp(transform.position, nextPoint, moveSpeed * Time.deltaTime / distance /*한 프레임당 이동할 거리의 % distance(총 거리)를 나눠줌으로써 등속이동 가능*/ /* 0.1f /*0~1 사이 범위 % 둘사이의 x%만큼 이동함*/  /*moveSpeed * Time.deltaTime*/);

                ////transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);

                //transform.LookAt(nextPoint);    //보간 없는 회전
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up/*defoult 값 = up*/), 0.15f/*15%씩*/);
                ////transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up/*defoult 값 = up*/), 0.15f/*15%씩*/);
                //spherical linear interpolation, 구체 선형 보간법 (호를 그리며 이동) //rotation 은 Slerp가 더 낫다?
                //speedAngle = 120
                //speedAngle * Time.deltaTime / angle   //등각속도운동


            }
        }
    }


    private void FixedUpdate()
    {
        Vector3 moveVect = agent.velocity.normalized - transform.forward;
        float angle = Mathf.Atan2(moveVect.y, moveVect.x);
        anim.SetFloat("Angle", angle);
    }
}
