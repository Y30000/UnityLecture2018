using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeapon : StateMachineBehaviour {

    //PlayerController pc;
    //Transform weaponHolder;
    //Transform weapon;
    //Rigidbody rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    pc = animator.GetComponent<PlayerController>();
    //    weaponHolder = pc.weapohHolder;
    //    weapon = weaponHolder.GetChild(0);
    //    weapon.SetParent(null);
    //    animator.SetBool("HasWeapon", false);
    //    rb = weapon.GetComponent<Rigidbody>();
    //    rb.isKinematic = false;
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform weaponHolder = animator.GetComponent<PlayerController>().weaponHolder;
        Transform weapon = weaponHolder.GetChild(0);
        weapon.SetParent(null);
        weapon.GetComponent<Rigidbody>().isKinematic = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
