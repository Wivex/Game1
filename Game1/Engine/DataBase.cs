using System;
using System.Collections.Generic;
using System.IO;
using Game1.Concepts;
using Microsoft.Xna.Framework.Graphics;
using XMLData;

namespace Game1.Engine
{
    /// <summary>
    /// Class that holds all XMLData and corresponding textures loaded
    /// </summary>
    public static class DataBase
    {
        public static Dictionary<string, Tuple<HeroData, Texture2D>> Heroes { get; set; } = new Dictionary<string, Tuple<HeroData, Texture2D>>();
        public static Dictionary<string, Tuple<EnemyData, Texture2D>> Enemies { get; set; } = new Dictionary<string, Tuple<EnemyData, Texture2D>>();
        public static Dictionary<string, Tuple<EquipmentData, Texture2D>> Equipment { get; set; } =
            new Dictionary<string, Tuple<EquipmentData, Texture2D>>();
        public static Dictionary<string, Tuple<ConsumableData, Texture2D>> Consumables { get; set; } =
            new Dictionary<string, Tuple<ConsumableData, Texture2D>>();
        public static Dictionary<string, Tuple<ItemData, Texture2D>> Loot { get; set; } =
            new Dictionary<string, Tuple<ItemData, Texture2D>>();
        public static Dictionary<string, Tuple<LocationData, Texture2D>> Locations { get; set; } = new Dictionary<string, Tuple<LocationData, Texture2D>>();
        public static Dictionary<string, Tuple<AbilityData, Texture2D>> Abilities { get; set; } = new Dictionary<string, Tuple<AbilityData, Texture2D>>();

        public static void Init()
        {
            LoadData(Heroes, "Heroes");
            LoadData(Enemies, "Enemies");
            LoadData(Equipment, @"Items\Equipment");
            LoadData(Consumables, @"Items\Consumables");
            LoadData(Loot, @"Items\Loot");
            LoadData(Locations, "Locations");
            LoadData(Abilities, "Abilities");
        }

        public static void LoadData<T>(Dictionary<string, Tuple<T, Texture2D>> dataDict, string subDirPath)
        {
            foreach (var file in GetAllFilesFrom(subDirPath))
            {
                var name = Path.GetFileNameWithoutExtension(file).Replace("_Data", "");
                var filePath = file.Replace("Content\\", "").Replace(".xnb", "");
                var XMLData = Globals.TryLoadData<T>(filePath);
                var texture = Globals.TryLoadTexture(filePath.Replace("_Data", ""));
                dataDict.Add(name, Tuple.Create(XMLData, texture));
            }
        }

        public static string[] GetAllFilesFrom(string dirPath)
        {
            return Directory.GetFiles($@"{Globals.Game.Content.RootDirectory}\{Globals.DataPathBase}\{dirPath}", "*_Data.xnb", SearchOption.AllDirectories);
        }
    }
}