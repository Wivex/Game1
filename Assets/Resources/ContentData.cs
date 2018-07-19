using UnityEngine;

public abstract class ContentData : ScriptableObject
{
    [Header("Content")]
    public Sprite icon;
    public new string name;
}