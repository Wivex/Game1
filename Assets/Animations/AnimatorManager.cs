using System.Collections;
using UIEventDelegate;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [Tooltip("These Animators will be triggered by TriggerLinkedAnimators()")]
    public Animator[] linkedAnimators;

    internal bool animationFinished = true;

    Animator animator;

    void Awake()    
    {
        animator = GetComponent<Animator>();
    }

    internal static void Trigger(AnimationTrigger value, params AnimatorManager[] animManagers)
    {
        animManagers.ForEach(manager => manager.animator.SetTrigger(value.ToString()));
    }

    /// <summary>
    /// Triggers linked animators. Used to do that during other animation, using animation events
    /// </summary>
    public void TriggerLinkedAnimators(AnimationTrigger value)
    {
        linkedAnimators.ForEach(animator => animator.SetTrigger(value.ToString()));
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