using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoving : MonoBehaviour {

    static int Counter;

	// Use this for initialization
	void Start () {
        Counter = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(++Counter >= 60)
        {
            Counter -= 60;
            transform.Rotate(Vector3.up * 90);
        }
        transform.Translate(Vector3.forward * 0.1f);

    }
}
