using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownManager : MonoBehaviour
{
    public Button newExpButtonPrefab;
    public Hero heroPrefab;

    public static List<Hero> heroes;

    public void CreateHero()
    {
        Instantiate(newExpButtonPrefab, transform);
        heroes.Add(Instantiate(heroPrefab, transform));
        Instantiate(newExpButtonPrefab, transform);
        heroes.Add(Instantiate(heroPrefab, transform));
        Instantiate(newExpButtonPrefab, transform);
        heroes.Add(Instantiate(heroPrefab, transform));
        Instantiate(newExpButtonPrefab, transform);
        heroes.Add(Instantiate(heroPrefab, transform));
        Instantiate(newExpButtonPrefab, transform);
        heroes.Add(Instantiate(heroPrefab, transform));
        Instantiate(newExpButtonPrefab, transform);
        heroes.Add(Instantiate(heroPrefab, transform));
    }
}
