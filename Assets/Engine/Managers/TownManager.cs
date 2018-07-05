using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownManager : MonoBehaviour
{
    public ExpeditionManager EM;
    public Button newExpButtonPrefab;
    public Hero heroPrefab;

    public static List<Hero> heroes;

    public void CreateHero()
    {
        Instantiate(newExpButtonPrefab, transform);
        var hero = Instantiate(heroPrefab, transform);
        heroes.Add(hero);
    }
}
