using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject shellPrefab;
    public Transform shellEjection;
    public float fireRate = 10;
    public Light muzzleFlash;
    public GameObject impactFX;
    public GameObject bulletHolePrefab;

    Camera fpsCamera;
    float nextTimeToFire = 0f;
    Vector3 originPos;
    Vector3 smoothVel;

    private void Start()
    {
        fpsCamera = GetComponentInParent<Camera>(); //main camera가 불려옴 부모의 Component
        originPos = transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Shoot();
        }

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, originPos, ref smoothVel, 0.1f);      //매 10%씩 ruf의 마지막 파라
    }

    private void Shoot()
    {
        muzzleFlash.enabled = true;
        Invoke("OffMuzzleFlash", 0.05f);

        MakeShell();

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 200f))
        {
            //print(hit.transform.name);
            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(fpsCamera.transform.forward * 500f);

            //      BulletSound bs = hit.transform.GetComponent<AudioSource>();
            //      if(bs != null)
            //        bs.Play();
        }

        Destroy(Instantiate(impactFX, hit.point, Quaternion.identity), 0.3f);

        MakeBulletHole(hit.point, hit.normal, hit.transform);

        transform.localPosition -= Vector3.forward * UnityEngine.Random.Range(0.07f, 0.3f);

        //play audioSo
        GetComponent<AudioSource>().Play();
        /*
        try
        {
            hit.transform.GetComponent<AudioSource>().Play();
        }
        finally
        {
            Destroy(Instantiate(impactFX, hit.point, Quaternion.identity), 0.3f);

            MakeBulletHole(hit.point, hit.normal, hit.transform);

            transform.localPosition -= Vector3.forward * UnityEngine.Random.Range(0.07f, 0.3f);

            //play audioSo
            GetComponent<AudioSource>().Play();
        }*/
    }




    private void MakeBulletHole(Vector3 point, Vector3 normal, Transform parent)
    {
        GameObject clone = Instantiate(bulletHolePrefab, point + normal * 0.02f, Quaternion.identity);

        clone.transform.SetParent(parent, true);

        clone.transform.localRotation = Quaternion.identity;
        clone.transform.localScale = DivVector3(Vector3.one, parent.lossyScale);

        Transform child = clone.transform.GetChild(0);
        child.rotation = Quaternion.LookRotation(-normal, child.transform.up);
       
        clone.layer = 0;
    }

    void OffMuzzleFlash()
    {
        muzzleFlash.enabled = false;
    }

    private void MakeShell()
    {
        GameObject clone = Instantiate(shellPrefab, shellEjection);

        //clone.transform.parent = null;
        clone.transform.SetParent(null);
    }

    public static Vector3 GetWorldScale(Transform transform)
    {
        Vector3 worldScale = transform.localScale;
        Transform parent = transform.parent;

        while (parent != null)
        {
            worldScale = Vector3.Scale(worldScale, parent.localScale);
            parent = parent.parent;
        }

        return worldScale;

    }

    Vector3 DivVector3(Vector3 vec1,Vector3 vec2)
    {
        Vector3 ans = new Vector3(vec1.x / vec2.x, vec1.y / vec2.y, vec1.z / vec2.z);
        return ans;
    }
}
