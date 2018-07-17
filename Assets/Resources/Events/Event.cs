using UnityEngine;

public abstract class Event
{
    public string name;
    public RectTransform eventPanel;
    public string feedback;

    public abstract void Update();
}