using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineStart : MonoBehaviour {
    IEnumerator Start()
    {
        print("Start()");
        yield return StartCoroutine(Todo());    //Coroutine 이 끝난뒤에 호출되라고 Start함수도 IEnumerator 로 선언
        print("C");
        yield return GetIdenticonFromURL();     //위의 StartCoroutine 과 같은 의미
    }

    IEnumerator Todo()
    {
        print("A");
        yield return new WaitForSeconds(1f);
        print("B");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator GetIdenticonFromURL()
    {
        string url = "http://www.gravatar.com/avatar/ctkim?s=256&d=retro&r=PG";

        using(WWW www = new WWW(url))
        {
            yield return www;
            var head = transform;
            head.GetComponent<Renderer>().material.mainTexture = www.texture;
        }
    }
}
