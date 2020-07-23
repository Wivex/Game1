using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : StateMachineBehaviour
{
    internal event Action AnimationSequenceFinished, AnimationSequenceStarted;

    MissionOverviewPanelDrawer drawer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckDrawer(animator);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckDrawer(animator);
    }

    void CheckDrawer(Animator animator)
    {
        if (drawer == null)
        {
            drawer = animator.GetComponentInParent<MissionOverviewPanelDrawer>();
            AnimationSequenceFinished += drawer.OnAnimationSequenceFinished;
            AnimationSequenceStarted += drawer.OnAnimationSequenceStarted;
        }
    }
}