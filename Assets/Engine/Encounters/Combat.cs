using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

internal enum CombatPhase
{
    UpdateOverTimeEffects,
    TurnOrderCheck,
    PickActorAction,
    ProcEffects,
    Looting
}

public class Combat : NoEncounter
{
    internal const int AP_AccumulationLimitMod = 2;

    internal Enemy enemy;
    internal Unit actor, target;
    internal TacticAction curAction;

    List<Item> lootDrops;
    Item curLoot;

    int heroAP, enemyAP;
    bool fasterUnitFinishedTurn;
    CombatPhase phase = CombatPhase.UpdateOverTimeEffects;

    #region EVENTS

    internal event Action<TacticAction> ActorActionPicked;
    internal event Action<Unit, Damage> DamageTaken;
    internal event Action<Unit, EffectOverTime> EffectAdded;
    internal event Action CombatTurnStarted;
    
    #endregion

    internal Combat(Mission mis) : base(mis)
    {
        type = EncounterType.Combat;
        // TODO used mission one
        enemy = new Enemy(mis.route.curZone.enemies.PickOne().enemyData);
        phase = CombatPhase.TurnOrderCheck;
    }

    void NewCombatTurn()
    {
        hero.UpdateCooldowns();
        enemy.UpdateCooldowns();
        phase = CombatPhase.UpdateOverTimeEffects;
        CombatTurnStarted?.Invoke();
    }

    internal override void EncounterUpdate()
    {
        switch (phase)
        {
            case CombatPhase.UpdateOverTimeEffects:
                UpdateOverTimeEffects();
                EncounterUpdate();
                break;
            case CombatPhase.TurnOrderCheck:
                TurnOrderCheck();
                break;
            case CombatPhase.PickActorAction:
                PickActorAction();
                break;
            case CombatPhase.ProcEffects:
                ProcEffects();
                break;
            case CombatPhase.Looting:
                NextItemDrop();
                break;
        }
    }

    bool CombatFinished()
    {
        if (hero.Dead)
        {
            // TODO: end mission
            return true;
        }
        if (enemy.Dead)
        {
            phase = CombatPhase.Looting;
            return true;
        }
        return false;
    }

    void UpdateOverTimeEffects()
    {
        hero.UpdateEffects(mis);
        enemy.UpdateEffects(mis);
        if (!CombatFinished())
            phase = CombatPhase.TurnOrderCheck;
    }

    void TurnOrderCheck()
    {
        heroAP = Math.Min(heroAP + hero.Speed, hero.Speed * AP_AccumulationLimitMod);
        enemyAP = Math.Min(enemyAP + enemy.Speed, enemy.Speed * AP_AccumulationLimitMod);
        actor = heroAP >= enemyAP ? (Unit) hero : enemy;
        target = heroAP >= enemyAP ? (Unit) enemy : hero;

        fasterUnitFinishedTurn = false;

        phase = CombatPhase.PickActorAction;
        PickActorAction();
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

                EncounterUpdate();
            }
            // Combat turn ended
            else
                NewCombatTurn();
        }
        else
        {
            phase = CombatPhase.ProcEffects;
            ActorActionPicked?.Invoke(curAction);
        }
    }

    internal void AddEffects(Unit unit, EffectOverTime effect)
    {
        unit.effectStacks.Add(effect);
        EffectAdded?.Invoke(unit, effect);
    }

    void ProcEffects()
    {
        curAction.Perform(this);
    }

    void EndCombat()
    {
        SpawnLoot();
        phase = CombatPhase.Looting;
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
        {
            resolved = true;
        }
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