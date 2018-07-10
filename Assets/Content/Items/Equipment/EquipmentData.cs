using System;
using UnityEngine;

public enum EquipmentSlot
{
    Head,
    Body,
    Arms
}

[CreateAssetMenu(menuName = "Content/Data Templates/Equipment")]
public class EquipmentData : ItemData
{
    [Header("Equipment")]
    public int cost;



    //[System.NonSerialized] public Effect[] effects;

    //protected void Awake()
    //{
    //    effects = GetComponents<Effect>();
    //}
}
