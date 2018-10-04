using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb;

    bool isGround;
    bool isDash;
    bool canDash;
    bool isStepBack;
    bool isSlip;

    int dashCounter;
    float globalTime;

    float dashTime;
    float dashLength;
    float jumpingPower;
    float jumpWaitingTime;
    float remainJumpingTime;
    float stepBackScale;
    float drag;
    
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

        isGround = false;
        isDash = false;
        canDash = true;
        isStepBack = false;
        isSlip = false;

        dashCounter = 1;
        globalTime = Time.time;

        dashTime = 0.2f;
        dashLength = 50;
        jumpingPower = 3;
        remainJumpingTime = jumpWaitingTime = .2f;
        stepBackScale = 0.5f;
        drag = 10f;
    }

    public void Update()
    {
        if (canDash && Input.GetKeyDown(KeyCode.Space))
        {
            if (isSlip)
            {
                remainJumpingTime = 0;
                isGround = true;
            }
            else
                DashStart();
        }
        else if (isGround)
        {
            if(remainJumpingTime > 0)
                remainJumpingTime -= Time.deltaTime;
            else
            {
                rb.gravityScale = 1f;
                remainJumpingTime = jumpWaitingTime;
                isGround = false;
                if(isSlip)
                    rb.velocity += Vector2.up *  Mathf.Sqrt(jumpingPower * drag * -2 * Physics2D.gravity.y);
                else
                    rb.velocity += Vector2.up * Mathf.Sqrt(jumpingPower * -2 * Physics2D.gravity.y);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDash)
            Dash();
        else if (isStepBack)
            StepBack();

        if (transform.position.x > 500)
        {
            Vector3 pos = transform.position;
            pos.x = 0;
            transform.position = pos;
        }

        if (!canDash && Time.time > globalTime - dashTime + dashTime * dashCounter / 5)
        {
            canDash = true;
        }
    }

    public void DashStart()
    {
        canDash = false;
        rb.gravityScale = 0;
        globalTime = Time.time + dashTime;
        isDash = true;
        ++dashCounter;
    }

    public void Dash()
    {
        if (globalTime > Time.time)
        {
            rb.MovePosition(transform.position + Vector3.right * dashLength * Time.fixedDeltaTime);
        }
        else
        {
            isDash = false;
            rb.gravityScale = 1;
        }
    }

    public void StepBackStart()
    {
        isDash = false;
        isStepBack = true;
        globalTime = Time.time + dashTime * stepBackScale;
        Vector2 vel = rb.velocity;
        vel.y = 0;
        rb.velocity = vel;
    }

    public void StepBack()
    {
        if (globalTime > Time.time)
        {
            rb.MovePosition(transform.position + Vector3.left * dashLength * Time.fixedDeltaTime);
        }
        else
        {
            isStepBack = false;
            rb.gravityScale = 1;
            canDash = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        Vector2 vv = Vector2.zero;

        foreach (var contact in collision.contacts)
        {
            vv += contact.point;
        }
        vv /= collision.contacts.Length;
        vv = (Vector2) transform.position - vv;

        if (Mathf.Abs(transform.position.y - collision.transform.position.y) >= 0.95)
        {
            isGround = true;
            dashCounter = 1;
        }
        else
        {

        }
        */

        Vector2 vv = Vector2.zero;

        foreach (var contact in collision.contacts)
        {
            vv += contact.normal;
        }
        /*
        vv /= collision.contacts.Length;
        vv -= (Vector2)transform.position;
        */

        if (vv.x < -0.9)
        {
            foreach (var contact in collision.contacts)
                if (contact.collider.tag == "Mob")
                {
                    StepBackStart();
                    break;
                }
        }

        if (vv.y > 0.9)
        {
            canDash = isGround = true;
            dashCounter = 1;
        }
        else if (vv.y < -0.9 && collision.collider.tag == "Mob")
        {
            Die();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 vv = Vector2.zero;

        foreach (var contact in collision.contacts)
        {
            vv += contact.normal;
        }

        if (collision.collider.tag == "Mob")
            return;

        if (vv.x < -0.9 && rb.velocity.y < 0)
        {
            isSlip = true;
            //rb.gravityScale = 0.2f;
            rb.drag = drag;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isSlip = false;
        rb.gravityScale = 1f;
        rb.drag = 0;
    }

    void Die()
    {
        transform.DOScaleY(0.2f, .5f);
        enabled = false;
    }
}