using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingAndMovingState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("MoveDown", false);    
    }
}
