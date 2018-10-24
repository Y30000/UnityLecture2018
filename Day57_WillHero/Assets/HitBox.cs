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
    void CollisionedWith(Collider collider);
}

public class HitBox : MonoBehaviour {
    public LayerMask mask;
    public Collider[] hitBoxes;
    public Color inactiveColor;
    public Color collisionOpenColor;
    public Color collidingColor;
    public bool drawGizmo = true;
    public ColliderState state = ColliderState.Closed;

    IHitBoxResponder responder = null;
    List<Collider> colliderList;

    void Awake()
    {
        colliderList = new List<Collider>();    
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmo)
            return;

        //UpdateHitBox();
        CheckGizmoColor();
        Gizmos.matrix = transform.localToWorldMatrix;
        foreach (var c in hitBoxes) {
            if (c.GetType() == typeof(BoxCollider))
            {
                BoxCollider bc = (BoxCollider) c;
                Gizmos.DrawCube(bc.center, bc.size);
            }
            if (c.GetType() == typeof(SphereCollider))
            {
                SphereCollider sc = (SphereCollider)c;
                Gizmos.DrawSphere(sc.center, sc.radius);
            }
        }
    }

    private void CheckGizmoColor()
    {
        switch (state)
        {
            case ColliderState.Closed:
                Gizmos.color = inactiveColor;
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
        colliderList.Clear();

        if (state == ColliderState.Closed)
        {
            return;
        }
        
        foreach(var c in hitBoxes)
        {
            if (c.GetType() == typeof(BoxCollider))
            {
                BoxCollider bc = (BoxCollider)c;
                Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(bc.center),
                                                          bc.size * 0.5f,
                                                          transform.rotation,
                                                          mask,
                                                          QueryTriggerInteraction.Collide);
                colliderList.AddRange(colliders);
            }
            if (c.GetType() == typeof(SphereCollider))
            {
                SphereCollider sc = (SphereCollider)c;
                Collider[] colliders = Physics.OverlapSphere(transform.TransformPoint(sc.center),
                                                             sc.radius,
                                                             mask,
                                                             QueryTriggerInteraction.Collide);
                colliderList.AddRange(colliders);
            }
        }
        foreach(var c in colliderList)
        {
            //C# 6.0: responder?.collisionedWith(c);
            if (responder != null)
                responder.CollisionedWith(c);
        }
        if (state != ColliderState.Closed)
            state = colliderList.Count > 0 ? ColliderState.Colliding : ColliderState.Open;      //responder 에서 hitbox상태를 닫을 수도 있기 때문에 돌던건 마저돌도록 처리해줌
    }

    public void StartCheckingCollision()
    {
        state = ColliderState.Open;
    }

    public void StopCheckingCollision()
    {
        state = ColliderState.Closed;
    }

    public void SetResponder(IHitBoxResponder responder)
    {
        this.responder = responder;
    }
}