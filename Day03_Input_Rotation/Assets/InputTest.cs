using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        //Keyboard
        if (Input.GetKey(KeyCode.Space))
        {
            print("Space");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Key Down: Space");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            print("Key Up: Space");
        }

        //Mouse
        if (Input.GetMouseButton(0))        // 0 좌 1 우 2 휠클릭
        {
            print("Left click");
            print("mouse position: " + Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(0))
        {
            print("Left click down");
        }
        if (Input.GetMouseButtonUp(0))
        {
            print("Left click up");
        }
    }
}
