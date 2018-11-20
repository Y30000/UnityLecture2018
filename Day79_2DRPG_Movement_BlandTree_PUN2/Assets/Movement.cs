using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour {

    public float moveSpeed = 4f;

    Animator anim;
    float lastX, lastY;

    PhotonView photonView;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();

        if (!GetComponent<PhotonView>().IsMine)
            Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {


        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 heading = new Vector3(h, v, 0).normalized;
        Vector3 movement = heading * moveSpeed * Time.deltaTime;
        transform.position += movement;

        UpdateAnimation(heading);
    }

    private void UpdateAnimation(Vector3 heading)
    {
        if(heading.x == 0 && heading.y == 0)
        {
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetBool("Movement", false);
        }
        else
        {
            lastX = heading.x;
            lastY = heading.y;
            anim.SetBool("Movement", true);
        }
        anim.SetFloat("DirX", heading.x);
        anim.SetFloat("DirY", heading.y);
    }
}
