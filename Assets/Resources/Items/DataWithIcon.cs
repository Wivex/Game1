using System;
using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

public abstract class DataWithIcon : ScriptableObject
{
    internal Sprite icon;

    /// <summary>
    /// Shows if SO has been renamed and it's not just created. Helps to control OnEnable() events properly.
    /// </summary>
    protected bool initialized;

    /// <summary>
    /// Undocumented support. Called when: same as OnEnable() + every SO values change (each keyboard input)
    /// </summary>
    void OnValidate()
    {
    }

    /// <summary>
    /// Called when: SO created/game started/SO clicked (once between game runs, if wasn't selected already)
    /// </summary>
    void OnEnable()
    {
        AutoLoadIcon();
    }

    protected void AutoLoadIcon()
    {
        if (initialized)
        {
            icon = GameManager.LoadNearbyAsset<Sprite>(this);
        }
        else if (!name.StartsWith("New "))
            initialized = true;
    }
}