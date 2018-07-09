using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class Panel : MonoBehaviour
{
    private Canvas canvas;

    protected virtual void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public virtual void Show()
    {
        canvas.enabled = true;
    }
    public virtual void Hide()
    {
        canvas.enabled = false;
    }
}
