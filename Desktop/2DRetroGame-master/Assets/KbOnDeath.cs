using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KbOnDeath : StateMachineBehaviour
{

    public float moveDistance = .001f; // Distance to move

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 currentPosition = animator.transform.position;

        //if the player isn't grounded I want there to be more knockback
        if (!PlayerController.grounded)
        {
            moveDistance = moveDistance * 2;
        }





    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


}
