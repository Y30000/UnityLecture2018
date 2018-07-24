using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print(transform.position);
        print(transform.rotation);
        print(transform.localScale);

        print(transform.forward);   //현재 오브젝트의 방향   //단위벡터
        print(transform.right);     //회전할때마다 변함
        print(transform.up);        //

        print(transform.childCount == 3);
        print(transform.GetChild(0).name == "B");
        print(transform.GetChild(0).parent.name == "A");
        print(transform.Find("D").name == "D");
        print(transform.Find("D/E").name == "E");           //디랙토리 검색
        print(transform.Find("D/E").root == transform);
        print(transform.Find("D/E").root.name == transform.name);     // root == 최상위 , 현재 A가 최상위니까 this.transform

        print(transform.GetComponent<Transform>() == transform);      // transform 은 A의 컨포넌트 //GetComponent 는 게임 오브젝트가 가지고 있음 //게임 오브젝트의 트렌스폼은 트렌스폼
        GetComponent<Transform>();
        gameObject.GetComponent<Transform>();
        GetComponent<MeshRenderer>().material.color = Color.cyan;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
