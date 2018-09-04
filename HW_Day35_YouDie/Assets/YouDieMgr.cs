using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouDieMgr : MonoBehaviour {

    Image img;
    Text tex;

    private void Start()
    {
        img = GetComponent<Image>();
        tex = GetComponentInChildren<Text>();
    }

    public void YouDie()
    {
        StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        Color imgColor;
        Color texColor;
        imgColor = img.color;
        texColor = tex.color;
        for (float alpha = 0; alpha < 1; alpha += Time.deltaTime / 2)
        {
            if (alpha > 1)
                alpha = 1;
            imgColor.a = texColor.a = alpha;
            img.color = imgColor;
            tex.color = texColor;
            yield return null;
        }
    }
}
