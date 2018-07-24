using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBar : MonoBehaviour {
    public float speed = 20;
    public float limite = 17.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxisRaw("Horizontal");

        h *= speed * Time.deltaTime;
        
        if(transform.position.x + h > limite)
            transform.position = Vector3.right * limite;
        else if(transform.position.x + h < -limite)
            transform.position = Vector3.left * limite;
        else
            transform.Translate(Vector3.right * h);

    }
}
