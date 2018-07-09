using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : Panel
{
    public ExpeditionPanel expeditionPanel;
    public Panel townPanel;
    public Panel debugPanel;

    private Panel currentPanel;

    protected void Start()
    {
        expeditionPanel.Hide();
        debugPanel.Hide();
        townPanel.Hide();
        ShowPanel(townPanel);
    }

    public void ShowPanel(Panel panel)
    {
        if (panel == currentPanel)
            return;
        if (currentPanel != null)
            currentPanel.Hide();
        panel.Show();
        currentPanel = panel;
    }

    public void ShowExpeition(Expedition expedition = null)
    {
        ShowPanel(expeditionPanel);
        if (expedition != null)
            expeditionPanel.ShowExpedition(expedition);
    }
}
