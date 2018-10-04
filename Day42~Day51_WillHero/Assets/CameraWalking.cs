using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraWalking : MonoBehaviour {

    public Transform playerPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.DOMoveX(playerPosition.position.x, .5f);
	}
}