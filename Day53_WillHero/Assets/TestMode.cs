using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMode : MonoBehaviour {

    [SerializeField]    //private 도 보이게
    bool isTestMode = false;

	// Use this for initialization
	void Start () {
        GameObject gameFlow = GameObject.Find("/GameFlow");
        isTestMode = gameFlow == null ? true : false ;

        if (isTestMode)
        {
            Physics.gravity *= 4f;
        }
        else
        {

        }
	}
	
}
