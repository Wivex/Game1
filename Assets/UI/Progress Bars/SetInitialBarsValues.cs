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

        drawer.heroHpBar.SetInitialValue((float) drawer.exp.hero.curStats.health / drawer.exp.hero.baseStats.health);
        drawer.enemyHpBar.SetInitialValue((float)combat.enemy.curStats.health / combat.enemy.baseStats.health);
        drawer.heroEnergyBar.SetInitialValue((float) drawer.exp.hero.curStats.energy / drawer.exp.hero.baseStats.energy);
        drawer.enemyEnergyBar.SetInitialValue((float) combat.enemy.curStats.energy / combat.enemy.baseStats.energy);
    }

    void TryCheckDrawer(Animator animator)
    {
        if (drawer == null)
            drawer = animator.GetComponentInParent<ExpPreviewPanelDrawer>();
    }
}