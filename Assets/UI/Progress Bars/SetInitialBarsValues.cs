using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitialBarsValues : StateMachineBehaviour
{
    ExpPreviewPanelDrawer drawer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TryCheckDrawer(animator);
        var combat = (drawer.exp.curEncounter as Combat);

        drawer.heroHpBar.SetInitialValue((float)combat.hero.HP / combat.hero.HPMax);
        drawer.enemyHpBar.SetInitialValue((float)combat.enemy.HP / combat.enemy.HPMax);
        drawer.heroEnergyBar.SetInitialValue((float)combat.hero.Energy / combat.hero.EnergyMax);
        drawer.enemyEnergyBar.SetInitialValue((float)combat.hero.Energy / combat.hero.EnergyMax);
    }

    void TryCheckDrawer(Animator animator)
    {
        if (drawer == null)
            drawer = animator.GetComponentInParent<ExpPreviewPanelDrawer>();
    }
}