using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakUp : MonoBehaviour {
    public Texture[] Cracks;
    public ParticleSystem fx;

    int numHits = 0;
    float lastHitTime;
    float hitTimeThreadhold = 0.05f;

    public void Hit()
    {
        CancelInvoke();
        if (lastHitTime + hitTimeThreadhold < Time.time)
        {
            numHits++;
            if (numHits < Cracks.Length)
                GetComponent<Renderer>().material.SetTexture("_DetailMask", Cracks[numHits]);
            else
            {
                var clone = Instantiate(fx,transform.position,Camera.main.transform.rotation);
                Destroy(clone.gameObject,2f);
                Destroy(gameObject);
            }
            lastHitTime = Time.time;
        }
        Invoke("Reset", hitTimeThreadhold + 0.1f);
        
    }

    private void Reset()
    {
        numHits = 0;
        GetComponent<Renderer>().material.SetTexture("_DetailMask", Cracks[0]);
    }
}
