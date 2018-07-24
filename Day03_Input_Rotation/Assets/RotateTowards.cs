using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour {

    public Transform cub;
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, cub.position - transform.position,1,0f));
	}
}
