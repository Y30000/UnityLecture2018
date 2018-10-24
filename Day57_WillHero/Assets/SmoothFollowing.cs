using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowing : MonoBehaviour {
    public float smoothTime = 0.05f;
    public float smoothTimeY = 0.7f;
    public float thresholdY = 2f;
    public Vector3 localPosition = new Vector3(3, 0, -24);

    Transform target;
    Vector3 velocity = Vector3.zero;
    float velocityY = 0f;

    // Use this for initialization
    void Start () {
        SetTarget();
	}

    private void SetTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            target = player.transform;
    }

    // Update is called once per frame
    void Update () {
		if(target == null)
        {
            SetTarget();
            return;
        }

        Vector3 targetPosition = target.TransformPoint(localPosition);

        float y = Mathf.SmoothDamp(transform.position.y, targetPosition.y, ref velocityY, smoothTimeY);
        Vector3 p = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);   //lep
        float d = targetPosition.y - transform.position.y;

        if (Mathf.Abs(d) > thresholdY)
            p.y = y;
        else
            p.y = transform.position.y;

        transform.position = p;
	}
}