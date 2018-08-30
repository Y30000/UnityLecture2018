using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut0 : MonoBehaviour {
    float alpha = 1.0f;
    bool fadeIn = false;
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I) && !fadeIn)
        {
            alpha = 1.0f;
            fadeIn = true;
        }

        if(fadeIn && alpha > 0)
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            Color c = renderer.material.color;
            alpha -= Time.deltaTime;
            if(alpha < 0f)
            {
                alpha = 0f;
                fadeIn = false;
            }
            c.a = alpha;
            renderer.material.color = c;
        }
	}
}
