using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("Hello Unity!");
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.W))
            transform.Rotate(-Vector3.left * 5f);
        if (Input.GetKey(KeyCode.S))
            transform.Rotate(Vector3.left * 5f);

    }
}
