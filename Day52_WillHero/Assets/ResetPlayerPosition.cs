using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.CompareTag("Player"))
            other.transform.root.position = new Vector3(-3, 10, 0);
    }
}
