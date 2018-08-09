using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour {

    public Transform player;
    public float distance;

    Vector3 dir;

	// Use this for initialization
	void Start () {
        dir = player.transform.position - transform.position;
        dir = dir.normalized;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position - distance * dir;
	}
}
