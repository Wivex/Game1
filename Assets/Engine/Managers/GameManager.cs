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
}