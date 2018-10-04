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

    internal void Trow()
    {
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
    }
}
