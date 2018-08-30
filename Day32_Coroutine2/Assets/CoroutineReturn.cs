using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutineReturn : MonoBehaviour {
    

	// Use this for initialization
	IEnumerator Start () {
        int ret = 10;
        yield return Todo("string", x => ret = x);
        print(ret);
	}

    IEnumerator Todo(string str, Action<int> result)
    {
        result(100);    //전역일때 ret = 100;
        yield return null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
