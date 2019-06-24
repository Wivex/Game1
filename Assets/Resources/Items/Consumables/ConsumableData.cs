using System.Collections.Generic;
using SubjectNerd.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Items/Consumable Data")]
public class ConsumableData : ItemData
{
    [Header("Consumable")]
    public int charges;
    [Reorderable]
    public List<Effect> effects;
}