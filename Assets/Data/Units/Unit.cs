using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Unit : MonoBehaviour
    {
        public string unitClass;
        public int level;

        public UnitStats baseStats;
        public UnitStats currentStats;

        public EquipmentCollection Outfit = new EquipmentCollection();

        public void CalculateBaseStats()
        {
            baseStats = DataBase.GetUnitBaseStats(unitClass, level);
            foreach(var equip in Outfit)
            {
                foreach(var effect in equip.effects)
                {
                    effect.Apply(ref baseStats);
                }
            }
        }

        //public void TakeDamage(int damage)
        //{
        //    curHealth -= damage;
        //    var floatingText = Instantiate(AM.floatingTextPrefab, transform);
        //    floatingText.GetComponent<TextMeshProUGUI>().text = $"-{damage}";
        //}

        [System.Serializable]
        public class EquipmentCollection : IEnumerable<Equipment>
        {
            [System.Serializable]
            private struct SlotWithEquipment
            {
                public Slot slot;
                public Equipment equipment;
            }
            [SerializeField]
            private List<SlotWithEquipment> slots = new List<SlotWithEquipment>();

            public Equipment this[Slot slot]
            {
                get { return slots.FirstOrDefault(s => s.slot == slot).equipment; }
                set {
                    var index = -1;
                    for (int i = 0; i < slots.Count; i++)
                        if (slots[i].slot == slot)
                            index = i;
                    if (index < 0)
                        slots.Add(new SlotWithEquipment() { slot = slot, equipment = value });
                    else
                        slots[index] = new SlotWithEquipment() { slot = slot, equipment = value };
                }
            }

            public IEnumerator<Equipment> GetEnumerator()
            {
                foreach (var s in slots)
                    yield return s.equipment;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                foreach (var s in slots)
                    yield return s.equipment;
            }
        }
    }

    public enum Slot
    {
        Head,
        Amulet,
        Body,
        Belt,
        Gloves,
        Boots,
        Ring1,
        Ring2,
        MainHand,
        OffHand
    }
}
