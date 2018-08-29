using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour {


	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
            StartCoroutine(FadeIn());
	}

    IEnumerator FadeIn()
    {
        for (float f = 1.0f; f >= 0; f -= 0.01f)
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return null;
        }
    }
}
