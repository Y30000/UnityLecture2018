using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    public float forceMin;
    public float forceMax;

    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
        float force = Random.Range(forceMin, forceMax);
        rb.AddForce(transform.right * force);
        rb.AddTorque(Random.insideUnitSphere * force);
        Destroy(gameObject, 25f);
	}
}
