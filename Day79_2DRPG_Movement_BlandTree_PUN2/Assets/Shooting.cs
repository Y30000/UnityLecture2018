using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour {


    // Update is called once per frame
    void Update () {
        PhotonView pv = PhotonView.Get(this);
        if (!pv.IsMine)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pv.RPC("Shoot", RpcTarget.AllViaServer, "Parameter");
        }
	}

    [PunRPC]
    void Shoot(string param, PhotonMessageInfo info)
    {
        print("shoot: " + info.Sender);
        print("shoot: " + param);

        GetComponent<Health>().TakeDamage(5);
    }
}
