using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostAttack : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if at all the swing happens from the ghost then we get hit and score is taken
        ScoreManager.instance.TakeScore(1);
    }

}
