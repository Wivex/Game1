using UnityEngine;
using TMPro;

public class ExpeditionPanelManager : MonoBehaviour
{
    public Expedition expedition;
    public HeroInfoPanelManager heroInfoPanel;
    public EnemyPanelManager enemyPanel;
    public HeroPanelManager heroPanel;
    public TextMeshProUGUI logText;
    LogManager log;
    CanvasManager canvasManager;

    public void UpdateLog(string logEntry)
    {
        if (log == null)
            log = new LogManager(logText);
        log.AddLogEntry(logEntry);
    }
}