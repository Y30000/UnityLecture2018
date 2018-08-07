using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour {

    public GameObject blockPrefab;

    Camera fpsCamera;
    // Use this for initialization
    void Start()
    {
        fpsCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position,
                                fpsCamera.transform.forward,
                                out hit,
                                10f))
            {
                print(hit.transform.name);
                if (hit.transform.CompareTag("Block"))
                {
                    hit.transform.GetComponent<BreakUp>().Hit();
                }
            }
        }
    }
}
