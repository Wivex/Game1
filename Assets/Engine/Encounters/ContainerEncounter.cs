using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class ContainerEncounter : NoEncounter
{
    internal ContainerData data;
    internal Item curLoot;

    // UNDONE
    List<Item> lootDrops;

    bool looting;

    //internal AnimationManager GetAnimManager(Unit unit) => unit is Hero ? mis.heroAM : mis.encounterAM;

    internal ContainerEncounter(Mission mis) : base(mis)
    {
        type = EncounterType.Container;
        hero = mis.hero;
        data = NewContainer();

        SpawnLoot();
        looting = true;
    }

    internal override void NextUpdate()
    {
        if (looting)
            NextItem();
    }

    //UNDONE
    void SpawnLoot()
    {
        lootDrops = new List<Item>();
        foreach (var loot in data.lootTable)
        {
            // TODO: add stack count implementation
            if (Random.value < loot.dropChance)
            {
                lootDrops.Add(new Item(loot.item));
            }
        }
    }

    void NextItem()
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
            looting = false;
            //mis.StartAnimation(AnimationTrigger.EndEncounter, mis.heroAM, mis.encounterAM);
            mis.curEncounter = null;
        }
    }


    ContainerData NewContainer()
    {
        // var spawnTries = 0;
        // while (data == null && spawnTries++ < 100)
        // {
        //     foreach (var cont in mis.curZone.pointsOfInterest)
        //     {
        //         if (Random.value < cont.chanceWeight)
        //             return cont.ContainerData;
        //     }
        // }

        throw new Exception("Too many tries to spawn container");
    }
}