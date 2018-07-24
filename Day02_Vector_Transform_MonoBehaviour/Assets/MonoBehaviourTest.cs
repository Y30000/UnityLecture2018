using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourTest : MonoBehaviour {

    private void Awake()
    {
        print("Awake()");
    }

    void Start () {
        print("Start()");

        print(name == "Cube");
        print(gameObject.name == name);                     //현재 이 클래스를 포함하는 게임 오브젝트 이 클레스는 게임 오브젝트에 속한 Component 그러니까 게임 오브젝트는 아님
        print(transform == GetComponent<Transform>());      //transform == shortcut 바로가기 같은거
        print(GetComponent<MonoBehaviourTest>() == this);   //this = 현재 클레스 = Component중 하나 (Script)
                                                            //지울때 Distory GameObject *중요*
    }
	
	// Update is called once per frame
	void Update () {
        print("Update()");
	}

    private void FixedUpdate()      //1/50 초마다 호출된다. 물리 시뮬같은거 할때
    {
        print("FixedUpdate()");
    }

    private void LateUpdate()
    {
        print("LateUpdate()");
    }
}
