using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExpeditionPanelManager : MonoBehaviour
{
    public Expedition expedition;
    public EnemyPanelManager enemyPanel;
    public HeroPanelManager heroPanel;
    public TextMeshProUGUI logText;
    CanvasManager canvasManager;

    public void UpdateLog()
    {
        switch (expedition.situation.type)
        {
            case SituationType.EnemyEncounter:
                logText.text = $"Fighting with {enemyPanel.enemy.enemyData.name}";
                break;
            case SituationType.Travelling:
                logText.text = $"Travelling through {expedition.location.name}";
                break;
        }

        //else
        //{
        //    if (expedition.situation.logIsUpdated)
        //    {
        //        logText.text += expedition.situation.Log;
        //    }
        //}
    }
}