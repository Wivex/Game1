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
    /// automatically put direct children of this object (1 depth) into canvases list
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

    public void ChangeActiveCanvas(Canvas selectedCanvas)
    {
        foreach (var canvas in canvases)
        {
            canvas.enabled = canvas == selectedCanvas || staticCanvases.Contains(canvas);
        }
    }

    public void HideSubCanvases()
    {
        DirectSubCanvases.ForEach(canvas => canvas.enabled = false);
    }
}