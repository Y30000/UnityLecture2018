using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public float force = 1000;
    bool isFire = false;
	// Use this for initialization
	void Start () {
		
	}
	
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFire)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * force);
            isFire = true;
            transform.parent = null;
        }
    }

}
