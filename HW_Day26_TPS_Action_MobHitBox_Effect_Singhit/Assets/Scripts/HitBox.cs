using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderState
{
    Closed,
    Open,
    Colliding
}

public interface IHitBoxResponder
{
    void CollisionWith(Collider collider);
}

public class HitBox : MonoBehaviour {
    public LayerMask mask;
    public BoxCollider hitBox;
    public Color inActiveColor;
    public Color CollisionOpenColor;
    public Color CollidingColor;

    public ColliderState state = ColliderState.Closed;
    IHitBoxResponder responder = null;

    private void OnDrawGizmos() //디버그용 Scens에서 호출됨
    {
        UpdateHitBox();
        CheckedGizmoColor();
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(hitBox.center, hitBox.size);
    }

    private void CheckedGizmoColor()
    {
        switch (state)
        {
            case ColliderState.Closed:
                Gizmos.color = inActiveColor;
                break;
            case ColliderState.Open:
                Gizmos.color = CollisionOpenColor;
                break;
            case ColliderState.Colliding:
                Gizmos.color = CollidingColor;
                break;
        }
    }

    public void UpdateHitBox()
    {
        if (state == ColliderState.Closed)
            return;
        Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(hitBox.center),
                                                  hitBox.size * .5f,
                                                  transform.rotation,
                                                  mask);
        
        //for (int i = 0; i < colliders.Length; i++)
        //{
        //    Collider aCollider = colliders[i];
        //    if (null != responder)
        //        responder.CollisionWith(aCollider);
        //}

        foreach (var aCollider in colliders)
        { 
            if (null != responder)
                responder.CollisionWith(aCollider);
        }

        state = colliders.Length > 0 ? ColliderState.Colliding : ColliderState.Open;
    }

    public void StartCheckingCollision()
    {
        state = ColliderState.Open;
        CheckedGizmoColor();
    }
    
    public void StopCheckingCollision()
    {
        state = ColliderState.Closed;
        CheckedGizmoColor();
    }

    public void SetResponder(IHitBoxResponder responder)
    {
        this.responder = responder;
    }
}