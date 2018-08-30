using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {
    public float angle = 90f;
    public float duration = 1f;
    bool isRotating = false;
    
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            isRotating = true;
            StartCoroutine(RotateStage(angle, duration));
        }
	}

    IEnumerator RotateStage(float angle, float duration)
    {
        float remainingAngle = angle;
        float remainingDuration = duration;
        while (remainingAngle > 0)
        {
            float anglePerFrame = remainingAngle / remainingDuration * Time.deltaTime;
            /*
            if(remainingAngle < anglePerFrame)
            {
                anglePerFrame = remainingAngle;
                isRotating = false;
            }
            */
            if (remainingAngle > anglePerFrame)
            {
                transform.Rotate(Vector3.up * anglePerFrame);
                remainingAngle -= anglePerFrame;
                remainingDuration -= Time.deltaTime;
            }
            else
            {
                transform.Rotate(Vector3.up * remainingAngle);
                remainingAngle = 0;
            }
            yield return null;
        }
        isRotating = false;
    }
}
