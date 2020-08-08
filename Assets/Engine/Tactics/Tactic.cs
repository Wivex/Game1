using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityExtensions;

[Serializable]
public class Tactic
{
    public List<TacticTrigger> triggers;
    public TacticAction action;

    internal bool Triggered(Combat combat)
    {
        return triggers.All(trigger => trigger.Triggered(combat));
    }
}


public static class TacticExtensions
{
    /// <summary>
    /// Returns TacticAction based on triggers and possibility checks
    /// </summary>
    public static TacticAction PickAction(this List<Tactic> tactics, Combat combat)
    {
        var action =  tactics.FirstOrDefault(tactic => tactic.Triggered(combat))?.action;
        if (action != null)
        {
            return action;
        }

        Debug.Log($"{combat.actor.Name} couldn't pick an action and waits");
        return new TacticAction {actionType = ActionType.Wait};
    }
}