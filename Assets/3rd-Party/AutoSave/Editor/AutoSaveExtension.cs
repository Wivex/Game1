using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class AutoSaveExtension
{
    // Static constructor that gets called when unity fires up.
    static AutoSaveExtension()
    {
        EditorApplication.playModeStateChanged += AutoSaveWhenPlaymodeStarts;
    }

    static void AutoSaveWhenPlaymodeStarts(PlayModeStateChange state)
    {
        // If we're about to run the scene...
        if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
        {
            // Save the scene and the assets.
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
            AssetDatabase.SaveAssets();
        }
    }
}