using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region MANAGER INITIALIZATION

    /// <summary>
    /// Can't access static variables and methods from inspector. So we use static instance to do that.
    /// </summary>
    public static GameManager i;

    //default initialization of Singleton instance
    void Awake()
    {
        //Check if instance already exists
        if (i == null)
            //if not, set instance to this
            i = this;
        //If instance already exists and it's not this:
        else if (i != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of it.

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
    }


    #endregion

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
        var assetPath = AssetDatabase.GetAssetPath(asset);
        //"Assets/Resources/Units/Heroes/Classes/Warrior/WarriorClass.asset"
        var startIndex = "Assets/Resources/".Length;
        var endIndex = assetPath.IndexOf(".asset");
        var resourcePath = assetPath.Substring(startIndex, endIndex - startIndex);
        //"Units/Heroes/Classes/Warrior/WarriorClass"
        return Resources.Load<T>(resourcePath);
    }
}