using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    float tempTime = 0;

    private void Start()
    {
        if (isLocalPlayer)
            return;

        Destroy(GetComponentInChildren<Camera>());
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isLocalPlayer)
            return;

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKey(KeyCode.Space) && tempTime < Time.time)
        {
            tempTime = Time.time + .1f;
            CmdFire();  //클라이언트 영역
        }
    }

    [Command]   //Remote Procedure Call  원격 함수 호출   //Cmd 붙어야 사용가능 // 서버에서만 실행하는 코드 //클리언트에서 동작(호출)하는데;;
    private void CmdFire()
    {
        var bullet = Instantiate(bulletPrefab,
                                 bulletSpawn.position,
                                 bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6f;
        NetworkServer.Spawn(bullet);    //여기서 생성됨
        Destroy(bullet, 2f);            //서버상에서 제거함 //서버에서 제거되면 클라이언트에서도 제거됨
    }

    public override void OnStartLocalPlayer()   //자신이 로컬일때만 출됨
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

}
