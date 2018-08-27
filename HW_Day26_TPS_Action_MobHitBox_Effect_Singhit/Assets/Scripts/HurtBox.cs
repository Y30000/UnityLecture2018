using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour {

    public BoxCollider hurtBox;
    public Color CollisionOpenColor;
    public Color CollidingColor;

    public ColliderState state = ColliderState.Open;

    private void OnDrawGizmos() //디버그용 Scens에서 호출됨
    {
        CheckedGizmoColor();
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(hurtBox.center, hurtBox.size);
    }

    private void CheckedGizmoColor()
    {
        switch (state)
        {
            case ColliderState.Open:
                Gizmos.color = CollisionOpenColor;
                break;
            case ColliderState.Colliding:
                Gizmos.color = CollidingColor;
                break;
        }
    }

    public void GetHitBy(int damage)
    {
        state = ColliderState.Colliding;
        Invoke("ResetState", 0.1f);
    }

    void ResetState()
    {
        state = ColliderState.Open;
    }
}
