using UnityEngine;
using UnityEditor;

public static class Extensions
{
    /// <summary>
    /// Destroys all children objects (clean up prefab templates)
    /// </summary>
    /// <param name="transform">Transform component of parent object</param>
    /// <returns></returns>
    public static Transform DestroyAllChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }

        return transform;
    }
}