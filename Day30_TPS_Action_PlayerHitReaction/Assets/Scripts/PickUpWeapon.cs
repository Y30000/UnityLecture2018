﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : StateMachineBehaviour {

    PlayerController pc;
    Transform weaponHolder;
    Rigidbody rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = animator.GetComponent<PlayerController>();
        weaponHolder = pc.weaponHolder;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (weaponHolder.childCount == 0 && 0.22f <= stateInfo.normalizedTime)
        {
            GameObject weapon = pc.GetNearstWeaponIn(radius: 3f, angle: 180f, weaponTag: "RightWeapon");
            if (weapon == null)
            {
                animator.SetInteger("HoldingWeaponID", 0);
                return;
            }
            //Destroy(weapon.GetComponent<Rigidbody>());
            foreach (var c in weapon.GetComponents<Collider>())
                c.enabled = false;
            weapon.transform.SetParent(weaponHolder.transform);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            animator.SetBool("HasWeapon", true);
            animator.SetInteger("HoldingWeaponID", weapon.GetComponent<WeaponType>().weaponID);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
