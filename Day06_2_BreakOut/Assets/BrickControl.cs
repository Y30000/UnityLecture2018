using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickControl : MonoBehaviour {

    public ParticleSystem effect;
    public float x = 0;
 //   public GameObject effect;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        var e = Instantiate(effect, transform.position,new Quaternion(x,0,0,1));
        Destroy(e.gameObject, 5f);
    }
}
