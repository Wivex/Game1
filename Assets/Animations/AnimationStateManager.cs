using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateManager : StateMachineBehaviour
{
    [Tooltip("Determines what animation events to monitor")]
    public bool monitorAnimationStart, monitorAnimationEnd;
    [Tooltip("Destroys object on animation end")]
    public bool destroyObjectOnEnd;
    [Tooltip("Resets transition trigger on animation start")]
    public bool resetTriggerOnStart;
    [HideIfNotBool("resetTriggerOnStart")]
    public AnimationTrigger triggerValue;

    AnimationManager AM;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     if (monitorAnimationStart)
    //     {
    //         TryCheckAM(animator);
    //         AM.animationFinished = false;
    //     }
    //
    //     if (resetTriggerOnStart)
    //     {
    //         TryCheckAM(animator);
    //         AM.ResetTrigger(triggerValue);
    //     }
    // }
    //
    // // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     if (monitorAnimationEnd)
    //     {
    //         TryCheckAM(animator);
    //         AM.animationFinished = true;
    //     }
    //
    //     if (destroyObjectOnEnd)
    //     {
    //         Destroy(animator.gameObject);
    //     }
    // }

    void TryCheckAM(Animator animator)
    {
        if (AM == null)
            AM = animator.GetComponent<AnimationManager>();
    }
}
