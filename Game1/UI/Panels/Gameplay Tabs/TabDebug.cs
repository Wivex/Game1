using Game1.Concepts;
using Game1.Objects.Units;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public static class TabDebug
    {
        public static bool EditorMode { get; set; } = true;

        public static void Init(PanelEmpty parentPanel)
        {
            var debugButtonSize = new Vector2(200, 50);
            parentPanel.AddChild(new Button("New Expedition", size: debugButtonSize)
            {
                OnClick = e =>
                {
                    var hero = new Hero("John", "Warrior");
                    new Expedition(hero, "Forest");
                }
            });

            var checkboxEditor = new CheckBox("Editor mode", size: debugButtonSize);
            parentPanel.AddChild(checkboxEditor);
            checkboxEditor.TextParagraph.SetAnchor(Anchor.Center);
            checkboxEditor.OnValueChange = e =>
            {
                EditorMode = !EditorMode;
            };
        }
    }
}