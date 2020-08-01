using System;
using System.Collections;
using System.Collections.Generic;
using UnityExtensions;

[Serializable]
public class Tactic
{
    public List<TacticTrigger> triggers;
    public TacticAction action;
}