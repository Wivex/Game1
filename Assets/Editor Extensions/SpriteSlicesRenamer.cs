using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteSlicesRenamer : EditorWindow
{
    Texture2D baseTexture;
    TextureImporter baseTextureImporter;
    string baseTexturePath, searchText = string.Empty, replaceText = string.Empty;
    List<string> oldNamesPreview = new List<string>(), newNamesPreview = new List<string>();
    SpriteMetaData[] oldMetaData, newMetaData;
    bool shouldAddIndex = false, canRevert;
    Vector2 baseNamesScrollPos, newNamesScrollPos;

    bool CanRename => searchText != replaceText;

    [MenuItem("My Tools/Sprite Slices Renamer")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        var window = GetWindow<SpriteSlicesRenamer>();
        window.titleContent.text = "Sprite Slices Renamer";
    }

    void UpdateNewNamesPreview()
    {
        newNamesPreview.Clear();
        for (var i = 0; i < oldNamesPreview.Count; i++)
        {
            var tempName = oldNamesPreview[i];

            if (CanRename)
                tempName = searchText != string.Empty ? tempName.Replace(searchText, replaceText) : replaceText;

            if (shouldAddIndex)
                tempName += i;

            newNamesPreview.Add(tempName);
        }
    }

    void LoadSprites(bool loadSelection = false)
    {

        if (Selection.activeObject != null)
        {
            switch (Selection.activeObject)
            {
                case Sprite _:
                    baseTexture = (Selection.activeObject as Sprite).texture;
                    break;
                case Texture2D _:
                    baseTexture = (Texture2D) Selection.activeObject;
                    break;
            }

            if (baseTexture != null)
            {
                baseTexturePath = AssetDatabase.GetAssetPath(baseTexture);
                baseTextureImporter = AssetImporter.GetAtPath(baseTexturePath) as TextureImporter;

                if (loadSelection)
                {
                    var loadedSprites = Selection.objects.OfType<Sprite>().ToList();
                    oldNamesPreview = loadedSprites.Select(sprite => sprite.name).ToList();
                }
                else
                {
                    oldNamesPreview = baseTextureImporter.spritesheet.Select(data => data.name).ToList();
                }
            }
        }
    }

    void Rename()
    {
        oldMetaData = baseTextureImporter.spritesheet;
        // we use temp array cause spritesheet array isa struct and we cannot modify it's individual values, only whole struct
        var tempMetaData = baseTextureImporter.spritesheet;
        var index = 0;
        for (var i = 0; i < tempMetaData.Length; i++)
        {
            // rename only selected sprites
            if (oldNamesPreview.Contains(tempMetaData[i].name))
            {
                tempMetaData[i].name = searchText != string.Empty
                    ? tempMetaData[i].name.Replace(searchText, replaceText)
                    : replaceText;

                if (shouldAddIndex)
                    tempMetaData[i].name += index++;
            }
        }

        // write new metadata
        baseTextureImporter.spritesheet = tempMetaData;
        newMetaData = tempMetaData;

        // reimport renamed assets
        EditorUtility.SetDirty(baseTextureImporter);
        baseTextureImporter.SaveAndReimport();
        AssetDatabase.ImportAsset(baseTexturePath, ImportAssetOptions.ForceUpdate);
    }

    void Revert()
    {
        // write old metadata
        baseTextureImporter.spritesheet = oldMetaData;

        // reimport renamed assets
        EditorUtility.SetDirty(baseTextureImporter);
        baseTextureImporter.SaveAndReimport();
        AssetDatabase.ImportAsset(baseTexturePath, ImportAssetOptions.ForceUpdate);
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.BeginVertical();
            {
                if (GUILayout.Button("Load All Texture Sprites", GUILayout.MaxWidth(200.0f),
                    GUILayout.MaxHeight(37.5f)))
                {
                    LoadSprites();
                    UpdateNewNamesPreview();
                    // required to avoid errors if any data changes during GUI Draw
                    GUIUtility.ExitGUI();
                }

                if (GUILayout.Button("Load Selected Sprites", GUILayout.MaxWidth(200.0f),
                    GUILayout.MaxHeight(37.5f)))
                {
                    LoadSprites(true);
                    UpdateNewNamesPreview();
                    // required to avoid errors if any data changes during GUI Draw
                    GUIUtility.ExitGUI();
                }
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            {
                EditorGUI.BeginChangeCheck();
                {

                    EditorGUILayout.LabelField("Rename Parameters", EditorStyles.boldLabel);
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("Find string:", GUILayout.MaxWidth(75.0f));
                        searchText = EditorGUILayout.TextField(searchText);
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("Replace to:", GUILayout.MaxWidth(75.0f));
                        replaceText = EditorGUILayout.TextField(replaceText);
                    }
                    EditorGUILayout.EndHorizontal();
                    shouldAddIndex = EditorGUILayout.Toggle("Add index? (0 to N)", shouldAddIndex);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    UpdateNewNamesPreview();
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();

        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Base sprite names:", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("New sprite names:", EditorStyles.boldLabel);
            }
            EditorGUILayout.EndHorizontal();

            // Preview Box
            EditorGUILayout.BeginHorizontal();
            {
                baseNamesScrollPos = EditorGUILayout.BeginScrollView(baseNamesScrollPos, GUI.skin.box,
                    GUILayout.MaxWidth(position.width / 2));
                {
                    foreach (var text in oldNamesPreview)
                    {
                        GUILayout.Label(text);
                    }
                }
                EditorGUILayout.EndScrollView();

                newNamesScrollPos = EditorGUILayout.BeginScrollView(newNamesScrollPos, GUI.skin.box,
                    GUILayout.MaxWidth(position.width / 2));
                {
                    foreach (var text in newNamesPreview)
                    {
                        GUILayout.Label(text);
                    }
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        // Bottom line space
        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        {
            // take all possible space, except minimal required for buttons
            GUILayout.FlexibleSpace();
            GUI.enabled = CanRename;
            if (GUILayout.Button("Rename"))
            {
                Rename();
                canRevert = true;
                UpdateNewNamesPreview();
            }

            GUI.enabled = canRevert;
            if (GUILayout.Button("Revert"))
            {
                Revert();
                canRevert = false;
                UpdateNewNamesPreview();
            }

            GUI.enabled = true;
        }
        EditorGUILayout.EndHorizontal();
    }
}