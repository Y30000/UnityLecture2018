using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour {

    public GameObject hitFXPrefab;

    public void TakeDamage(int amount , Vector3 knuckBackDir)
    {
        GetComponent<Health>().DecreaseHP(amount);
        GetComponent<Rigidbody>().velocity = knuckBackDir * 2;
        GameObject clone = Instantiate(hitFXPrefab,
                                       transform.position,
                                       Quaternion.LookRotation(-knuckBackDir));

        Destroy(clone, 1.5f);
    }
}
