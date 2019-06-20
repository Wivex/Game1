using System;
using System.Collections;
using System.Collections.Generic;
using UIEventDelegate;
using UnityEngine;
using UnityEngine.UI;

public class AnimationEventHandler : MonoBehaviour
{
    public Animator[] targetAnimators;
    public ReorderableEventList animationFinishedEvents;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void InformThatAnimating()
    {
        foreach (var anim in targetAnimators)
        {
            anim.SetBool("animating", true);
        }
    }

    public void InformThatNotAnimating()
    {
        foreach (var anim in targetAnimators)
        {
            anim.SetBool("animating", false);
        }
    }

    public void RunAnimationFinishedEvents()
    {
        EventDelegate.Execute(animationFinishedEvents.List);
    }

    public void PauseAnimationFor(float sec)
    {
        StartCoroutine(PauseCour(sec));
    }

    IEnumerator PauseCour(float sec)
    {
        anim.enabled = false;
        yield return new WaitForSeconds(sec);
        anim.enabled = true;
    }
}