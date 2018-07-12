using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public List<Canvas> canvases = new List<Canvas>();

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
                canvases.Add(canvas);
        }
    }

    public void ChangeActiveCanvas(Canvas selectedCanvas)
    {
        foreach (var canvas in canvases)
        {
            canvas.enabled = false;
        }
        selectedCanvas.enabled = true;
    }
}
