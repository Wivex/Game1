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
    public bool selfDestroyOnStateExit;
    public AnimatorStateMonitorType animationFinishedEvent;

    internal event Action<Animator> AnimationFinished;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animationFinishedEvent == AnimatorStateMonitorType.OnStateEnter)
            AnimationFinished?.Invoke(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animationFinishedEvent == AnimatorStateMonitorType.OnStateExit)
            AnimationFinished?.Invoke(animator);
        if (selfDestroyOnStateExit)
            Destroy(animator.gameObject);
    }
}