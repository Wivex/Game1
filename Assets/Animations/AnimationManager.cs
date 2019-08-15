using System.Collections;
using UIEventDelegate;
using UnityEngine;

public class AnimationStateReference
{
    public AnimationState state;
}

public enum AnimationState
{
    Animating,
    Done
}

public class AnimationManager : MonoBehaviour
{
    public ReorderableEventList events;

    /// <summary>
    /// animation state reference from other script
    /// </summary>
    internal AnimationStateReference animStateRef;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Pass string value to gameObject animator trigger
    /// </summary>
    public void SetTrigger(string value)
    {
        animator.SetTrigger(value);
    }

    public void RunAllEvents()
    {
        EventDelegate.Execute(events.List);
    }

    public void RunEventAtIndex(int index)
    {
        events.List[index].Execute();
    }

    public void PauseAnimationForSecs(float sec)
    {
        StartCoroutine(PauseCour(sec));
    }

    public void NotifyOfAnimationState(AnimationState state)
    {
        animStateRef.state = state;
    }

    IEnumerator PauseCour(float sec)
    {
        animator.enabled = false;
        yield return new WaitForSeconds(sec);
        animator.enabled = true;
    }
}