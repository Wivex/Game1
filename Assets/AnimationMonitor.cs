using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMonitor : StateMachineBehaviour
{
    internal event Action AnimationsFinished;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimationsFinished?.Invoke();
    }
}