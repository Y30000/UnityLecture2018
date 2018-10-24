using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneBehaviour : StateMachineBehaviour {

    RectTransform ui;

    void OnPlayerDeath()
    {
        GameFlow.Instance.player.playableOn = false;
        GameFlow.Instance.fsm.SetTrigger("Death");
        ui.gameObject.SetActive(false);
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ui = GameFlow.Instance.playScene;
        ui.gameObject.SetActive(true);

        GameFlow.Instance.player.playableOn = true;
        GameFlow.Instance.player.FirstDash();
        GameFlow.Instance.player.OnDeath += OnPlayerDeath;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

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
