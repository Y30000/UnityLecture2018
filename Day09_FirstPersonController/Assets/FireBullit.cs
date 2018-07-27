using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullit : MonoBehaviour {
    public GameObject bullet;
    public GameObject tanPi;
    public GameObject gun;
    public Transform bulletPos;
    public Transform tanpiPos;
    GameObject g;

    bool isFired = false;
    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(0) && !isFired)
        {
            g = Instantiate(bullet, gun.transform.forward + gun.transform.position, gun.transform.rotation);
            g.GetComponent<Rigidbody>().AddForce(gun.transform.forward * 1500);
            Destroy(g, 10f);

            g = Instantiate(tanPi, gun.transform.right * 0.2f + gun.transform.position, gun.transform.rotation);
            g.GetComponent<Rigidbody>().AddForce(gun.transform.right * 10);
            Destroy(g, 10f);
            isFired = true;
            Invoke("FireStatus", Time.deltaTime * 3);
        }
	}

    void FireStatus()
    {
        isFired = false;
    }
}
