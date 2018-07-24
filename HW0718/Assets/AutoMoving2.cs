using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoving2 : MonoBehaviour {

    static int Counter, Dir;

    // Use this for initialization
    void Start()
    {
        Dir = Counter = 0;
    }

    // Update is called once per frame
    void Update () {
        if (++Counter >= 60)
        {
            Counter -= 60;
            Dir = (Dir + 1) % 4;
        }
        switch (Dir)
        {
            case 0:
                transform.Translate(Vector3.forward * 0.1f);
                break;
            case 1:
                transform.Translate(Vector3.right * 0.1f);
                break;
            case 2:
                transform.Translate(Vector3.back * 0.1f);
                break;
             case 3:
                transform.Translate(Vector3.left * 0.1f);
                break;
        }
        

    }
}

