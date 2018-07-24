using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour {

    public Transform dest;
    public GameObject destObject;

    public List<Transform> waypoints;

	// Update is called once per frame
	void Update () {

        transform.position = Vector3.MoveTowards(transform.position/*현재 좌표*/, dest.position/*목표 좌표*/, 0.1f/*1프레임당 속도*/);
        //      transform.position = Vector3.MoveTowards(transform.position, destObject.transform.position, 0.1f);
 //       transform.position = Vector3.MoveTowards(transform.position, destObject.GetComponent<Transform>().position, 0.1f);

    }
}
