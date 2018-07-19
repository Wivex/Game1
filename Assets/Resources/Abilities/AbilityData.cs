using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data/Ability")]
public class AbilityData : ScriptableObject
{
    public Sprite icon;
    public int damage;
    public int cooldown;
    public DamageType damageType;
    //public List<ItemEffect> effects;
}
