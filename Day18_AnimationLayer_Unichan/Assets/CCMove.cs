using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CCMove : MonoBehaviour {
    public float moveSpeed = 8f;
    
    CharacterController con;
    Vector3 moveDirection = Vector3.zero;

    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public float slideSpeed = 3f;

    Animator anim;
    // Use this for initialization
    void Start () {
        con = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
	}

    bool isOnSlope;
    

    // Update is called once per frame
    void Update () {
        if (con.isGrounded)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            moveDirection = (new Vector3(h, 0, v)).normalized;
            transform.LookAt(transform.position + moveDirection);
            moveDirection *= moveSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        

        isOnSlope = Vector3.Angle(hitNomal, transform.up) > con.slopeLimit;
        Vector3 slideDriction = Vector3.zero;

        if (isOnSlope)
        {
            //1.approximation sliding vector
            //slideDriction = (new Vector3(hitNomal.x, 0, hitNomal.z) * slideSpeed);
            
            //2. accurate sliding vector
            Vector3 c = Vector3.Cross(hitNomal, Vector3.up);
            slideDriction = Vector3.Cross(hitNomal,c) * slideSpeed;

            Debug.DrawRay(hitPoint, slideDriction, Color.magenta, 3f);

            transform.LookAt(transform.position + new Vector3(slideDriction.x,0, slideDriction.z));
        }
        
        con.Move( (moveDirection + slideDriction) * Time.deltaTime);

        anim.SetFloat("Speed", con.velocity.magnitude);
        anim.SetBool("isHealing", isHealing);
        anim.SetBool("isSliding", isOnSlope);
    }

    public GameObject healFX;
    bool isHealing = false;

    Vector3 hitNomal;
    Vector3 hitPoint;
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.name == "Step3")
        {
            if (!isHealing)
            {
                Destroy(Instantiate(healFX, transform.Find("FXPos")), 1.9f);
                isHealing = true;
                Invoke("RemoveHealFX", 2.0f);       //(호출할 함수명, 일정시간 후)
            }
        }

        hitNomal = hit.normal;
        hitPoint = hit.point;
    }
    void RemoveHealFX()
    {
        isHealing = false;
    }

    /*
     * bool isSloping = false;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.name == "Step3")
        {
            if (!isHealing)
            {
                Destroy(Instantiate(healFX, transform.Find("FXPos")), 1.9f);
                isHealing = true;
                Invoke("RemoveHealFX", 2.0f);       //(호출할 함수명, 일정시간 후)
            }
        }
        Vector3 other = hit.namal;
        Vector3 here = GetComponent<Transform>().up;
  //      print(Vector3.Cross(Vector3.Cross(here, other), other).normalized);
        if (Vector3.Angle(other, here) > con.slopeLimit && !isSloping)
        {
            con.Move(Vector3.Cross(other,Vector3.Cross(here, other)).normalized * 1/50f);
            isSloping = true;
        }
    }
        private void FixedUpdate()
    {
        isSloping = false;
    }
     */
}
