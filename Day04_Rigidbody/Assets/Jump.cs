using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
  //          GetComponent<Rigidbody>().AddForce(new Vector3(0f, 400f, 0f));
            GetComponent<Rigidbody>().AddForce(Vector3.up * 400f);
        }
    }
}
