﻿using System.Collections;
using UIEventDelegate;
using UnityEngine;

/// <summary>
/// Enum wrapper class to pass enum as reference from other scripts
/// </summary>
public class AnimationStateReference
{
    public AnimationState state;
}

public enum AnimationState
{
    InProgress,
    Finished
}

public class AnimationManager : MonoBehaviour
{
    /// <summary>
    /// Animation state reference from other script
    /// </summary>
    internal AnimationStateReference animStateRef;

    Animator animator;

    void Awake()    
    {
        animator = GetComponent<Animator>();
    }

    internal static void Trigger(AnimationTrigger value, params AnimationManager[] managers)
    {
        managers.ForEach(manager => manager.animator.SetTrigger(value.ToString()));
    }

    /// <summary>
    /// Changes animation state in the linked object
    /// </summary>
    public void NotifyAnimationStateChanged(AnimationState state)
    {
        animStateRef.state = state;
    }

    public void PauseAnimationForSecs(float sec)
    {
        StartCoroutine(PauseCour(sec));
    }

    IEnumerator PauseCour(float sec)
    {
        animator.enabled = false;
        yield return new WaitForSeconds(sec);
        animator.enabled = true;
    }
}