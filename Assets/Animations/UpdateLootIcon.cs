using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLootIcon : StateMachineBehaviour
{
    ExpPreviewPanelDrawer drawer;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TryCheckDrawer(animator);

        if (drawer.exp.curEncounter is Combat combat)
            drawer.lootIcon.sprite = combat.curLoot.data.icon;
        if (drawer.exp.curEncounter is ContainerEncounter cont)
            drawer.lootIcon.sprite = cont.curLoot.data.icon;
    }

    void TryCheckDrawer(Animator animator)
    {
        if (drawer == null)
            drawer = animator.GetComponentInParent<ExpPreviewPanelDrawer>();
    }
}