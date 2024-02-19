using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Land", false);    
    }
}
