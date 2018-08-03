using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {
    Camera fpsCamera;
	// Use this for initialization
	void Start () {
        fpsCamera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
        RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 4f))
            {
                print(hit.transform.name);
                Openable door = hit.transform.GetComponent<Openable>();
                if (door != null) { }
                    door.ToggleDoor();
            }
        }
    }
}
