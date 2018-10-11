using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : Singleton<GameFlow> {

    protected GameFlow() { }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        Physics.gravity *= 4f;
	}
	
}
