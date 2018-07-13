using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data Templates/Classes/Unit Data")]
public class UnitData : ContentData
{
    [Header("Unit Data")]
    public StatValues stats;
    public List<AbilityData> abilities;
}