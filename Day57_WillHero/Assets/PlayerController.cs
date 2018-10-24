using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    public float dashDistance = 3f;
    public bool playableOn = false;
    public bool dashOn = true;
    public Transform weaponHolder;
    public GameObject javelinPrefab;
    public float slidingSpeed;

    Rigidbody rb;
    Coroutine coDash;
    
    EZObjectPool objectPool;

    public event System.Action OnDeath;

    Jumping jumping;

    [SerializeField]
    bool isSliding = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectPool = GetComponent<EZObjectPool>();
        jumping = GetComponent<Jumping>();
    }

    private void Update()
    {
        if(playableOn && (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            if (coDash != null)
                StopCoroutine(coDash);
            if (dashOn && !isSliding)
                coDash = StartCoroutine(Dash(dashDistance,0.1f));
            if (isSliding)
            {
                rb.velocity +=/* Physics.gravity * -jumping.jumpHeight */ Vector3.up* Mathf.Sqrt(jumping.jumpHeight * -2 * Physics.gravity.y);
                print(rb.velocity);
            }
            FireWeapon();
        }
        print(rb.velocity);
    }

    public void FirstDash()
    {
        coDash = StartCoroutine(Dash(dashDistance, 0.2f));
        FireWeapon();
    }

    private void FireWeapon()
    {
        /*
        GameObject weaponBullet = Instantiate(javelinPrefab,
                                              weaponHolder.transform.position,
                                              weaponHolder.transform.rotation);
                                            //Quaternion.AngleAxis(30,Vector3.forward));
        weaponBullet.GetComponent<JavelinController>().Trow();
        Destroy(weaponBullet, 10f);
        */

        GameObject weaponBullet;
        if(objectPool.TryGetNextObject( weaponHolder.transform.position,
                                        weaponHolder.transform.rotation,
                                        out weaponBullet))
        {
            weaponBullet.GetComponent<JavelinController>().Trow();
        }
        else
        {
            print("There is no pool object");
        }
    }

    IEnumerator Dash(float distance, float duration)
    {
        float d = 0;
        while (d < distance)
        {
            rb.velocity = Vector3.zero;
            float tempDistance = distance / duration * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + Vector3.right * tempDistance);
            d += tempDistance;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            RaycastHit hit;
            Ray r = new Ray(transform.position, Vector3.right);
            Debug.DrawLine(transform.position, transform.right, Color.magenta, 1f);
            bool isInFrontOfMe = Physics.Raycast(r, out hit, 1f, 1 << LayerMask.NameToLayer("PushBoxMob")/*shift 연산 PushBoxMob의 레이어 번호 14*/); /* 1 << LayerMask.NameToLayer("PushBoxMob") | 1 << LayerMask.NameToLayer("Ground") 두개연산 */
            //                                                   00000000000000000100000000000000
            // Hero 는 collider 이 없고 자식인 pushBox 가 box collider 를 가지고 있지만 rigidbody 있는곳으로 충돌정보가 날라옴
            if (isInFrontOfMe)
            {
                StartCoroutine(KnockBack(-transform.right, 2f, 0.1f));
            }
        }
    }

    private IEnumerator KnockBack(Vector3 dir, float distance, float duration)
    {
        float d = 0;
        while(d < distance)
        {
            rb.velocity = Vector3.zero;
            float tempDistance = distance / duration * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + dir * tempDistance);
            d += tempDistance;
            print("KnockBack");
            yield return new WaitForFixedUpdate();
        }
    }
    
    internal void RingOut()
    {
        if (null != OnDeath)
            OnDeath();
    }

    internal void Beaten()
    {
        Death();
        
        if (null != OnDeath)
            OnDeath();
    }

    void Death()
    {
        playableOn = jumping.loopOn = false;
        transform.Find("Model").DOScaleY(0.2f, 0.5f);
    }

    private void FixedUpdate()
    {
        CheckSliding();
    }

    private void CheckSliding()
    {
        if (!jumping.isGrounded && rb.velocity.y <= 0)
        {
            RaycastHit hitT;
            Ray rT = new Ray(transform.position + Vector3.up * 0.5f, Vector3.right);
            Debug.DrawLine(rT.origin, rT.origin + rT.direction * .65f, Color.magenta, Time.fixedDeltaTime);
            bool isInFrontOfMeTop = Physics.Raycast(rT,
                                                 out hitT,
                                                 0.5f + 0.15f,  //컴파일할때 미리 계산 해줌
                                                 1 << LayerMask.NameToLayer("Ground"),//중요 까먹지말것
                                                 QueryTriggerInteraction.Ignore
                                                 );

            RaycastHit hitB;
            Ray rB = new Ray(transform.position + Vector3.down * 0.5f, Vector3.right);
            Debug.DrawLine(rB.origin, rB.origin + rB.direction * .65f, Color.cyan, Time.fixedDeltaTime);
            bool isInFrontOfMeBot = Physics.Raycast(rB,
                                                 out hitB,
                                                 0.5f + 0.15f,  //컴파일할때 미리 계산 해줌
                                                 1 << LayerMask.NameToLayer("Ground"),//중요 까먹지말것
                                                 QueryTriggerInteraction.Ignore
                                                 );

            if (isInFrontOfMeTop || isInFrontOfMeBot)
            {
                RaycastHit hit = isInFrontOfMeTop ? hitT : hitB;
                rb.useGravity = false;
                Vector3 pos = transform.position;
                float properDistance = 0.55f;
                if (hit.distance < properDistance)
                    pos.x -= properDistance - hit.distance;
                rb.MovePosition(pos + Vector3.down * slidingSpeed * Time.fixedDeltaTime);
                isSliding = true;
            }
            else
            {
                rb.useGravity = true;
                isSliding = false;
            }
        }
        else
        {
            rb.useGravity = true;
            isSliding = false;
        }
    }
}
