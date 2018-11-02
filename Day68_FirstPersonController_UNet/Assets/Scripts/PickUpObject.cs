using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour {
    public Transform holder;

    Camera fpsCamera;

    private void Start()
    {
        fpsCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if(Physics.Raycast(fpsCamera.transform.position,fpsCamera.transform.forward,out hit, 4f))
            {
                print(hit.transform.name);
                Pickable item = hit.transform.gameObject.GetComponent<Pickable>();
                if (item != null)
                    Pickup(hit.transform);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            ThrowItem();
        }
	}

    private void ThrowItem()
    {
        if(holder.childCount > 0)
        {
            Transform item = holder.GetChild(0);
            item.SetParent(null);
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.forward * 1000f);
        }
    }

    private void Pickup(Transform item)
    {
        if(holder.childCount < 1)
        {
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.SetParent(holder);
            //item.transform.position = holder.transform.position;
            item.transform.localPosition = Vector3.zero;
        }
    }
}
