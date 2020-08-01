using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityExtensions;

[Serializable]
public class Tactic
{
    public List<TacticTrigger> triggers;
    public TacticAction action;
}


public static class TacticExtensions
{
    /// <summary>
    /// Returns true if all triggers are triggered
    /// </summary>
    public static bool AllTriggered(this List<TacticTrigger> triggers, EnemyEncounter enc)
    {
         return triggers.All(trigger => trigger.IsTriggered(enc.enemy));
    }

    /// <summary>
    /// Returns true if all triggers are triggered
    /// </summary>
    public static bool IsPossible(this TacticAction action, EnemyEncounter enc)
    {
        return triggers.All(trigger => trigger.IsTriggered(enc.enemy));
    }


    /// <summary>
    /// Returns TacticAction based on triggers and possibility checks
    /// </summary>
    public static TacticAction PickAction(this List<Tactic> tactics)
    {
        foreach (var tactic in tactics)
        {
            if (tactic.triggers.Exists(trigger => !trigger.IsTriggered(enemy)))
                continue;
            tactic.action.DoAction(this);
        }

        return null;
    }
}