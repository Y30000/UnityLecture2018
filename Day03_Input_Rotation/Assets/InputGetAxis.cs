using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGetAxis : MonoBehaviour {
    public float speed = 10f;
    public float rotationSpeed = 120f;

	// Update is called once per frame
	void Update () {
        float v = Input.GetAxis("Vertical");                 //누르고 있으면 1 또는 -1까지 손때면 0으로 점점 줄어듬
 //       float v = Input.GetAxisRaw("Vertical");                //-1,0,1
        float h = Input.GetAxis("Horizontal");
        //       print("translation : " + Input.GetAxisRaw("Vertical"));   

//        transform.Translate(0, 0, v * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * v * speed * Time.deltaTime);
 //       transform.Rotate(0, h * rotationSpeed * Time.deltaTime, 0);
 //       transform.Rotate(Vector3.up * h * rotationSpeed * Time.deltaTime);
        transform.rotation *= Quaternion.AngleAxis(h * rotationSpeed * Time.deltaTime,Vector3.up);      // AngleAxis((1),(2))  (2) 방향으로 (1)만큼 회전        //rotation 현재 회전값   (*= 누적)
    }
}
