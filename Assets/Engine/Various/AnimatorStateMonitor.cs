using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimatorStateMonitorType
{
    OnStateEnter,
    OnStateExit
}

public class AnimatorStateMonitor : StateMachineBehaviour
{
    public AnimatorStateMonitorType animationFinishedEvent;

    internal event Action AnimationsFinished;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animationFinishedEvent == AnimatorStateMonitorType.OnStateEnter)
            AnimationsFinished?.Invoke();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animationFinishedEvent == AnimatorStateMonitorType.OnStateExit)
            AnimationsFinished?.Invoke();
    }
}