using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealToPlayer : MonoBehaviour {

    public GameObject healFx;
    private bool isHealing = false;

    // Use this for initialization
    void Start () {
    
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !isHealing)
        {
            print("Heal");
            Destroy(Instantiate(healFx, collision.gameObject.transform.Find("FXPos")),2f);
            isHealing = true;
            Invoke("StatusBack", 2f);
        }
    }

    void StatusBack()
    {
        isHealing = false;
    }
}
