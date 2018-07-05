using System.Collections.Generic;
using UnityEngine;

public class ExpeditionManager : MonoBehaviour
{
    public Expedition expeditionPrefab;
    public RectTransform expeditionsPanel;
    public Location location;

    public static List<Expedition> expeditions;

    //, Location destination
    public void NewExpedition(Hero hero)
    {
        Expedition expedition = Instantiate(expeditionPrefab, expeditionsPanel);
        //expedition.hero = 
        //expedition.transform.SetParent(expeditionsPanel.transform, false);


    }
}
