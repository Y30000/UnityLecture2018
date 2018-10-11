using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JavelinController : MonoBehaviour, IHitBoxResponder {
    public float trowingForce = 35f;

    Rigidbody rb;
    HitBox hitbox;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hitbox = GetComponentInChildren<HitBox>();
        hitbox.SetResponder(this);
        hitbox.StartCheckingCollision();
    }

    private void OnEnable()
    {
        hitbox.StartCheckingCollision(); ;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //transform.parent = null;
    }

    private void OnDisable()
    {
        hitbox.StopCheckingCollision();
        DOTween.Kill(transform);
    }

    internal void Trow()
    {
        rb.isKinematic = false;

        rb.AddForce(transform.right * trowingForce, ForceMode.VelocityChange);
        transform.DORotate(-transform.forward * 80f, 3f);
    }

    private void Update()
    {
        hitbox.UpdateHitBox();
    }

    public void CollisionedWith(Collider collider)
    {
        rb.isKinematic = true;
        DOTween.Kill(transform);
        transform.SetParent(collider.gameObject.transform);
        hitbox.StopCheckingCollision();

        HurtBox hurtBox = collider.GetComponent<HurtBox>();
        if (hurtBox != null)
            hurtBox.GetHitBy(1);

        HitReaction hr = collider.GetComponentInParent<HitReaction>();
        if (hr != null)
            hr.Beaten();
    }
}
