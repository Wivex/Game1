using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void CloseGame()
    {
    #if UNITY_EDITOR
        // If running game in Unity, stop it's execution
        UnityEditor.EditorApplication.isPlaying = false;
    #else
    // If running game in app, quit it
        Application.Quit();
    #endif
    }

    /// <summary>
    /// Loads asset of selected type in the same directory and same name, as selected asset.
    /// </summary>
    internal static T LoadNearbyAsset<T>(Object asset) where T : Object
    {
        // Can use AssetDatabase only in editor mode, otherwise compile error
    #if UNITY_EDITOR
        var assetPath = AssetDatabase.GetAssetPath(asset);
        //"Assets/Resources/Units/Heroes/Classes/Warrior/WarriorClass.asset"
        var startIndex = "Assets/Resources/".Length;
        var endIndex = assetPath.IndexOf(".asset");
        var resourcePath = assetPath.Substring(startIndex, endIndex - startIndex);
        //"Units/Heroes/Classes/Warrior/WarriorClass"
        return Resources.Load<T>(resourcePath);
    #else
    // should never be used in normal circumstances during actual game, only in editor
        return null;
    #endif
    }
}