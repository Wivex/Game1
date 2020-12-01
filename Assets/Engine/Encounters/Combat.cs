using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

internal enum CombatPhase
{
    NewCombatTurnSetUp,
    PickActorAction,
    ApplyPickedAction,
    ApplyNextActorEffectOverTime,
    Looting,
    HeroDeath
}

public class Combat : NoEncounter
{
    internal const int AP_AccumulationLimitMod = 2;

    internal Enemy enemy;
    internal Unit actor, target;
    internal TacticAction curAction;

    List<Item> lootDrops;
    Item curLoot;

    bool fasterUnitFinishedTurn;
    CombatPhase phase;

    #region EVENTS

    internal event Action<TacticAction> ActorActionPicked;
    internal event Action NewCombatTurnStarted;
    
    #endregion

    internal Combat(Mission mis) : base(mis)
    {
        type = EncounterType.Combat;
        // TODO used mission one
        enemy = new Enemy(mis.route.curZone.enemies.PickOne().enemyData);
    }

    void NewCombatTurnSetUp()
    {
        hero.AP = Math.Min(hero.AP + hero.Speed, hero.Speed * AP_AccumulationLimitMod);
        enemy.AP = Math.Min(enemy.AP + enemy.Speed, enemy.Speed * AP_AccumulationLimitMod);
        actor = hero.AP >= enemy.AP ? (Unit) hero : enemy;
        target = hero.AP >= enemy.AP ? (Unit) enemy : hero;
        fasterUnitFinishedTurn = false;
        NewCombatTurnStarted?.Invoke();
        ActorTurnSetUp();
    }

    void ActorTurnSetUp()
    {
        actor.UpdateCooldowns();
        actor.unitEffects.CalculateEffectPower();
        phase = CombatPhase.ApplyNextActorEffectOverTime;
        NextEncounterAction();
    }

    internal override void NextEncounterAction()
    {
        switch (phase)
        {
            case CombatPhase.NewCombatTurnSetUp:
                NewCombatTurnSetUp();
                break;
            case CombatPhase.ApplyNextActorEffectOverTime:
                ApplyNextActorEffectOverTime();
                break;
            case CombatPhase.PickActorAction:
                PickActorAction();
                break;
            case CombatPhase.ApplyPickedAction:
                ApplyPickedAction();
                break;
            case CombatPhase.Looting:
                NextItemDrop();
                break;
            case CombatPhase.HeroDeath:
                //UNDONE
                break;
        }
    }

    void ApplyNextActorEffectOverTime()
    {
        if (actor.unitEffects.unappliedEffectsNumber > 0)
        {
            actor.ApplyNextEffectType();
            CombatFinishedCheck();
        }
        else
        {
            phase = CombatPhase.PickActorAction;
            NextEncounterAction();
        }
    }

    void PickActorAction()
    {
        curAction = actor.tactics.PickAction(this);
        if (curAction.actionType == ActionType.Wait)
        {
            // if faster unit can't do more actions
            if (!fasterUnitFinishedTurn)
            {
                fasterUnitFinishedTurn = true;
                // switch actor and target
                var actorRole = actor;
                actor = target;
                target = actorRole;
                ActorTurnSetUp();
            }
            // Combat turn ended
            else
                NewCombatTurnSetUp();
        }
        else
        {
            phase = CombatPhase.ApplyPickedAction;
            actor.AP -= TacticAction.APCost;
            ActorActionPicked?.Invoke(curAction);
        }
    }

    void ApplyPickedAction()
    {
        curAction.Apply(this);
        phase = CombatPhase.PickActorAction;
        CombatFinishedCheck();
    }

    void CombatFinishedCheck()
    {
        if (hero.Dead)
            phase = CombatPhase.HeroDeath;
        if (enemy.Dead)
        {
            phase = CombatPhase.Looting;
            SpawnLoot();
        }
    }

    void NextItemDrop()
    {
        if (lootDrops.Any())
        {
            // TODO: use Queue or Stack instead
            curLoot = lootDrops.ExtractFirstElement();
            hero.backpack.Add(curLoot);
            //mis.StartAnimation(AnimationTrigger.StartTransferLoot, mis.lootAM, mis.interactionAM, mis.locationAM);
        }
        else
            FinishEncounter();
    }

    //UNDONE
    void SpawnLoot()
    {
        lootDrops = new List<Item>();
        foreach (var loot in enemy.data.lootTable)
        {
            // TODO: add stack count implementation
            if (Random.value < loot.dropChance)
            {
                lootDrops.Add(new Item(loot.item));
            }
        }
    }
}