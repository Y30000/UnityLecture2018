using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    bool onGround;
    float jumpPressure;
    float minJumpPressure;
    float maxJumpPressure;

    Rigidbody rb;
    Animator anim;

	// Use this for initialization
	void Start () {
        onGround = true;
        jumpPressure = 0;
        minJumpPressure = 2f;
        maxJumpPressure = 10f;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (onGround)
        {
            if (Input.GetButton("Jump"))
            {
                if (jumpPressure < maxJumpPressure)
                    jumpPressure += 10f * Time.deltaTime;
                else
                    jumpPressure = maxJumpPressure;
                anim.speed = 1f + (jumpPressure/10);
                anim.SetFloat("jumpPressure", jumpPressure);
            }
            else
            {
                if(jumpPressure > 0)
                {
                    jumpPressure += minJumpPressure;
                    rb.velocity = Vector3.up * jumpPressure;
                    jumpPressure = 0;
                    onGround = false;
                    anim.SetFloat("jumpPressure", jumpPressure);
                    anim.SetBool("onGround", onGround);
                }
            }
        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            anim.SetBool("onGround", onGround);
        }
    }
}
