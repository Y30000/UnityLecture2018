using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3 v = new Vector3(1f, 1f, 1f);
        Vector3 u = new Vector3(1f, 1f, 1f);
        print(v == u);
        print(2*v == new Vector3(2f,2f,2f));
        v = new Vector3(1f, 0f, 0f);
        print(v == Vector3.right);
        print(v.magnitude == 1);
        print(Vector3.one.magnitude);
        print(v.ToString() == "(1.0, 0.0, 0.0)");
        print(v);
        print(Vector3.Distance(Vector3.zero, u) == u.magnitude);
        print(Vector3.Angle(Vector3.right, Vector3.up) == 90f);  //예각
        print(Vector3.Angle(Vector3.right, Vector3.forward) == 90f);  //예각
        print((2 * Vector3.up).normalized == Vector3.up);
        print((2 * Vector3.up) / (2 * Vector3.up).magnitude == Vector3.up);
        print(Vector3.Cross(Vector3.right, Vector3.up) == Vector3.forward);
        print(Vector3.Cross(Vector3.up, Vector3.right) == Vector3.back);    //왼손 좌표

        transform.position = new Vector3(5f, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
