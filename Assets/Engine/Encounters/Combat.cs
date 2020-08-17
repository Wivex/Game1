using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

internal enum CombatPhase
{
    UpdateEffects,
    TurnOrderCheck,
    PickActorAction,
    ApplyActionEffects,
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
    CombatPhase phase = CombatPhase.UpdateEffects;

    #region EVENTS

    internal event Action<TacticAction> ActorActionPicked; 
    internal event Action<Unit, Damage> DamageTaken;
    internal event Action CombatTurnStarted;
    bool AnybodyDead => hero.Dead || enemy.Dead;

    #endregion

    internal Combat(Mission mis) : base(mis)
    {
        type = EncounterType.Combat;
        // TODO used mission one
        enemy = new Enemy(mis.route.curZone.enemies.PickOne().enemyData);
        phase = CombatPhase.TurnOrderCheck;
    }

    internal override void EncounterUpdate()
    {
        if (!AnybodyDead)
            switch (phase)
            {
                case CombatPhase.UpdateEffects:
                    UpdateEffects();
                    EncounterUpdate();
                    break;
                case CombatPhase.TurnOrderCheck:
                    TurnOrderCheck();
                    break;
                case CombatPhase.PickActorAction:
                    PickActorAction();
                    break;
                case CombatPhase.ApplyActionEffects:
                    ApplyActionEffects();
                    break;
                case CombatPhase.Looting:
                    NextItemDrop();
                    break;
            }
        else
        {
            phase = CombatPhase.Looting;
            EncounterUpdate();
        }
    }

    void NewCombatTurn()
    {
        hero.abilities.ForEach(abil => abil.NextTurn());
        enemy.abilities.ForEach(abil => abil.NextTurn());
        phase = CombatPhase.UpdateEffects;
        CombatTurnStarted?.Invoke();
    }

    void UpdateEffects()
    {
        hero.effects.ForEach(effect => effect.NextTurn(mis, actor));
        enemy.abilities.ForEach(abil => abil.NextTurn());
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
            phase = CombatPhase.ApplyActionEffects;
            ActorActionPicked?.Invoke(curAction);
        }
    }

    void ApplyActionEffects()
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