using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlock : MonoBehaviour {

    public GameObject blockPrefab;

    Camera fpsCamera;
	// Use this for initialization
	void Start () {
        fpsCamera = GetComponentInChildren<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(fpsCamera.transform.position,
                                fpsCamera.transform.forward,
                                out hit,
                                10f))
            {
                print(hit.transform.name);
                Instantiate(blockPrefab, hit.transform.position + hit.normal, Quaternion.identity);
            }
        }
	}
}
