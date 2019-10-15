using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : StateMachineBehaviour
{
    [Tooltip("Determines what animation events to monitor")]
    public bool monitorAnimationStart, monitorAnimationEnd;
    [Tooltip("Destroys object on animation end")]
    public bool destroyObjectOnEnd;

    AnimatorManager AM;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monitorAnimationStart)
        {
            TryCheckAM(animator);
            AM.animationFinished = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monitorAnimationEnd)
        {
            TryCheckAM(animator);
            AM.animationFinished = true;
        }

        if (destroyObjectOnEnd)
        {
            Destroy(animator.gameObject);
        }
    }

    void TryCheckAM(Animator animator)
    {
        if (AM == null)
            AM = animator.GetComponent<AnimatorManager>();
    }
}
