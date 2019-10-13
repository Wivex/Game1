using System;
using System.Collections;
using System.Collections.Generic;
using SubjectNerd.Utilities;

[Serializable]
public class Tactic
{
    // can't use this attribute for nested arrays
    //[Reorderable(ReorderableNamingType.VariableValue, "triggerType")]
    public List<TacticTrigger> triggers;
    public TacticAction action;
}