using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour {

    Animator anim;
    bool isOpened;

	// Use this for initialization
	void Start () {
        anim = GetComponentInParent<Animator>();

	}
	
    public void ToggleDoor()
    {
        isOpened = !isOpened;
        anim.SetBool("isOpened", isOpened);
    }
}
