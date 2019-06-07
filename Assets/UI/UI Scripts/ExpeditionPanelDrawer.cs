using UnityEngine;
using System.Linq;
using TMPro;

public class ExpeditionPanelDrawer : MonoBehaviour
{
    public Expedition expedition;

    public SituationPanelDrawer situationPanel;
    public LogPanelDrawer logPanel;

    public void ShowSituationCheck(Canvas canvas)
    {
        var cMan = GetComponent<CanvasManager>();
        cMan.ChangeActiveCanvas(GameManager.expeditions.Count > 0 ? canvas : cMan.defaultActiveCanvases.FirstOrDefault());
    }
}