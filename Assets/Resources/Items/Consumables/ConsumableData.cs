using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Items/Consumable Data")]
public class ConsumableData : ItemData
{
    [Header("Consumable")]
    public int charges;
    public List<Effect> effects;
}