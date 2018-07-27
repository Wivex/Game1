using UnityEngine;
using TMPro;

public class ExpeditionPanelManager : MonoBehaviour
{
    public Expedition expedition;
    public EnemyPanelManager enemyPanel;
    public HeroPanelManager heroPanel;
    public TextMeshProUGUI logText;
    LogManager log;

    public void UpdateLog(string logEntry)
    {
        if (log == null)
            log = new LogManager(logText);
        log.AddLogEntry(logEntry);
    }
}