using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;

[Serializable]
public struct SnLBookmark
{
    public Vector3 pos;
    public Quaternion rot;
    public float size;
    public int layersState;
    public GameObject selectedObj;

    public SnLBookmark(SceneView sceneView)
    {
        pos = sceneView.pivot;
        rot = sceneView.rotation;
        size = sceneView.size;
        layersState = Tools.visibleLayers;
        selectedObj = Selection.activeGameObject;
    }
}

internal static class SceneAndLayersBookmarks
{
    const int UndoSlot = 0;

    static string NamePref(int slot) => $"SnLBookmark {slot}";

    static void SaveBookmark(int slot)
    {
        var bookmark = new SnLBookmark(SceneView.lastActiveSceneView);
        var prefKey = NamePref(slot);
        var json = JsonUtility.ToJson(bookmark);
        EditorPrefs.SetString(prefKey, json);
        Debug.Log($"Scene view bookmarked in slot {slot}.");
    }

    static void MoveToBookmark(int slot)
    {
        // Save current Scene and Layers values for Undo action
        if (slot!= UndoSlot) SaveBookmark(UndoSlot);

        var key = NamePref(slot);
        var json = EditorPrefs.GetString(key);
        var bookmark = JsonUtility.FromJson<SnLBookmark>(json);
        var sceneView = SceneView.lastActiveSceneView;
        sceneView.pivot = bookmark.pos;
        sceneView.rotation = bookmark.rot;
        sceneView.size = bookmark.size;
        Tools.visibleLayers = bookmark.layersState;
        Selection.activeGameObject = bookmark.selectedObj;
        sceneView.Repaint();
    }
	
    /// <summary>
    /// Used for MenuItem validation
    /// </summary>
    static bool BookmarkExists(int slot)
    {
        return EditorPrefs.HasKey(NamePref(slot));
    }


    #region MENU ITEMS

    [MenuItem("My Tools/Scene Bookmarks/Bookmark Scene View 1 %1", false, 200)]
    static void BookmarkSceneView1()
    {
        SaveBookmark(1);
    }

    [MenuItem("My Tools/Scene Bookmarks/Bookmark Scene View 2 %2", false, 200)]
    static void BookmarkSceneView2()
    {
        SaveBookmark(2);
    }

    [MenuItem("My Tools/Scene Bookmarks/Bookmark Scene View 3 %3", false, 200)]
    static void BookmarkSceneView3()
    {
        SaveBookmark(3);
    }

    [MenuItem("My Tools/Scene Bookmarks/Bookmark Scene View 4 %4", false, 200)]
    static void BookmarkSceneView4()
    {
        SaveBookmark(4);
    }

    [MenuItem("My Tools/Scene Bookmarks/Bookmark Scene View 5 %5", false, 200)]
    static void BookmarkSceneView5()
    {
        SaveBookmark(5);
    }

    [MenuItem("My Tools/Scene Bookmarks/Bookmark Scene View 6 %6", false, 200)]
    static void BookmarkSceneView6()
    {
        SaveBookmark(6);
    }

    [MenuItem("My Tools/Scene Bookmarks/Bookmark Scene View 7 %7", false, 200)]
    static void BookmarkSceneView7()
    {
        SaveBookmark(7);
    }

    [MenuItem("My Tools/Scene Bookmarks/Bookmark Scene View 8 %8", false, 200)]
    static void BookmarkSceneView8()
    {
        SaveBookmark(8);
    }

    [MenuItem("My Tools/Scene Bookmarks/Bookmark Scene View 9 %9", false, 200)]
    static void BookmarkSceneView9()
    {
        SaveBookmark(9);
    }


    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 1 &1", false, 100)]
    static void MoveSceneViewToBookmark1()
    {
        MoveToBookmark(1);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 2 &2", false, 100)]
    static void MoveSceneViewToBookmark2()
    {
        MoveToBookmark(2);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 3 &3", false, 100)]
    static void MoveSceneViewToBookmark3()
    {
        MoveToBookmark(3);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 4 &4", false, 100)]
    static void MoveSceneViewToBookmark4()
    {
        MoveToBookmark(4);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 5 &5", false, 100)]
    static void MoveSceneViewToBookmark5()
    {
        MoveToBookmark(5);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 6 &6", false, 100)]
    static void MoveSceneViewToBookmark6()
    {
        MoveToBookmark(6);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 7 &7", false, 100)]
    static void MoveSceneViewToBookmark7()
    {
        MoveToBookmark(7);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 8 &8", false, 100)]
    static void MoveSceneViewToBookmark8()
    {
        MoveToBookmark(8);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 9 &9", false, 100)]
    static void MoveSceneViewToBookmark9()
    {
        MoveToBookmark(9);
    }

    [MenuItem("My Tools/Scene Bookmarks/Return To Previous Scene View %b")]
    static void MoveSceneViewToPreviousState()
    {
        MoveToBookmark(UndoSlot);
    }

    #endregion

    #region MENU ITEM VALIDATORS
    
    // Validations performed by methods with same MenuItem name parameter, but named as "Validate[MenuItem method name]"
    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 1 &1", true)]
    static bool ValidateMoveSceneViewToBookmark1()
    {
        return BookmarkExists(1);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 2 &2", true)]
    static bool ValidateMoveSceneViewToBookmark2()
    {
        return BookmarkExists(2);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 3 &3", true)]
    static bool ValidateMoveSceneViewToBookmark3()
    {
        return BookmarkExists(3);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 4 &4", true)]
    static bool ValidateMoveSceneViewToBookmark4()
    {
        return BookmarkExists(4);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 5 &5", true)]
    static bool ValidateMoveSceneViewToBookmark5()
    {
        return BookmarkExists(5);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 6 &6", true)]
    static bool ValidateMoveSceneViewToBookmark6()
    {
        return BookmarkExists(6);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 7 &7", true)]
    static bool ValidateMoveSceneViewToBookmark7()
    {
        return BookmarkExists(7);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 8 &8", true)]
    static bool ValidateMoveSceneViewToBookmark8()
    {
        return BookmarkExists(8);
    }

    [MenuItem("My Tools/Scene Bookmarks/Move Scene View To Bookmark 9 &9", true)]
    static bool ValidateMoveSceneViewToBookmark9()
    {
        return BookmarkExists(9);
    }

    [MenuItem("My Tools/Scene Bookmarks/Return To Previous Scene View &b", true)]
    static bool ValidateMoveSceneViewToPreviousState()
    {
        return BookmarkExists(UndoSlot);
    }

    #endregion
}