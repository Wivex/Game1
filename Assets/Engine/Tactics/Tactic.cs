using System;
using System.Collections;
using System.Collections.Generic;
//using SubjectNerd.Utilities;
using UnityEngine;

[Serializable]
public class Tactic
{
    public List<TacticTrigger> triggers;
    public TacticAction action;
}