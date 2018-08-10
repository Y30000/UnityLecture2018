using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 3f;
    public float rotationSpeed = 150f;

    CharacterController con;
    Vector3 moveDirection = Vector3.zero;
    Animator anim;

    float jumpSpeed = 15f;
    float gravity = 23f;
    float currentDownSpeed;

    MouseLook mouseLook;
	// Use this for initialization
	void Start () {
        con = GetComponent<CharacterController>();
        mouseLook = GetComponentInChildren<MouseLook>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouseMoveX = Input.GetAxis("Mouse X");
        float mouseMoveY = Input.GetAxis("Mouse Y");


        if (Input.GetMouseButton(1))
        {
            moveDirection = new Vector3(h, 0, v).normalized;
            float rotationY = mouseMoveX * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * rotationY);
            mouseLook.ResetCamera();

            if(h > 0)
            {
                anim.SetBool("isRight", true);
                anim.SetBool("isLeft", false);
            }
            else if(h < 0)
            {
                anim.SetBool("isRight", false);
                anim.SetBool("isLeft", true);
            }
            else
            {
                anim.SetBool("isRight", false);
                anim.SetBool("isLeft", false);
            }
        }
        else
        {
            moveDirection = new Vector3(0, 0, v).normalized;
            float rotationY = h * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * rotationY);
            anim.SetBool("isRight", false);
            anim.SetBool("isLeft", false);
        }

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed * 1.5f;

        if (Input.GetButtonDown("Jump"))
            anim.SetBool("isJump", true);

        if (Input.GetButtonUp("Jump"))
                Jump();

        currentDownSpeed -= gravity * Time.deltaTime;
        moveDirection.y = currentDownSpeed;

        con.Move(moveDirection * Time.deltaTime);
        anim.SetFloat("moveSpeed", con.velocity.magnitude);
        anim.SetBool("isGround", con.isGrounded);
    }

    void Jump()
    {
        if (!con.isGrounded)
            return;
        currentDownSpeed = jumpSpeed; 
        anim.SetBool("isJump",false);
    }
}
