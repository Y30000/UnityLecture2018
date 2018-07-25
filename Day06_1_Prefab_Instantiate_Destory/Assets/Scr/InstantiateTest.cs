using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTest : MonoBehaviour {

    public GameObject target;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject g;
            g = Instantiate(target);
            g.name = "Instantiate(target)";

            g = Instantiate(target, transform);
            g.name = "Instantiate(target, transform)";

            g = Instantiate(target, transform.position, Quaternion.identity);
            g.name = "Instantiate(target, transform.position, Quaternion.identity)";

            Destroy(g, 10f);
        }

        
	}
}
