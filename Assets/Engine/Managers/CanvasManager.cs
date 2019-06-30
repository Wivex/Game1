using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Disabled]
    public List<Canvas> canvases = new List<Canvas>();

    public Canvas defaultCanvas;
    public List<Canvas> staticCanvases = new List<Canvas>();

    /// <summary>
    /// automatically put direct children of this object (1 depth) into canvases list
    /// </summary>
    void OnValidate()
    {
        var compTransform = GetComponent<Transform>();
        var directChildren = GetComponentsInChildren<Transform>()
            .Where(comp => comp.parent == compTransform);
        foreach (var child in directChildren)
        {
            var canvas = child.gameObject.GetComponent<Canvas>();
            if (canvas != null && !canvases.Contains(canvas)) canvases.Add(canvas);
        }
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
}