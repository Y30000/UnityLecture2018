using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullit : MonoBehaviour {
    public GameObject bullet;
    public GameObject tanPi;
    Transform bulletPos;
    Transform tanpiPos;
    public transform parent;
    GameObject g;

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            g = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            Destroy(g , 10f);
            g.GetComponentInChildren<Rigidbody>().AddForce(parent.localRotation * 1000);

            Destroy(Instantiate(tanPi, tanpiPos.position, tanpiPos.rotation), 10f);
        }
	}
}
