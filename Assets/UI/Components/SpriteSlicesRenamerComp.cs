using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SubjectNerd.Utilities;
using TMPro;
using UnityEditor;
using UnityEngine.UI;


public class SpriteSlicesRenamerComp : MonoBehaviour
{
    public Texture2D baseTexture;
    public string newName;

    private string path;
    private TextureImporter textureImporter;

    void Start()
    {
        path = AssetDatabase.GetAssetPath(baseTexture);
        textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
        SpriteMetaData[] sliceMetaData = textureImporter.spritesheet;

        int index = 0;
        foreach (SpriteMetaData individualSliceData in sliceMetaData)
        {
            sliceMetaData[index].name = string.Format(newName + "_{0}", index);
            print(sliceMetaData[index].name);

            index++;
        }

        textureImporter.spritesheet = sliceMetaData;
        EditorUtility.SetDirty(textureImporter);
        textureImporter.SaveAndReimport();

        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }
}