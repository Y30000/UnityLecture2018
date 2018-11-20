using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviour , IPunObservable{
    //IPunObservable 있어야 Observe 에 등록가능
    public int health;
    public int maxHealth;

    private void Start()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable()
        {
            {"maxHealth", maxHealth }   //헤쉬 테이블 등록하면 자동으로
        });
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
        }
    }

    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            Hashtable ht = PhotonNetwork.LocalPlayer.CustomProperties;
            maxHealth = (int)ht["maxHealth"];
        }
    }
}
