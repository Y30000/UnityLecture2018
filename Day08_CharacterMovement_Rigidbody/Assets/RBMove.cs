using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBMove : MonoBehaviour {

    public float moveSpeed = 8f;
    public float jumpForce = 250f;
    public GameObject healFX;

    Vector3 moveDirection = Vector3.zero;

    Rigidbody rb;
    bool isHealing = false;
    private bool grounded = false;
    private GameObject fx;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(transform.position, -transform.up);       //Ray(시작 점 vector3, 방향 vector3)
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1 + 0.1f))    // 캐릭터 바닥까지 거리 + 0.1f
            grounded = true;
        else
            grounded = false;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * jumpForce);
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDirection = (new Vector3(h, 0, v)).normalized;
        moveDirection *= moveSpeed;
        transform.LookAt(transform.position + moveDirection);
    }

    private void FixedUpdate()
    {
        Vector3 move = moveDirection * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    /*
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.name == "Step3")
        {
            if (!isHealing)
            {
                fx = Instantiate(healFX, transform.Find("FXPos"));
                isHealing = true;
                Invoke("RemoveHealFX", 1.9f);
            }
        }
    }

    void RemoveHealFX()
    {
        Destroy(fx);
        isHealing = false;
    }
    */
}
