using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour {

    public float jumpHeight = 4f;
    public BoxCollider groundCheckBox;
    public LayerMask groundMask;
    public bool drawGizmo = true;
    public bool loopOn = true;
    public bool isGrounded = false;

    Rigidbody rb;
    bool queuedJumpingForce = false;
    ColliderState state;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void OnDrawGizmos()
    {
        if (!drawGizmo)
            return;

        CheckGizmoColor();
        Gizmos.matrix = groundCheckBox.transform.localToWorldMatrix;
        Gizmos.DrawCube(groundCheckBox.center, groundCheckBox.size);//로컬꺼 사용
    }

    private void CheckGizmoColor()
    {
        switch (state)
        {
            case ColliderState.Open:
                Gizmos.color = Color.green;
                break;
            case ColliderState.Colliding:
                Gizmos.color = Color.magenta;
                break;
        }
    }

    void FixedUpdate()
    {
        bool overlapsGround;              //groundCheckBox.transform.position 과 같음
        overlapsGround = Physics.CheckBox(  groundCheckBox.transform.TransformPoint(groundCheckBox.center),
                                            groundCheckBox.size * .5f,
                                            groundCheckBox.transform.rotation,
                                            groundMask,
                                            QueryTriggerInteraction.Ignore  //트리거 생략 또는 무시
                                            );
        isGrounded = overlapsGround;

        if (isGrounded)
            state = ColliderState.Colliding;
        else
            state = ColliderState.Open;

        if(isGrounded && loopOn && !queuedJumpingForce)
        {
            Invoke("Jump", 0.15f);
            queuedJumpingForce = true;
        }
    }

    void Jump()
    {
        queuedJumpingForce = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); //only y value
      //rb.velocity += Vector3.up * Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y); 같음
        if (!isGrounded)
            return;

        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y), ForceMode.VelocityChange);
    }
}
