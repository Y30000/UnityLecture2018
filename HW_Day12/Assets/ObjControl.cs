﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * 0.1f);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(-Vector3.forward * 0.1f);

    }
}
