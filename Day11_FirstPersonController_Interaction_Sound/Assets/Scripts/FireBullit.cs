using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullit : MonoBehaviour {
    public GameObject bullet;
    public GameObject tanPi;
    public GameObject gun;
    public Transform bulletPos;
    public Transform tanpiPos;
    public GameObject bulletMark;

    GameObject g;

    bool isFired = false;

    // Update is called once per frame
    void Update () {

        Vector3 fwd = gun.transform.TransformDirection(Vector3.forward);
        Vector3 originPos = gun.transform.position;
        Debug.DrawRay(originPos, fwd, Color.magenta);

        if (Input.GetButton("Fire1") && !isFired)
        {
            
            RaycastHit hit;
            
            if (Physics.Raycast(originPos, fwd,out hit, 100f))
                {
                g = Instantiate(bulletMark, hit.point + hit.normal * .001f, Quaternion.FromToRotation(Vector3.up, hit.normal));
                g.transform.Rotate(Vector3.up * Random.Range(0f, 360.0f), Space.Self);
                Destroy(g,10f);
                }
            /*
            g = Instantiate(bullet, gun.transform.forward + gun.transform.position, gun.transform.rotation);
            g.GetComponent<Rigidbody>().AddForce(gun.transform.forward * 1500);
            Destroy(g, 10f);
            */
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
