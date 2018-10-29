using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        var health = collision.gameObject.GetComponent<Health>();
        if(null != health)
        {
            health.DecreaseHP(10);
        }

        Destroy(gameObject);
    }
}