using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class HWPoolObject : MonoBehaviour {

    public EZObjectPool objectPool;
    public float firePerSecond = 10;

    GameObject outObject;
    float time = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && time < Time.time)
        {
            time = Time.time + 1 / firePerSecond;
            if (objectPool.TryGetNextObject(transform.position, transform.rotation, out outObject))
            {
                Rigidbody rb = outObject.GetComponent<Rigidbody>();
                rb.AddForce(transform.right * 100f);
                StartCoroutine(returnStatus(outObject));
            }
        }
    }

    IEnumerator returnStatus(GameObject obj)
    {
        yield return new WaitForSeconds(10f);
        obj.SetActive(false);
    }
}
