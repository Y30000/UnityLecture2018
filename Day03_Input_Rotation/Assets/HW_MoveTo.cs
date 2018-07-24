using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_MoveTo : MonoBehaviour
{
    static int to = 0;
    public List<GameObject> waypoints;

    void Start()
    {
        waypoints[to].GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, waypoints[to].GetComponent<Transform>().position - transform.position, 1, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != waypoints[to].GetComponent<Transform>().position)
            transform.position = Vector3.MoveTowards(transform.position, waypoints[to].GetComponent<Transform>().position, 0.1f);
        else {
            waypoints[to].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            to = (to + 1) % waypoints.Count;
            waypoints[to].GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, waypoints[to].GetComponent<Transform>().position - transform.position, 1, 0f));
        }
    }
}