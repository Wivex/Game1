using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    internal event Action AnimationEvent;
    internal Animator animator;
    internal AnimatorStateMonitor animMonitor;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animMonitor = animator.GetBehaviour<AnimatorStateMonitor>();
    }

    public void PassAnimationEvent()
    {
        AnimationEvent?.Invoke();
    }
}
