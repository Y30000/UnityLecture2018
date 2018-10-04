using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float dashDistance = 3f;
    public bool dashOn = true;
    public Transform weaponHolder;
    public GameObject javelinPrefab;

    Rigidbody rb;
    Coroutine coDash;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (coDash != null)
                StopCoroutine(coDash);
            if (dashOn)
                coDash = StartCoroutine(Dash(dashDistance,0.2f));
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        GameObject weaponBullet = Instantiate(javelinPrefab,
                                              weaponHolder.transform.position,
                                              Quaternion.AngleAxis(30,Vector3.forward));
        weaponBullet.GetComponent<JavelinController>().Trow();
        Destroy(weaponBullet, 10f);
    }

    IEnumerator Dash(float distance, float duration)
    {
        float d = 0;
        while (d < distance)
        {
            rb.velocity = Vector3.zero;
            float tempDistance = distance / duration * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + Vector3.right * tempDistance);
            d += tempDistance;
            yield return new WaitForFixedUpdate();
        }
    }
}
