using System;
using System.Collections;
using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[Serializable]
public class TacticsPreset
{
    public string presetName;
    [Reorderable]
    public List<Tactic> tactics;
}