using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingoutZone : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.CompareTag("Player"))
        {
            PlayerController pc = other.transform.root.GetComponent<PlayerController>();
            if(null != pc)
            {
                pc.RingOut();
            }
        }
    }
}
