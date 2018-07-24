using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public float speed = 20;
    public float rotateSpeed = 90;

	// Update is called once per frame
	void Update () {
        float v = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += transform.forward * v;

    }
}
