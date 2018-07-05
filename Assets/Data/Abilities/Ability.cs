using System.Collections.Generic;

public class Ability
{
    public int damage;
    public int curCooldown;
    public int maxCooldown;
    public bool Ready => curCooldown == 0;

    public List<Effect> effects;

    public void Use(Unit actor, Unit target)
    {
        if (damage != 0)
            //target.TakeDamage(new Damage(DamageType.Magic, damage), Texture);
        TryApplyEffects(actor, target);
        CDReset();
    }

    public void CDReset()
    {
        curCooldown = maxCooldown;
    }

    public void TryApplyEffects(Unit actor, Unit target)
    {
        foreach (var effect in effects)
        {
            //if (effect.XMLData.TargetSelf)
            //    actor.effects.Add(effect);
            //else
            //    target.effects.Add(effect);
        }
    }
}
