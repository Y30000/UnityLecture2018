using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class PoolObject : MonoBehaviour {

    public EZObjectPool objectPool;

    GameObject outObject;

	// Use this for initialization
	void Start () {

        if (objectPool.TryGetNextObject(Vector3.zero,Quaternion.identity,out outObject))
        {
            
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (objectPool.TryGetNextObject(Vector3.zero, Quaternion.identity, out outObject))
                StartCoroutine(returnStat(outObject));
        }
	}

    IEnumerator returnStat(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        obj.SetActive(false);
    }
}
