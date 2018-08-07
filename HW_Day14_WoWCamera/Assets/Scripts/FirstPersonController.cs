using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

    public float moveSpeed = 8f;
    public float jumpForce = 250f;
    public float mouseSensitivityX = 2;
    public float mouseSensitivityY = 2;

    Vector3 moveDirection = Vector3.zero;

    Rigidbody rb;
 //   bool isHealing = false;
    private bool grounded = false;
 //   private GameObject fx;
    Transform cameraTransform;
    Transform CameraGizmoTransform;
    float cameraDistance;
    float verticalLookRotation = 0;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = GetComponentInChildren<Camera>().transform;   //아래 모두 훝어봄 //손자이하는 안봄
        CameraGizmoTransform = cameraTransform.GetComponentInParent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        //       transform.LookAt(transform.position + moveDirection);
  //      print("Mouse X " + Input.GetAxis("Mouse X"));

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);      //mouseSensitivityX 감도
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 90);
        //verticalLookRotation = Mathf.Clamp(verticalLookRotation + Input.GetAxis("Mouse Y") * mouseSensitivityY, -30, 30);

        cameraTransform.localEulerAngles = Vector3.right * -verticalLookRotation;       //부모관점에서 회전함
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
        }
       
    }

    private void FixedUpdate()
    {
        //   Vector3 move = transform.TransformDirection(moveDirection) * Time.fixedDeltaTime;
        Vector3 move = transform.localRotation * moveDirection * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }
}
