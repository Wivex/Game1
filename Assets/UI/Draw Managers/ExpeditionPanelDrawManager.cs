using UnityEngine;
using System.Linq;
using TMPro;

public class ExpeditionPanelDrawManager : MonoBehaviour
{
    public ExpPreviewPanelDrawer expPreviewPanelPrefab;
    public Transform previewContentPanel;
    public Canvas overviewCanvas, detailsCanvas, noExpCanvas;
    //public ExpDetailsPanelDrawManager expDetailsPanelDrawManager;
    public LogPanelDrawer logPanelDrawer;

    internal Expedition selectedExp;

    CanvasManager cMan;

    // initializations
    void Awake()
    {
        cMan = GetComponent<CanvasManager>();

        // remove prefab template from content panel
        previewContentPanel.DestroyAllChildren();
    }

    public void ShowOverviewPanel()
    {
        cMan.ChangeActiveCanvas(ExpeditionManager.expeditions.Any() ? overviewCanvas : noExpCanvas);
    }

    // can't pass class as parameter with button click from inspector
    internal void ShowDetailsPanel(Expedition exp)
    {
        selectedExp = exp;
        //expDetailsPanelDrawManager.InitHeroPanel(hero);
        cMan.ChangeActiveCanvas(detailsCanvas);
    }

    /// <summary>
    /// generate new preview panel from prefab and make it child of content panel
    /// </summary>
    internal void NewPreviewPanel(Expedition exp)
    {
        var panel = Instantiate(expPreviewPanelPrefab, previewContentPanel);
        panel.Init(exp);
    }
}