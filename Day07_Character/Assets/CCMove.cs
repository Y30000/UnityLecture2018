using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMove : MonoBehaviour {
    public float moveSpeed = 8f;
    
    CharacterController con;
    Vector3 moveDirection = Vector3.zero;

    public float jumpSpeed = 8f;
    public float gravity = 20f;

    // Use this for initialization
    void Start () {
        con = GetComponent<CharacterController>();
	}
	
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
        con.Move(moveDirection * Time.deltaTime);
    }

    public GameObject healFX;
    bool isHealing = false;
    GameObject fx;
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
    }
    void RemoveHealFX()
    {
        isHealing = false;
    }
}
