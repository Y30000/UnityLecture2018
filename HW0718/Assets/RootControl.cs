using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * 1f);
        if(Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * 1f);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(-Vector3.up * 1f);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up * 1f);
        if (Input.GetKey(KeyCode.U))
            transform.Rotate(-Vector3.right * 1f);
        if (Input.GetKey(KeyCode.J))
            transform.Rotate(Vector3.right * 1f);
        if (Input.GetKey(KeyCode.H))
            transform.Rotate(Vector3.forward * 1f);
        if (Input.GetKey(KeyCode.K))
            transform.Rotate(-Vector3.forward * 1f);
    }
}
