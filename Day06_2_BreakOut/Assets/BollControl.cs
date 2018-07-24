using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollControl : MonoBehaviour {
    public float power = 400;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.parent = null;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(0f, power, 0f));
            enabled = false;
        }
	}
}
