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
    List<Sprite> loadedSprites = new List<Sprite>();
    List<string> oldNames = new List<string>(), newNames = new List<string>();
    bool shouldAddIndex = false, canRevert;
    Vector2 baseNamesScrollPos, newNamesScrollPos;

    bool CanRename => searchText != replaceText;

    [MenuItem("Tools/Sprite Slices Renamer")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        var window = (SpriteSlicesRenamer) GetWindow(typeof(SpriteSlicesRenamer));
        window.titleContent.text = "Sprite Slices Renamer";
        window.Show();
    }

    void UpdateNamePreviews()
    {
        newNames.Clear();
        foreach (var name in oldNames)
        {
            newNames.Add(CanRename ? name.Replace(searchText, replaceText) : name);
        }
    }

    void LoadSprites(bool loadSelection = false)
    {
        if (loadSelection)
        {
            loadedSprites = Selection.objects.OfType<Sprite>().ToList();
            oldNames = loadedSprites.Select(sprite => sprite.name).ToList();
        }
        else
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
            }

            baseTexturePath = AssetDatabase.GetAssetPath(baseTexture);
            baseTextureImporter = AssetImporter.GetAtPath(baseTexturePath) as TextureImporter;
            oldNames = baseTextureImporter.spritesheet.Select(meta => meta.name).ToList();
        }
    }

    void Rename()
    {
        var tempMetaData = baseTextureImporter.spritesheet;
        for (var i = 0; i < tempMetaData.Length; i++)
        {
            tempMetaData[i].name.Replace(searchText, replaceText);
            if (shouldAddIndex)
            {
                tempMetaData[i].name += i;
            }
        }

        // write new metadata
        baseTextureImporter.spritesheet = tempMetaData;

        // reimport renamed assets
        EditorUtility.SetDirty(baseTextureImporter);
        baseTextureImporter.SaveAndReimport();
        AssetDatabase.ImportAsset(baseTexturePath, ImportAssetOptions.ForceUpdate);
    }

    void Revert()
    {
        var tempMetaData = baseTextureImporter.spritesheet;
        for (var i = 0; i < tempMetaData.Length; i++)
        {
            tempMetaData[i].name = oldNames[i];
        }

        // write new metadata
        baseTextureImporter.spritesheet = tempMetaData;

        // reimport renamed assets
        EditorUtility.SetDirty(baseTextureImporter);
        baseTextureImporter.SaveAndReimport();
        AssetDatabase.ImportAsset(baseTexturePath, ImportAssetOptions.ForceUpdate);
    }

    void OnGUI()
    {
        // required to avoid errors if any data changes during GUI Draw
        // try
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical();
                {
                    if (GUILayout.RepeatButton("Load All Texture Sprites", GUILayout.MaxWidth(200.0f),
                        GUILayout.MaxHeight(37.5f)))
                    {
                        LoadSprites();
                        UpdateNamePreviews();
                    }

                    if (GUILayout.RepeatButton("Load Selected Sprites", GUILayout.MaxWidth(200.0f),
                        GUILayout.MaxHeight(37.5f)))
                    {
                        LoadSprites(true);
                        UpdateNamePreviews();
                    }
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
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
                        foreach (var text in oldNames)
                        {
                            GUILayout.Label(text);
                        }
                    }
                    EditorGUILayout.EndScrollView();

                    newNamesScrollPos = EditorGUILayout.BeginScrollView(newNamesScrollPos, GUI.skin.box,
                        GUILayout.MaxWidth(position.width / 2));
                    {
                        foreach (var text in newNames)
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
                }

                GUI.enabled = canRevert;
                if (GUILayout.Button("Revert"))
                {
                    Revert();
                    canRevert = false;
                }

                GUI.enabled = true;
            }
            EditorGUILayout.EndHorizontal();

            // detects changes in text fields and toggles (can't detect button presses)
            if (GUI.changed) UpdateNamePreviews();
        }
        // required to avoid errors if any data changes during GUI Draw
        // catch
        // {
        //     GUIUtility.ExitGUI();
        // }
    }
}