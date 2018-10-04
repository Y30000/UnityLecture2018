using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class HitReaction : MonoBehaviour {

    Rigidbody rb;
    Health health;

	void Start () {
		rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        health.OnDeath += Die;


    }

    private void Die()
    {
        print("Die!!");
        transform.Find("PushBox").GetComponent<BoxCollider>().enabled = false;  //바로자식
        transform.GetComponent<Jumping>().loopOn = false;
        transform.Find("Model").GetComponent<MeshRenderer>().material.color = Color.red;
        rb.AddForce(Vector3.right * 10f, ForceMode.VelocityChange);
        
        rb.freezeRotation = false;
        rb.AddTorque(Vector3.back * 20f, ForceMode.VelocityChange);

//      transform.DORotate(-Vector3.forward * 720, 2, RotateMode.FastBeyond360).OnComplete(() => { print("completed!"); } /*람다 완료뒤 호출*/);
    }

    public void Beaten()
    {
        health.DecreaseHP(10);
        StartCoroutine(KnockBack(transform.right, 0.4f, 0.05f));
    }

    private IEnumerator KnockBack(Vector3 dir, float distance, float duration)
    {
        float d = 0;
        while (d < distance)
        {
            rb.velocity = Vector3.zero;
            float tempDistance = distance / duration * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + dir * tempDistance);
            d += tempDistance;
            yield return new WaitForFixedUpdate();
        }
    }

}
