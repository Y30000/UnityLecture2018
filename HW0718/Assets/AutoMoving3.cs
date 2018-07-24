using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class AutoMoving3 : MonoBehaviour
{

    static float x, z, y, xx, zz;
    static int Dir, counter;

    // Use this for initialization
    void Start()
    {
        Dir = counter = 0;
        x = transform.position.x;
        z = transform.position.z;
        y = transform.position.y;

    }
//    Math.Cos()
    // Update is called once per frame
    void Update()
    {
        if (++counter >= 60)
        {
            counter -= 60;
            Dir = (Dir + 1) % 4;
        }
        
        switch (Dir)
        {
            case 0:
                z += 0.1f;
                break;
            case 1:
                x += 0.1f;
                break;
            case 2:
                z -= 0.1f;
                break;
            case 3:
                x -= 0.1f;
                break;
        }

        transform.position = new Vector3(x, y, z);
    }
}
