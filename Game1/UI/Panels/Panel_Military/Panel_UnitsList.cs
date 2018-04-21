using System.Collections.Generic;
using System.Linq;
using Game1.Objects;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI.Panels
{
    public static class Panel_UnitsList
    {
        public static void Init(Game1 game, Panel panel, List<Unit> units)
        {
            var panelSize = new Vector2(panel.Size.X - VerticalScrollbar.DefaultSize.X + 1, panel.Size.Y / 3);
            foreach (var unit in units)
            {
                var unitPanel = new Panel(panelSize, PanelSkin.Simple, Anchor.Auto);
                InitUnitPanel(game, unitPanel, unit);
                panel.AddChild(unitPanel);
            }
        }

        public static void InitUnitPanel(Game1 game, Panel panel, Unit unit)
        {
            // Unit icon
            var unitImagePanel = new Panel(new Vector2(panel.Size.Y * 0.4f, panel.Size.Y * 0.4f), PanelSkin.Simple,
                Anchor.TopLeft);
            var unitImage = new Image(game.Content.Load<Texture2D>("warrior"), Vector2.Zero);
            unitImagePanel.AddChild(unitImage);
            panel.AddChild(unitImagePanel);

            // Object icon
            var objectImagePanel = new Panel(unitImagePanel.Size, PanelSkin.Simple,
                Anchor.TopRight);
            var objectImage = new Image(game.Content.Load<Texture2D>("monk"), Vector2.Zero);
            objectImagePanel.AddChild(objectImage);
            panel.AddChild(objectImagePanel);

            // Stat bars
            var statPanel = new Panel(new Vector2(panel.Size.X - unitImagePanel.Size.X * 2, unitImagePanel.Size.Y),
                PanelSkin.Simple, Anchor.TopCenter)
            {
                Padding = new Vector2(3, 3)
            };

            var infoPanel = new Panel(panel.Size - new Vector2(0, unitImagePanel.Size.Y), PanelSkin.Simple,Anchor.Auto);
            
            var barSize = new Vector2(statPanel.Size.X / 2 - 5, statPanel.Size.Y / 3 - 7);

            var healthBarUnit = new ProgressBar(0, unit.Health, barSize, Anchor.TopLeft);
            healthBarUnit.Caption.Text = "50/100";
            statPanel.AddChild(healthBarUnit);

            var healthBarEnemy = new ProgressBar(0, unit.Health, barSize, Anchor.TopRight);
            healthBarEnemy.Caption.Text = "50/100";
            statPanel.AddChild(healthBarEnemy);

            var energyBarUnit = new ProgressBar(0, unit.Health, barSize, Anchor.CenterLeft);
            energyBarUnit.Caption.Text = "50/100";
            energyBarUnit.ProgressFill.FillColor = Color.DeepSkyBlue;
            statPanel.AddChild(energyBarUnit);

            var energyBarEnemy = new ProgressBar(0, unit.Health, barSize, Anchor.CenterRight);
            energyBarEnemy.Caption.Text = "50/100";
            energyBarEnemy.ProgressFill.FillColor = Color.DeepSkyBlue;
            statPanel.AddChild(energyBarEnemy);

            var actionBarUnit = new ProgressBar(0, unit.Health, barSize, Anchor.BottomLeft);
            actionBarUnit.Caption.Text = "50/100";
            actionBarUnit.ProgressFill.FillColor = Color.LightGoldenrodYellow;
            statPanel.AddChild(actionBarUnit);

            var actionBarEnemy = new ProgressBar(0, unit.Health, barSize, Anchor.BottomRight);
            actionBarEnemy.Caption.Text = "50/100";
            actionBarEnemy.ProgressFill.FillColor = Color.LightYellow;
            statPanel.AddChild(actionBarEnemy);

            ////var unitNamePanel = new Panel(barSize, PanelSkin.None, Anchor.TopLeft);
            //var unitName = new Paragraph(unit.Attack.ToString(), Anchor.Center);
            ////unitNamePanel.AddChild(unitName);
            //panel.AddChild(unitName);

            //var objectNamePanel = new Panel(barSize, PanelSkin.None, Anchor.TopRight);
            //var objectName = new Paragraph(unit.Defence.ToString(), Anchor.Center);
            //objectNamePanel.AddChild(objectName);
            //statPanel.AddChild(objectNamePanel);

            //panel.AddChild(infoPanel);

            //var iconSize = new Vector2(30, 30);
            //for (int i = 0; i < 9; i++)
            //{
            //    var skillIcon = new Icon((IconType)i, Anchor.AutoInline, background: true);
            //    skillIcon.Size = iconSize;
            //    panel.AddChild(skillIcon);
            //}
            //panel.AddChild(new HorizontalLine());





            panel.AddChild(statPanel);
        }
    }
}