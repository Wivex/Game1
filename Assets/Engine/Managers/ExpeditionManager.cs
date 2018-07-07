using System.Collections.Generic;
using UnityEngine;

public class ExpeditionManager : MonoBehaviour
{
    public Expedition expeditionPrefab;
    public RectTransform contentPanel;

    public List<Expedition> expeditions;

    //, Location destination
    public void NewExpedition(Hero hero)
    {
        Expedition expedition = Instantiate(expeditionPrefab, contentPanel);
        //expedition.hero = 
        //expedition.transform.SetParent(expeditionsPanel.transform, false);
        
    }
}
