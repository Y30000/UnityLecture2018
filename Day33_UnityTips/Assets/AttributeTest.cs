using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeTest : MonoBehaviour {
    [Range(0, 10)]  //대괄호로 시작하는건 Attribute 
    public int DamageRange = 10;

    [Header("Health Settings")]
    public int health;
    public int maxHealth;

    [Space(50)] //줄 띄우기
    [Header("Danage Settings")]
    public int damage;

    [HideInInspector] //다른 스크립트에서 불러오지만 사람이 건드리진 못하게
    public float hiddenValue;

    [SerializeField]
    private int privateInt = 100;

    public Person person;

	// Use this for initialization
	void Start () {
        person = new Person();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

//[Serializable]
[SerializeField]
public class Person {
    public string address;
}
