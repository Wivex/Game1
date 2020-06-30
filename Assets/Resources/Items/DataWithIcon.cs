using System;
using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

public abstract class DataWithIcon : ScriptableObject
{
    public Sprite icon;

    /// <summary>
    /// Undocumented support. Called when: same as OnEnable() + every SO values change (each keyboard input)
    /// </summary>
    void OnValidate()
    {
    }

    /// <summary>
    /// Called when: SO created/game started/SO clicked (once between game runs, if wasn't selected already)
    /// </summary>
    protected void OnEnable()
    {
        AutoLoadIcon();
    }

    protected void AutoLoadIcon()
    {
        if (!name.StartsWith("New ") && icon == null)
        {
            icon = GameManager.LoadNearbyAsset<Sprite>(this);
        }
    }
}