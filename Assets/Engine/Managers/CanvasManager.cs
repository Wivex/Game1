using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // disabled, but not hidden, to be able to see list contents in editor
    [Disabled]
    public List<Canvas> controlledCanvases = new List<Canvas>();
    public Canvas defaultCanvas;
    public List<Canvas> staticCanvases = new List<Canvas>();

    void OnValidate()
    {
        controlledCanvases = gameObject.DirectSubCanvases();
    }

    void Start()
    {
        //set enabled status for canvases
        foreach (var canvas in controlledCanvases)
        {
            canvas.enabled = staticCanvases.Contains(canvas) || canvas == defaultCanvas;
        }
    }

    /// <summary>
    /// Should be called hierarchically for each nested Canvas Manager, to properly update Visibility
    /// </summary>
    public void ChangeActiveCanvas(Canvas selectedCanvas)
    {
        foreach (var canvas in controlledCanvases)
        {
            var visible = canvas == selectedCanvas || staticCanvases.Contains(canvas);
            canvas.enabled = visible;
            //canvas.ChangeContentVisibility(visible);
            HideSubCanvases(canvas);
        }
    }

    public void HideSubCanvases(Canvas canvas)
    {
        foreach (var canv in GetComponentsInChildren<Canvas>())
        {
            canv.enabled = false;
            canv.ChangeContentVisibility(false);
        }
    }
}