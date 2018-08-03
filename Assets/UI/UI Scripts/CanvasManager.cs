using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Disabled]
    public List<Canvas> canvases = new List<Canvas>();

    public List<Canvas> defaultActiveCanvases;
    public List<Canvas> alwaysActiveCanvases = new List<Canvas>();

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
            canvas.enabled = alwaysActiveCanvases.Contains(canvas) ||
                             defaultActiveCanvases.Contains(canvas);
        }
    }

    public void ChangeActiveCanvasess(Canvas[] selectedCanvases)
    {
        foreach (var canvas in canvases)
        {
            if (selectedCanvases.Contains(canvas) || alwaysActiveCanvases.Contains(canvas))
                canvas.enabled = true;
            else
                canvas.enabled = false;
        }
    }

    public void ChangeActiveCanvases(params Canvas[] selectedCanvases)
    {
        foreach (var canvas in canvases)
        {
            if (selectedCanvases.Contains(canvas) || alwaysActiveCanvases.Contains(canvas))
                canvas.enabled = true;
            else
                canvas.enabled = false;
        }
    }

    public void AddCanvasesToDefaultActive(params Canvas[] selectedCanvases)
    {
        foreach (var canvas in selectedCanvases)
        {
            defaultActiveCanvases.Add(canvas);
            canvas.enabled = true;
        }
    }

    public void AddCanvasesToAlwaysActive(params Canvas[] selectedCanvases)
    {
        foreach (var canvas in selectedCanvases)
        {
            alwaysActiveCanvases.Add(canvas);
            canvas.enabled = true;
        }
    }
}