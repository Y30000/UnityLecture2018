using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Vector3 dir = other.gameObject.GetComponents<Transform>()[0].position - transform.position;
        other.gameObject.GetComponents<Rigidbody>()[0].velocity = dir.normalized * other.gameObject.GetComponents<Rigidbody>()[0].velocity.magnitude;
        Destroy(this.gameObject);
    }
}
