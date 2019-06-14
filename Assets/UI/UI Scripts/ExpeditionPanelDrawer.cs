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
        foreach (Transform child in previewContentPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void TryShowOverviewPanel()
    {
        cMan.ChangeActiveCanvas(GameManager.expeditions.Count > 0 ? overviewCanvas : noExpCanvas);
    }

    public void ShowSelectedExpDetailsPanel(Expedition exp)
    {
        selectedExp = exp;
        cMan.ChangeActiveCanvas(detailsCanvas);
    }

    public void AddExpedition(Expedition exp)
    {
        // generate new panel from prefab and make it child of content panel
        var panel = Instantiate(expPreviewPanelPrefab, previewContentPanel);
        panel.GetComponent<ExpPreviewPanelDrawer>().Init(exp);
    }
}