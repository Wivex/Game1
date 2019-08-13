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

    internal AnimationStateReference animStateRef;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
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

    public void NotifyAnimationState(AnimationState state)
    {
        animStateRef.state = state;
    }

    IEnumerator PauseCour(float sec)
    {
        anim.enabled = false;
        yield return new WaitForSeconds(sec);
        anim.enabled = true;
    }
}