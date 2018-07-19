using System;
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

    void Start()
    {
        logText.text = string.Empty;
    }

    void Update()
    {
        if (expedition.situation.newLogEntry)
        {
            logText.text += expedition.situation.Log;
        }
    }
}