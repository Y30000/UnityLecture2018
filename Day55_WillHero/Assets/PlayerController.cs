using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class PlayerController : MonoBehaviour {

    public float dashDistance = 3f;
    public bool playableOn = false;
    public bool dashOn = true;
    public Transform weaponHolder;
    public GameObject javelinPrefab;

    Rigidbody rb;
    Coroutine coDash;
    
    EZObjectPool objectPool;

    public event System.Action OnDeath;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectPool = GetComponent<EZObjectPool>();
    }

    private void Update()
    {
        if(playableOn && (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            if (coDash != null)
                StopCoroutine(coDash);
            if (dashOn)
                coDash = StartCoroutine(Dash(dashDistance,0.2f));
            FireWeapon();
        }
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
            yield return new WaitForFixedUpdate();
        }
    }
    
    internal void RingOut()
    {
        if (null != OnDeath)
            OnDeath();
    }
}
