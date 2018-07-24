using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour {
    /* OnCollisionEnter 발생조건
     * 1. 두 개의 gameObject 모두 collider component가 존재해야 한다.
     * 2. 둘 중 하나는 rigidbody component가 있어야 한다.
     * 3. 그리고 rigibody를 가진 gameobject가 움직여야 충돌되었을 때 발생한다. //버그? 그 반대는 10%로 발생한다
    */
    private void OnCollisionEnter(Collision collision)      //callback 함수 다른놈이 불러줌 // 상대방쪽에도 이 함수 불러줌
    {
        print("OnCollisionEnter" + collision.gameObject.name);
        foreach(ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.magenta, 5f);
        }
    }
}
