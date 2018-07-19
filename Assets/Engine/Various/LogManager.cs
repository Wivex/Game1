using System;
using System.Collections.Generic;
using TMPro;

[Serializable]
public class LogManager
{
    const int maxEntries = 8;

    public TextMeshProUGUI logDrawer;
    public Queue<string> log;

    public LogManager(TextMeshProUGUI logDrawer)
    {
        log = new Queue<string>(maxEntries);
        this.logDrawer = logDrawer;
        logDrawer.text = string.Empty;
    }
    
    //NOTE: bad performance?
    void UpdateLog()
    {
        logDrawer.text = string.Empty;
        foreach (var logEntry in log) logDrawer.text += logEntry;
    }

    public void AddLogEntry(string text)
    {
        if (log.Count >= maxEntries)
            log.Dequeue();
        log.Enqueue($"{text}\n");
        UpdateLog();
    }
}