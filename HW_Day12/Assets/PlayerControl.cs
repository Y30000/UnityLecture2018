using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float moveSpeed = 10f;
    Animator anim;
    Rigidbody rb;

    bool isWarking = false;
    bool isGround = true;

    // Update is called once per frame
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(h, 0, v).normalized;

        if (moveDirection == Vector3.zero)
            isWarking = false;
        else
        {
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
            isWarking = true;
        }

        transform.LookAt(transform.position + moveDirection);
        anim.SetBool("IsWarking", isWarking);

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.velocity = Vector3.up * 10f;
            isGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGround = true;
    }
}
