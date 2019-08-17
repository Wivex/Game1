using UnityEngine;
using System.Linq;
using TMPro;

public class ExpeditionPanelDrawManager : MonoBehaviour, ICanvasVisibility
{
    public ExpPreviewPanelDrawer expPreviewPanelPrefab;
    public Transform previewContentPanel;
    public Canvas overviewCanvas, detailsCanvas, noExpCanvas;
    //public ExpDetailsPanelDrawManager expDetailsPanelDrawManager;
    public LogPanelDrawer logPanelDrawer;

    internal Expedition selectedExp;

    CanvasManager cMan;

    public bool Visible { get; set; }

    // initializations
    void Awake()
    {
        cMan = GetComponent<CanvasManager>();

        // remove prefab template from content panel
        previewContentPanel.DestroyAllChildren();
    }
    
    public void ShowOverviewPanel()
    {
        cMan.ChangeActiveCanvas(ExpeditionsManager.i.expeditions.Any() ? overviewCanvas : noExpCanvas);
    }

    // can't pass class as parameter with button click from inspector
    internal void ShowDetailsPanel(Expedition exp)
    {
        selectedExp = exp;
        //expDetailsPanelDrawManager.InitHeroPanel(hero);
        cMan.ChangeActiveCanvas(detailsCanvas);
    }

    /// <summary>
    /// Init preview panel and keep it updating in the background (not initialize again each time exp panel is opened)
    /// </summary>
    internal void NewPreviewPanel(Expedition exp)
    {
        var panel = Instantiate(expPreviewPanelPrefab, previewContentPanel);
        panel.Init(exp);
    }
}