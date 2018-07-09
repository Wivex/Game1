using UnityEngine;

public class Equipment : MonoBehaviour
{
    public int cost;
    public new string name;
    public string description;
    [System.NonSerialized] public Effect[] effects;

    protected void Awake()
    {
        effects = GetComponents<Effect>();
    }
}
