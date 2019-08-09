#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace MyBox.Internal
{
	[InitializeOnLoad]
	public class AutoSaveFeature
	{
        const string MenuItemName = "Tools/MyBox/AutoSave on Play";

        static bool IsEnabled
		{
			get => MyBoxSettings.AutoSaveEnabled;
            set => MyBoxSettings.AutoSaveEnabled = value;
        }

		[MenuItem(MenuItemName, priority = 100)]
        static void MenuItem()
		{
			IsEnabled = !IsEnabled;
		}

		[MenuItem(MenuItemName, true)]
        static bool MenuItemValidation()
		{
			Menu.SetChecked(MenuItemName, IsEnabled);
			return true;
		}


		static AutoSaveFeature()
		{
			EditorApplication.playModeStateChanged += AutoSaveWhenPlaymodeStarts;
		}

        static void AutoSaveWhenPlaymodeStarts(PlayModeStateChange obj)
		{
			if (!IsEnabled) return;

			if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
			{
				for (var i = 0; i < SceneManager.sceneCount; i++)
				{
					var scene = SceneManager.GetSceneAt(i);
					if (scene.isDirty) EditorSceneManager.SaveScene(scene);
				}

				AssetDatabase.SaveAssets();
			}
		}
	}
}
#endif