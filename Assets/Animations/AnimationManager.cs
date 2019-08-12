using System.Collections;
using UIEventDelegate;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public ReorderableEventList events;

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

    IEnumerator PauseCour(float sec)
    {
        anim.enabled = false;
        yield return new WaitForSeconds(sec);
        anim.enabled = true;
    }
}