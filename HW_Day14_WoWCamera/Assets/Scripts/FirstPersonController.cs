using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

    public float moveSpeed = 8f;
    public float jumpForce = 250f;
    public float mouseSensitivityX = 2;
    public float mouseSensitivityY = 2;
    public float rotateSeSensitivity = 2;

    Vector3 moveDirection = Vector3.zero;

    Rigidbody rb;
 //   bool isHealing = false;
    private bool grounded = false;
 //   private GameObject fx;
    Transform cameraTransform;
    Transform CameraGizmoTransform;
    float cameraDistance;
    float horizontalLookRotation = 0;
    float verticalLookRotation = 0;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = GetComponentInChildren<Camera>().transform;   //아래 모두 훝어봄 //손자이하는 안봄
        CameraGizmoTransform = cameraTransform.parent;
        /*
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        */
        print(CameraGizmoTransform.name);
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

        if (Input.GetMouseButton(0))
        {
            moveDirection = Vector3.forward * v * moveSpeed;
            transform.Rotate(Vector3.up, h * rotateSeSensitivity);

            horizontalLookRotation = Input.GetAxis("Mouse X") * mouseSensitivityX;
            verticalLookRotation = Input.GetAxis("Mouse Y") * mouseSensitivityY;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 90);
            //verticalLookRotation = Mathf.Clamp(verticalLookRotation + Input.GetAxis("Mouse Y") * mouseSensitivityY, -30, 30);

            CameraGizmoTransform.eulerAngles = CameraGizmoTransform.eulerAngles + Vector3.right * -verticalLookRotation + Vector3.up * horizontalLookRotation;       //부모관점에서 회전함
            CameraGizmoTransform.Rotate(Vector3.up, -h * rotateSeSensitivity);
            //CameraGizmoTransform.Rotate(0, horizontalLookRotation, 0);
            //CameraGizmoTransform.Rotate(-verticalLookRotation, 0, 0);
        }
        else if (Input.GetMouseButton(1))
        {
            moveDirection = (new Vector3(h, 0, v)).normalized;
            moveDirection *= moveSpeed;

            horizontalLookRotation = Input.GetAxis("Mouse X") * mouseSensitivityX;
            verticalLookRotation = Input.GetAxis("Mouse Y") * mouseSensitivityY;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 90);
            CameraGizmoTransform.eulerAngles = CameraGizmoTransform.eulerAngles + Vector3.right * -verticalLookRotation + Vector3.up * horizontalLookRotation;       //부모관점에서 회전함
            if (moveDirection != Vector3.zero)
            {
                transform.eulerAngles = new Vector3( transform.eulerAngles.x , transform.eulerAngles.y +CameraGizmoTransform.localEulerAngles.y, transform.eulerAngles.z);
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + CameraGizmoTransform.localEulerAngles.y, transform.eulerAngles.z);
            }
        }
        else
        {
            moveDirection = Vector3.forward * v * moveSpeed;
            transform.Rotate(Vector3.up, h * rotateSeSensitivity);
            if (moveDirection != Vector3.zero)
            {
                CameraGizmoTransform.localRotation = Quaternion.Lerp(CameraGizmoTransform.localRotation, Quaternion.identity, .05f);
            }
        }
        //       transform.LookAt(transform.position + moveDirection);
        //      print("Mouse X " + Input.GetAxis("Mouse X"));

        //CameraGizmoTransform.localEulerAngles = Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX;      //mouseSensitivityX 감도

        

        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
        }
       */
    }

    private void FixedUpdate()
    {
        //   Vector3 move = transform.TransformDirection(moveDirection) * Time.fixedDeltaTime;
        Vector3 move = transform.localRotation * moveDirection * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }
}
