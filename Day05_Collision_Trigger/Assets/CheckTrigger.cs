using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : MonoBehaviour {
    /* OnTriggerEnter 발생 조건
     * 1. 두 개의 gameObject 모두 collider component가 존재해야 한다.
     * 2. 둘 중 하나는 rigidbody component가 있어야 한다.
     * 3. 둘 중 하나는 collider component에 Is Trigger가 On 되어야 한다.
     * 4. 그리고 어느 쪽이 움직이더라도 서로 만나면 이벤트가 발생한다.
     * 5. OnTriggerEnter 발생 시 OnCollisionEnter가 발생하지 않는다.
     * **쌍방이다**
     */

    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter: " + other.gameObject.name);
    }
}
