using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Data Templates/Ability")]
public class AbilityData : ScriptableObject
{
    public Sprite icon;
    public int damage;
    public int cooldown;
    public DamageType damageType;
    public List<ItemEffect> effects;

    //public void Use(Unit actor, Unit target)
    //{
    //    if (damage != 0)
    //        //target.TakeDamage(new Damage(DamageType.Magic, damage), Texture);
    //    TryApplyEffects(actor, target);
    //    CDReset();
    //}

    //public void CDReset()
    //{
    //    curCooldown = maxCooldown;
    //}

    //public void TryApplyEffects(Unit actor, Unit target)
    //{
    //    foreach (var effect in effects)
    //    {
    //        //if (effect.XMLData.TargetSelf)
    //        //    actor.effects.Add(effect);
    //        //else
    //        //    target.effects.Add(effect);
    //    }
    //}
}
