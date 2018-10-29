using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTWEEN : MonoBehaviour {

    public AnimationCurve ac;

    [Range(0f,1f)]
    public float t;

    bool flag = true;

	// Update is called once per frame
	void Start () {
        //transform.DOScale(Vector3.one * .5f , 1).SetLoops(-1,LoopType.Yoyo);
    }

    private void Update()
    {
        if (flag && t < 1)
        {
            t += Time.deltaTime;
        }else if(t > 0)
        {
            flag = false;
            t -= Time.deltaTime;
        }
        else
        {
            flag = true;
        }

        transform.localScale = Vector3.one * ac.Evaluate(t);
    }
}
