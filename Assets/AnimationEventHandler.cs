using System;
using System.Collections;
using System.Collections.Generic;
using UIEventDelegate;
using UnityEngine;
using UnityEngine.UI;

public class AnimationEventHandler : MonoBehaviour
{
    public Animator[] linkedAnimators;
    public ReorderableEventList codeEvents;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerLinkedAnimators(AnimationTrigger trigger)
    {
        foreach (var anim in linkedAnimators)
        {
            anim.SetTrigger(trigger.ToString());
        }
    }

    public void RunCodeEvents()
    {
        EventDelegate.Execute(codeEvents.List);
    }

    public void PauseAnimationForSecs(float sec)
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