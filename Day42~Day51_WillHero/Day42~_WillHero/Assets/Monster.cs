using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    private bool isGround;
    private float remainJumpingTime;
    private Rigidbody2D rb;
    private float jumpingPower;
    private float jumpWaitingTime;
    private float drag;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        isGround = false;
        remainJumpingTime = 1;
        jumpingPower = 3;
        jumpWaitingTime = .5f;
        drag = 1;

    }
	
	// Update is called once per frame
	void Update () {
        if (isGround)
        {
            if (remainJumpingTime > 0)
                remainJumpingTime -= Time.deltaTime;
            else
            {
                remainJumpingTime = jumpWaitingTime;
                isGround = false;
                rb.drag = 0;
                rb.velocity += Vector2.up * Mathf.Sqrt(jumpingPower * -2 * Physics2D.gravity.y);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 vv = Vector2.zero;

        foreach (var contact in collision.contacts)
        {
            vv += contact.normal;
        }

        if (vv.y > 0.9)
        {
            isGround = true;
            rb.drag = drag;
        }
    }
}
