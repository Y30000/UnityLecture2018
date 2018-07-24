using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pingpong : MonoBehaviour {
    public float speed = 5.0f;
    public float dir = 1f;

	//// Use this for initialization
	//void Start () {
 //       speed = 100;
	//}
	
	// Update is called once per frame
	void Update ()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (x > 5f)
            dir = -1;
        if (x < 0f)
            dir = 1;

        transform.Translate(dir * speed * Time.deltaTime, 0f, 0f);
    }
}
