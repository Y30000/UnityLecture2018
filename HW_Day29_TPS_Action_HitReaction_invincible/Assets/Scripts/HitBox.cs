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
    public Collider[] hitBoxs;
    public Color inActiveColor;
    public Color collisionOpenColor;
    public Color collidingColor;

    public ColliderState state = ColliderState.Closed;
    IHitBoxResponder responder = null;
    List<Collider> colliderList;

    private void Awake()
    {
        colliderList = new List<Collider>();
    }

    private void OnDrawGizmos() //디버그용 Scens에서 호출됨
    {
        //UpdateHitBox();
        CheckedGizmoColor();
        Gizmos.matrix = transform.localToWorldMatrix;
        //Gizmos.DrawCube(hitBox.center, hitBox.size);
        foreach (var c in hitBoxs)
        {
            if(c.GetType() == typeof(BoxCollider))
            {
                BoxCollider bc = (BoxCollider)c;
                Gizmos.DrawCube(bc.center, bc.size);
            }
            else if(c.GetType() == typeof(SphereCollider))
            {
                SphereCollider sc = (SphereCollider)c;
                Gizmos.DrawSphere(sc.center, sc.radius);
            }

        }

    }

    private void CheckedGizmoColor()
    {
        switch (state)
        {
            case ColliderState.Closed:
                Gizmos.color = inActiveColor;
                break;
            case ColliderState.Open:
                Gizmos.color = collisionOpenColor;
                break;
            case ColliderState.Colliding:
                Gizmos.color = collidingColor;
                break;
        }
    }

    public void UpdateHitBox()
    {
        if (state == ColliderState.Closed)
            return;
        foreach (var c in hitBoxs)
        {
            if(c.GetType() == typeof(BoxCollider))
            {
                BoxCollider bc = (BoxCollider)c;
                Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(bc.center),
                                                  bc.size * .5f,    //center로 부터 앞뒤좌우위아래, size는 전체 길이
                                                  transform.rotation,
                                                  mask);

                colliderList.AddRange(colliders);
            }
            else if(c.GetType() == typeof(SphereCollider))
            {
                SphereCollider sc = (SphereCollider)c;
                Collider[] colliders = Physics.OverlapSphere(transform.TransformPoint(sc.center),
                                               sc.radius,
                                               mask);

                colliderList.AddRange(colliders);
            }
            
        }


        //for (int i = 0; i < colliders.Length; i++)
        //{
        //    Collider aCollider = colliders[i];
        //    if (null != responder)
        //        responder.CollisionWith(aCollider);
        //}

        foreach (var c in colliderList)
        {
            // C# 6.0: responder?.CollisionWith(c)
            if (null != responder)
                responder.CollisionWith(c);
        }

        state = colliderList.Count > 0 ? ColliderState.Colliding : ColliderState.Open;
        colliderList.Clear();
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