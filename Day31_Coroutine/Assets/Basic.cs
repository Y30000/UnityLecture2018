using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("Start()");
        StartCoroutine(Todo());     // or StartCoroutine("Todo");
        print("B");
    }
	IEnumerator Todo()
    {
        print("A");
        yield return null;
        print("C");
        yield return new WaitForSeconds(0.1f);
        print("D");
        yield return StartCoroutine(NewTodo()); //StartCoroutine(NewTodo());이 종료될때 까지 
        print("G");
    }

    IEnumerator NewTodo()
    {
        print("E");
        yield return new WaitForSeconds(1f);
        print("F");
    }
    int count = 1;
    private void Update()
    {
        print("Update " + count++);
    }
}
