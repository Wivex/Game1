using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Test : MonoBehaviour {
    
//    public DataBase dataBase;


//    protected void Awake()
//    {
//        // move to some manager
//        dataBase.Init();

//        CreateHero();
//    }

//    public void CreateHero()
//    {
//        var go = new GameObject("unit");
//        var unit = go.AddComponent<Unit>();
//        //unit.unitClass = "Warrior";
//        //unit.level = 1;
//        //unit.CalculateBaseStats();
//    }

//    public abstract class StatEffect
//    {
//        public abstract StatValues Apply(StatValues stats);
//    }


//    public class DamageEffect : StatEffect
//    {
//        public int additionalAttack;

//        public override StatValues Apply(StatValues stats)
//        {
//            stats.attack += additionalAttack;
//            return stats;
//        }
//    }
//    public class SuperDamageEffect : StatEffect
//    {
//        public int coeff;

//        public override StatValues Apply(StatValues stats)
//        {
//            stats.attack += stats.defence * coeff;
//            return stats;
//        }
//    }
//}
