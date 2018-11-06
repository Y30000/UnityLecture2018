using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemInteraction : NetworkBehaviour
{
    public Transform itemHolder;

    Camera fpsCamera;

	// Use this for initialization
	void Start () {
        fpsCamera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            CmdCheckInterationItem();
        }
	}

    [Command]
    private void CmdCheckInterationItem()
    {
        if(itemHolder.childCount > 0)
        {
            RpcTrowItem();
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position,
                            fpsCamera.transform.forward,
                            out hit,
                            4f,
                            1 << LayerMask.NameToLayer("Item")))
        {
            Item item = hit.transform.gameObject.GetComponent<Item>();
            if (item != null)
                RpcPickup(hit.transform.GetComponentInParent<NetworkIdentity>().netId);

            DoorButton door = hit.transform.gameObject.GetComponent<DoorButton>();
            if (door != null)
                door.ToggleDoor();
        }
    }

    private void RpcTrowItem()
    {
        if (itemHolder.childCount == 0)
        {
            Transform item = itemHolder.GetChild(0);
            item.SetParent(null);
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.GetComponent<NetworkTransform>().enabled = true;
            item.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.forward * 700f);
        }
    }

    [ClientRpc]
    private void RpcPickup(NetworkInstanceId netId)
    {
        if(itemHolder.childCount == 0)
        {
            Transform item = ClientScene.FindLocalObject(netId).transform;
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.GetComponent<NetworkTransform>().enabled = false;
            item.SetParent(itemHolder);
            item.transform.position = itemHolder.transform.position;
        }
    }
}
