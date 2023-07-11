using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ghostSurvey : StateMachineBehaviour
{
    public float moveDistance = 1f;
    private bool movingRight = true;
    private float moveMaxRight;
    private float moveMaxLeft;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //startX should be calling from enemy script wehn I need to make a return state

        // get the starting position of the ghost
        float startX = animator.transform.position.x;

        // calculate the maximum distance the ghost can move in either direction
        moveMaxRight = startX + moveDistance;
        moveMaxLeft = startX - moveDistance;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // current x position of ghost
        float currentX = animator.transform.position.x;

        // move the ghost to the right
        if (movingRight)
        {
            animator.transform.Translate(Vector3.right * Time.deltaTime);

            // check if the ghost has reached its maximum distance to the right
            if (currentX >= moveMaxRight)
            {
                movingRight = false;
                animator.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }

        // move the ghost to the left
        else
        {
            animator.transform.Translate(Vector3.left * Time.deltaTime);

            // check if the ghost has reached its maximum distance to the left
            if (currentX <= moveMaxLeft)
            {
                movingRight = true;
                animator.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("follow");
    }
}
