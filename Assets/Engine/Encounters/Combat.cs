using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

internal enum CombatPhase
{
    TurnOrderCheck,
    PickActorAction,
    ApplyPickedAction,
    UpdateEffectsOverTime,
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

    int heroAP, enemyAP;
    bool fasterUnitFinishedTurn;
    CombatPhase phase;

    #region EVENTS

    internal event Action<TacticAction> ActorActionPicked;
    internal event Action CombatTurnStarted;
    
    #endregion

    internal Combat(Mission mis) : base(mis)
    {
        type = EncounterType.Combat;
        // TODO used mission one
        enemy = new Enemy(mis.route.curZone.enemies.PickOne().enemyData);
        NewCombatTurn();
    }

    void NewCombatTurn()
    {
        hero.UpdateCooldowns();
        enemy.UpdateCooldowns();
        hero.effects.CalculateEffectStacksPower();
        enemy.effects.CalculateEffectStacksPower();
        TurnOrderCheck();
        CombatTurnStarted?.Invoke();
    }
    void TurnOrderCheck()
    {
        heroAP = Math.Min(heroAP + hero.Speed, hero.Speed * AP_AccumulationLimitMod);
        enemyAP = Math.Min(enemyAP + enemy.Speed, enemy.Speed * AP_AccumulationLimitMod);
        actor = heroAP >= enemyAP ? (Unit) hero : enemy;
        target = heroAP >= enemyAP ? (Unit) enemy : hero;

        fasterUnitFinishedTurn = false;

        phase = CombatPhase.PickActorAction;
        NextEncounterAction();
    }

    internal override void NextEncounterAction()
    {
        switch (phase)
        {
            case CombatPhase.UpdateEffectsOverTime:
                UpdateEffectsOverTime();
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
        }
    }

    void UpdateEffectsOverTime()
    {
        if (hero.effects.unappliedStacks > 0)
            hero.ApplyNextEffectStack();
        else if (enemy.effects.unappliedStacks > 0)
            enemy.ApplyNextEffectStack();
        if (!CombatFinished())
            phase = CombatPhase.TurnOrderCheck;
    }

    bool CombatFinished()
    {
        if (hero.Dead)
        {
            phase = CombatPhase.HeroDeath;
            return true;
        }
        if (enemy.Dead)
        {
            phase = CombatPhase.Looting;
            SpawnLoot();
            return true;
        }
        return false;
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

                NextEncounterAction();
            }
            // Combat turn ended
            else
                NewCombatTurn();
        }
        else
        {
            phase = CombatPhase.ApplyPickedAction;
            ActorActionPicked?.Invoke(curAction);
        }
    }

    void ApplyPickedAction()
    {
        curAction.Apply(this);
        CombatFinished();
        phase = CombatPhase.PickActorAction;
        // NextEncounterAction() called by animation
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