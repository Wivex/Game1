using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogPanelDrawer : Drawer
{
    const int maxEntries = 20;

    public TextMeshProUGUI logTextObject;
    public Queue<string> log;

    void Start()
    {
        log = new Queue<string>(maxEntries);
        logTextObject.text = string.Empty;
    }

    //NOTE: bad performance?
    void UpdateLog()
    {
        logTextObject.text = string.Empty;
        foreach (var logEntry in log) logTextObject.text += logEntry;
    }

    public void AddLogEntry(string text)
    {
        if (log.Count >= maxEntries)
            log.Dequeue();
        log.Enqueue($"{text}\n");
        UpdateLog();
    }
}