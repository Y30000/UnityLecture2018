using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 3f;
    public float rotationSpeed = 150f; 

    CharacterController con;
    Vector3 moveDirection = Vector3.zero;
    Animator anim;

    float jumpSpeed = 15f;
    float gravity = 23f;
    float currentDownSpeed;

    MouseLook mouseLook;
    public Transform weaponHolder;
    public Transform weaponDisarmHolder;

    public bool isEquipped {
        get
        {
            return weaponHolder != null && weaponHolder.childCount > 0;
        }
    }

    public bool isDisarmed {
        get
        {
            return weaponDisarmHolder != null && weaponDisarmHolder.childCount > 0;
        }

    }

    // Use this for initialization
    void Start () {
        con = GetComponent<CharacterController>();
        mouseLook = GetComponentInChildren<MouseLook>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	public void FrameMove () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouseMoveX = Input.GetAxis("Mouse X");
        //float mouseMoveY = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(1))
        {
            moveDirection = new Vector3(h, 0, v).normalized;
            float rotationY = mouseMoveX * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * rotationY);
            mouseLook.ResetCamera();

            //if(h > 0)
            //{
            //    anim.SetBool("isRight", true);
            //    anim.SetBool("isLeft", false);
            //}
            //else if(h < 0)
            //{
            //    anim.SetBool("isRight", false);
            //    anim.SetBool("isLeft", true);
            //}
            //else
            //{
            //    anim.SetBool("isRight", false);
            //    anim.SetBool("isLeft", false);
            //}
        }
        else
        {
            moveDirection = new Vector3(0, 0, v).normalized;
            float rotationY = h * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * rotationY);
            //anim.SetBool("isRight", false);
            //anim.SetBool("isLeft", false);
        }

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed * 1.5f;

        //if (Input.GetButtonDown("Jump"))
        //    anim.SetBool("isJump", true);

        if (Input.GetButtonUp("Jump"))
            Jump();

        currentDownSpeed -= gravity * Time.deltaTime;
        moveDirection.y = currentDownSpeed;

        con.Move(moveDirection * Time.deltaTime);
        //anim.SetFloat("moveSpeed", con.velocity.magnitude);
        //anim.SetBool("isGround", con.isGrounded);
        anim.SetFloat("h", h);
        anim.SetFloat("v", v);

    }

    internal GameObject GetNearstWeaponIn(float radius, float angle, string weaponTag)
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag(weaponTag);
        var list = new List<GameObject>(weapons);
        int index = list.FindIndex(o =>
        {
            Vector3 dir = o.transform.position - transform.position;
            return dir.magnitude < radius && Vector3.Angle(dir, transform.forward) < angle;
        });
        if (index == -1)
            return null;
        list[index].GetComponent<Rigidbody>().isKinematic = true;
        return list[index];
    }

    void Jump()
    {
        if (!con.isGrounded)
            return;
        currentDownSpeed = jumpSpeed; 
        anim.SetBool("isJump",false);
    }

    void Disarm()
    {
        print("disarm");
        if (isEquipped)
        {
            Transform weapon = weaponHolder.GetChild(0);
            weapon.SetParent(weaponDisarmHolder);
        }
    }

    void Equip()
    {
        print("equip");
        if (isDisarmed)
        {
            Transform weapon = weaponDisarmHolder.GetChild(0);
            weapon.SetParent(weaponHolder);
        }
    }
}