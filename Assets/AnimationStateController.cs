using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : StateMachineBehaviour
{
    event Action AnimationSequenceFinished;
    MissionOverviewPanelDrawer drawer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (drawer == null)
        {
            drawer = animator.GetComponentInParent<MissionOverviewPanelDrawer>();
            AnimationSequenceFinished += drawer.mis.NextAction;
        }

        AnimationSequenceFinished();
    }
}