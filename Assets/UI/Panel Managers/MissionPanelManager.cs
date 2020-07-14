using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class MissionPanelManager : MonoBehaviour
{
    public Canvas overviewCanvas, detailsCanvas, noExpCanvas;
    //public MissionDetailsPanelManager expDetailsPanelDrawManager;
    public LogPanelDrawer logPanelDrawer;

    internal Dictionary<Mission, MissionOverviewPanelDrawer> expPreviewPanels = new Dictionary<Mission, MissionOverviewPanelDrawer>();
    internal Mission selectedExp;

    CanvasManager cMan;

    // initializations
    void Awake()
    {
        cMan = GetComponent<CanvasManager>();

        // remove prefab template from content panel
        //previewContentPanel.DestroyAllChildren();
    }
    
    public void ShowOverviewPanel()
    {
        cMan.ChangeActiveCanvas(MissionsManager.missions.Any() ? overviewCanvas : noExpCanvas);
    }

    // can't pass class as parameter with button click from inspector
    internal void ShowDetailsPanel(Mission exp)
    {
        selectedExp = exp;
        //expDetailsPanelDrawManager.InitHeroPanel(hero);
        cMan.ChangeActiveCanvas(detailsCanvas);
    }

    /// <summary>
    /// Init preview panel and keep it updating in the background (not initialize again each time mis panel is opened)
    /// </summary>
    internal void NewPreviewPanel(Mission exp)
    {
        //var panel = Instantiate(missionOverviewPanelPrefab, previewContentPanel);
        //panel.Init(exp);
        //expPreviewPanels.Add(exp, panel);
    }
}