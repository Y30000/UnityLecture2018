using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SmoothTransform : NetworkBehaviour     //call by reference;
{
    struct TransformData    //call by value;
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    [SyncVar]
    TransformData syncTransform; //서버가 알아서 다 뿌려줌
    public float lerpRate = 15f;

    private void FixedUpdate()
    {
        if(isLocalPlayer)
            SendTransform();
        else
            LerpTransform();
    }

    void SendTransform()
    {
        TransformData tm = new TransformData()
        {
            position = transform.position,
            rotation = transform.rotation
        };
        CmdSendTransformToSever(tm);
    }

    [Command]   //Local을 서버에게
    void CmdSendTransformToSever(TransformData tm)  //서버에서 동작함
    {
        syncTransform = tm;
    }

    [ClientCallback]    //클라이언트에서만 호출가능
    void LerpTransform()
    {
        transform.position = Vector3.Lerp(transform.position, syncTransform.position, Time.fixedDeltaTime * lerpRate);
        transform.rotation = Quaternion.Lerp(transform.rotation, syncTransform.rotation, Time.fixedDeltaTime * lerpRate);
    }

}
