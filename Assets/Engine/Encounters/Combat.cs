using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Random = UnityEngine.Random;

public class Combat : Encounter
{
    internal Hero hero;
    internal Enemy enemy;
    internal Unit actor, target;

    List<ItemData> lootDrops;

    bool looting;

    bool HeroFaster => hero.curStats.speed >= enemy.curStats.speed;

    public Combat(Expedition exp) : base(exp)
    {
        type = EncounterType.Combat;
        hero = exp.hero;
        enemy = NewEnemy();
        //TODO: update UI?
        //enemy.unitPreviewIcon = expedition.expPreviewPanel.objectIcon.transform;
        //enemy.unitDetailsIcon = UIManager.instance.expPanelDrawer.expDetailsPanelDrawer.enemyPanel.unitImage.transform;
        ResetAllCooldowns();
    }

    internal override void Update()
    {
        if (hero.Dead)
        {
            Kill(hero);
            // TODO: stop situation
        }
        else if (enemy.Dead)
        {
            Kill(enemy);
        }
        else
        {
            if (HeroFaster)
            {
                DoTurn(hero, enemy);
                DoTurn(enemy, hero);
            }
            else
            {
                DoTurn(enemy, hero);
                DoTurn(hero, enemy);
            }
        }
    }

    public void DoTurn(Unit actor, Unit target)
    {
        this.actor = actor;
        this.target = target;

        actor.initiative += actor.curStats.speed * CombatManager.i.combatSpeed;
        if (actor.initiative >= CombatManager.i.reqInitiativePerAction)
        {
            actor.initiative -= CombatManager.i.reqInitiativePerAction;
            DoActorAction();
        }
    }

    public void DoActorAction()
    {
        UpdateActorEffects();
        UpdateActorTactics();
        UpdateActorCooldowns();
    }

    public void UpdateActorEffects()
    {
        for (var i = actor.effects.Count - 1; i >= 0; i--)
            actor.effects[i].UpdateEffect();
    }

    public void UpdateActorTactics()
    {
        foreach (var tactic in actor.tacticsPreset.tactics)
        {
            // skip tactic if not all triggers are triggered
            if (tactic.triggers.Exists(trigger => !trigger.IsTriggered(enemy)))
                continue;
            tactic.action.DoAction(this);
            break;
        }
    }

    public void UpdateActorCooldowns()
    {
        foreach (var ability in actor.abilities)
        {
            if (ability.curCooldown > 0)
                ability.curCooldown--;
        }
    }

    public void ResetAllCooldowns()
    {
        foreach (var ability in hero.abilities) ability.curCooldown = 0;
        foreach (var ability in enemy.abilities) ability.curCooldown = 0;
    }

    public void Kill(Hero hero)
    {
        // TODO: implement
    }

    public void Kill(Enemy enemy)
    {
        if (!looting)
        {
            // set up item transfer animation
            looting = true;
            SpawnLoot();
            // show "dead" status icon
            //expedition.expPreviewPanel.enemyStatusIcon.enabled = true;
            //// start cycles of loot transfer
            //expedition.expPreviewPanel.lootAnim.SetTrigger(AnimationTrigger.StartTransferLoot.ToString());
            //// make combat icon disappear
            //expedition.expPreviewPanel.interAnim.SetTrigger(AnimationTrigger.EndEncounter.ToString());
        }

        // loot transfer process (run before each cycle)
        if (lootDrops.Count > 0)
        {
            var item = lootDrops.FirstOrDefault();
            //expedition.expPreviewPanel.lootIcon.sprite = item.icon;
            // lock situation Updater until animation ends
            //state = SituationState.RunningAnimation;
            lootDrops.Remove(item);
        }
        else
        {
            // stop animating item transfer
            //expedition.expPreviewPanel.lootAnim.SetTrigger(AnimationTrigger.StopTransferLoot.ToString());
            //// hero continue travelling
            //expedition.expPreviewPanel.heroAnim.SetTrigger(AnimationTrigger.EndEncounter.ToString());
            //// hide enemy icon
            //expedition.expPreviewPanel.eventAnim.SetTrigger(AnimationTrigger.EndEncounter.ToString());
            //Resolve();
        }
    }

    public void SpawnLoot()
    {
        lootDrops = new List<ItemData>();
        foreach (var loot in enemy.enemyData.lootTable)
        {
            // TODO: add stack count implementation
            if (Random.value < loot.dropChance)
                lootDrops.Add(loot.item);
        }
    }

    // NOTE: move to combat manager?
    // NOTE: increase chance with each iteration?
    Enemy NewEnemy()
    {
        var spawnTries = 0;
        while (enemy == null && spawnTries++ < 100)
        {
            foreach (var e in exp.curLocation.enemies)
            {
                if (Random.value < e.chance)
                    return new Enemy(e.enemyData);
            }
        }

        throw new Exception("Too many tries to spawn enemy");
    }
}