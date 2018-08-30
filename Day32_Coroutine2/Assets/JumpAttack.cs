using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(TripleJumpAttack());
	}

    IEnumerator TripleJumpAttack()
    {
        Jump(3f);
        yield return new WaitForSeconds(1.8f);
        Jump(3f);
        yield return new WaitForSeconds(1.8f);
        yield return WheelWindJump(5f);
    }

    IEnumerator WheelWindJump(float v)
    {
        Jump(v);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Vector3.up * 100f;
        print("WheelWindJump");
        yield return null;

        rb.angularDrag = 1f;
        while (true)
        {
            if (rb.angularVelocity.magnitude < 1)
                break;
            yield return null;
        }
        rb.angularVelocity = Vector3.zero;
        rb.angularDrag = 0.05f;
        print("WheelWindJump stopped");
    }

    void Jump(float height)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 velocity = rb.velocity;
        velocity.y = Mathf.Sqrt(2.0f * 9.8f * height);
        rb.velocity = velocity;
        print("Jump");
    }
}
