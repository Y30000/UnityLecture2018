using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullit : MonoBehaviour {
    public GameObject bullet;
    public GameObject tanPi;
    public Transform bulletPos;
    public Transform tanpiPos;
    GameObject p;

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject g;
            g = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            g.GetComponentInChildren<Rigidbody>().AddForce(bulletPos.up * 1000);
            Destroy(g, 10f);

            g = Instantiate(tanPi, tanpiPos.position, tanpiPos.rotation);
            Destroy(g, 10f);
        }
	}
}
