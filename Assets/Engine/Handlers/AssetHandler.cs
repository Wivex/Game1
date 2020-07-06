using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public static class AssetHandler
{
    /// <summary>
    /// Loads asset of selected type in the same directory and same name, as selected asset.
    /// </summary>
    internal static T LoadNearbyAssetWithSameName<T>(Object asset) where T : Object
    {
        var assetPath = AssetDatabase.GetAssetPath(asset);
        var startIndex = "Assets/Resources/".Length;
        var endIndex = assetPath.IndexOf(".asset");
        var resourcePath = assetPath.Substring(startIndex, endIndex - startIndex);
        return Resources.Load<T>(resourcePath);
    }

    /// <summary>
    /// Loads assets of selected type which are children of specified asset
    /// </summary>
    internal static List<T> LoadChildAssets<T>(Object asset) where T : Object
    {
        return AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(asset))
            .OfType<T>().ToList();
    }
}