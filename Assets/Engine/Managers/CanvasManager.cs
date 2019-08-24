using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Disabled]
    public List<Canvas> canvases = new List<Canvas>();

    public Canvas defaultCanvas;
    public List<Canvas> staticCanvases = new List<Canvas>();

    public List<Canvas> DirectSubCanvases => 
        GetComponentsInChildren<Canvas>()
            .Where(canvas => canvas.transform.parent == transform).ToList();

    /// <summary>
    /// Automatically put direct children of this object (1 depth) into canvases list
    /// </summary>
    void OnValidate()
    {
        canvases.Clear();
        DirectSubCanvases.ForEach(canvas => canvases.Add(canvas));
    }

    void Start()
    {
        //set enabled status for canvases
        foreach (var canvas in canvases)
        {
            canvas.enabled = staticCanvases.Contains(canvas) || canvas == defaultCanvas;
        }
    }

    /// <summary>
    /// Should be called hierarchically for each nested Canvas Manager, to properly update Visibility
    /// </summary>
    public void ChangeActiveCanvas(Canvas selectedCanvas)
    {
        foreach (var canvas in canvases)
        {
            var visibility = canvas == selectedCanvas || staticCanvases.Contains(canvas);
            canvas.enabled = visibility;
            canvas.ChangeContentVisibility(visibility);
        }
    }

    public void HideSubCanvases()
    {
        foreach (var canvas in DirectSubCanvases)
        {
            canvas.enabled = false;
            canvas.ChangeContentVisibility(false);
        }
    }
}