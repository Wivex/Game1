using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateWatch : StateMachineBehaviour
{
    AnimationManager manager;

    void TryFindManager(Animator animator)
    {
        if (manager == null)
            manager = animator.GetComponent<AnimationManager>();
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TryFindManager(animator);
        manager.state = AnimationState.InProgress;
        Debug.Log("Animation Started");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TryFindManager(animator);
        manager.state = AnimationState.Finished;
        Debug.Log("Animation Finished");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Animation Updated");
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Animation Moved");
    }

    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Animation IK");
    }
}
