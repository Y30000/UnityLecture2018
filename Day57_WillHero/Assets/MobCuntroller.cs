using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCuntroller : MonoBehaviour, IHitBoxResponder {

    HitBox hitbox;

    public void CollisionedWith(Collider collider)
    {
        PlayerController pc = collider.gameObject.GetComponentInParent<PlayerController>();
        pc.Beaten();
        hitbox.StopCheckingCollision();     //한방에 죽으니까 이렇게 사용가능
    }

    // Use this for initialization
    void Start () {
        hitbox = GetComponentInChildren<HitBox>();
        hitbox.SetResponder(this);
        hitbox.StartCheckingCollision();
    }
	
	// Update is called once per frame
	void Update () {
        hitbox.UpdateHitBox();  //체크하는 부분   //코루틴으로 속도조절 가능
	}
}
