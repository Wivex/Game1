using System.Collections;
using UIEventDelegate;
using UnityEngine;

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

    public void RunAllCodeEvents()
    {
        EventDelegate.Execute(codeEvents.List);
    }

    public void RunCodeEventAtIndex(int index)
    {
        codeEvents.List[index].Execute();
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