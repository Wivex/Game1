using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Disabled]
    public List<Canvas> canvases = new List<Canvas>();

    public List<Canvas> defaultEnabledCanvases;
    public List<Canvas> alwaysEnabledCanvases = new List<Canvas>();

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
            canvas.enabled = alwaysEnabledCanvases.Contains(canvas) ||
                             defaultEnabledCanvases.Contains(canvas);
        }
    }

    /// <summary>
    /// special method to operate with unity OnClick mouse event (1 parameter only)
    /// </summary>
    /// <param name="selectedCanvas"></param>
    public void EnableCanvas(Canvas selectedCanvas)
    {
        foreach (var canvas in canvases)
        {
            if (canvas == selectedCanvas || alwaysEnabledCanvases.Contains(canvas))
                canvas.enabled = true;
            else
                canvas.enabled = false;
        }
    }

    public void ChangeActiveCanvases(params Canvas[] selectedCanvases)
    {
        foreach (var canvas in canvases)
        {
            if (selectedCanvases.Contains(canvas) || alwaysEnabledCanvases.Contains(canvas))
                canvas.enabled = true;
            else
                canvas.enabled = false;
        }
    }

    public void EnableCanvases(params Canvas[] selectedCanvases)
    {
        foreach (var canvas in selectedCanvases)
        {
            canvas.enabled = true;
        }
    }

    public void AddCanvasesToAlwaysActive(params Canvas[] selectedCanvases)
    {
        foreach (var canvas in selectedCanvases)
        {
            alwaysEnabledCanvases.Add(canvas);
            canvas.enabled = true;
        }
    }
}