using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas defaultActiveCanvas;
    [Disabled] public List<Canvas> canvases = new List<Canvas>();
    public List<Canvas> alwaysActiveCanvases = new List<Canvas>();

    /// <summary>
    /// automatically put direct children of this object (1 depth) into canvases list
    /// </summary>
    void OnValidate()
    {
        var compTransform = GetComponent<Transform>();
        var directChildren = GetComponentsInChildren<Transform>().Where(comp => comp.parent == compTransform);
        foreach (var child in directChildren)
        {
            var canvas = child.gameObject.GetComponent<Canvas>();
            if (canvas != null && !canvases.Contains(canvas))
            {
                canvases.Add(canvas);
            }
        }
    }

    void Start()
    {
        foreach (var canvas in canvases)
        {
            //disable unused canvases
            canvas.enabled = alwaysActiveCanvases.Contains(canvas) || canvas == defaultActiveCanvas;
        }
    }

    public void ChangeActiveCanvas(Canvas selectedCanvas)
    {
        foreach (var canvas in canvases)
        {
            if (!alwaysActiveCanvases.Contains(canvas) && canvas != selectedCanvas)
                canvas.enabled = false;
        }

        selectedCanvas.enabled = true;
    }
}