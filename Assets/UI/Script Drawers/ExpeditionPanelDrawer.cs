using UnityEngine;
using System.Linq;
using TMPro;

public class ExpeditionPanelDrawer : MonoBehaviour
{
    public GameObject expPreviewPanelPrefab;
    public Transform previewContentPanel;
    public Expedition selectedExp;
    public Canvas overviewCanvas, detailsCanvas, noExpCanvas;
    public DetailsPanelDrawer detailsPanelDrawer;
    public LogPanelDrawer logPanelDrawer;

    CanvasManager cMan;

    // initializations
    void Awake()
    {
        cMan = GetComponent<CanvasManager>();

        // clean up preview panel from prefab templates
        Destroy(previewContentPanel.transform.GetChild(0).gameObject);
    }

    public void TryShowOverviewPanel()
    {
        cMan.ChangeActiveCanvas(GameManager.instance.expeditions.Count > 0 ? overviewCanvas : noExpCanvas);
    }

    public void ShowSelectedExpDetailsPanel(Expedition exp)
    {
        selectedExp = exp;
        cMan.ChangeActiveCanvas(detailsCanvas);
    }

    /// <summary>
    /// generate new preview panel from prefab and make it child of content panel
    /// </summary>
    public void NewPreviewPanel(Expedition exp)
    {
        var panel = Instantiate(expPreviewPanelPrefab, previewContentPanel);
        exp.expPreviewPanel = panel.GetComponent<ExpPreviewPanelDrawer>();
        exp.expPreviewPanel.Init(exp);
    }
}