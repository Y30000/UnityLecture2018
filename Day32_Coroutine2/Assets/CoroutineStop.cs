using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineStop : MonoBehaviour {

    Coroutine co;

    IEnumerator Start()
    {
        co = StartCoroutine(Todo(2.0f));
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Todo(float value)
    {
        while (true)
        {
            print("Todo with " + value);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            //StopCoroutine(co);
            StopAllCoroutines();    //이 MonoBehaviour 에 존재하는 모든 Coroutine 종료
    }

    /*
	// Use this for initialization
	IEnumerator Start () {
        StartCoroutine("Todo", 2.0f);
        yield return new WaitForSeconds(1f);
        StopCoroutine("Todo");
        //StartCoroutine(Todo(3.0f));           //종료 하려면 Coroutine 변수로 값 가지고 있어야함
        //yield return new WaitForSeconds(1f);
        //StopCoroutine(Todo(3.0f));

    }

    IEnumerator Todo(float value)
    {
        while (true)
        {
            print("Todo with " + value);
            yield return new WaitForSeconds(0.2f);
        }
    }
    */
}
