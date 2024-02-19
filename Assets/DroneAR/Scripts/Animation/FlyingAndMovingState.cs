using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAndMovingState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("MoveUp", true);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("MoveUp", false);
    }
}
