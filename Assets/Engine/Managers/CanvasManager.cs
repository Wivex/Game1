using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // disabled, but not hidden, to be able to see list contents in editor
    [Disabled]
    public List<Canvas> controlledCanvases = new List<Canvas>();
    public Canvas defaultActiveCanvas;
    public List<Canvas> alwaysActiveCanvases = new List<Canvas>();

    void OnValidate()
    {
        controlledCanvases = gameObject.DirectSubCanvases().ToList();
    }

    void Start()
    {
        ResetCanvases();
    }

    internal void ResetCanvases()
    {
        ChangeActiveCanvas(defaultActiveCanvas);
    }

    /// <summary>
    /// Should be called hierarchically for each nested Canvas Manager, to properly update Visibility
    /// </summary>
    public void ChangeActiveCanvas(Canvas selectedCanvas)
    {
        foreach (var canvas in controlledCanvases)
        {
            var visible = canvas == selectedCanvas || alwaysActiveCanvases.Contains(canvas);
            canvas.enabled = visible;
            canvas.ChangeContentVisibility(visible);
        }
    }
}