using System;
using System.Collections.Generic;
using Game1.Objects.Units;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI.Panels
{
    public static class Tab_Military
    {
        public static void Init(Game1 game, PanelEmpty parentPanel)
        {
            // left panel with hero selection
            var heroesPanel =
                new PanelEmpty(new Vector2((int) parentPanel.SizeInternal.X / 3, parentPanel.SizeInternal.Y), Anchor.CenterLeft)
                {
                    PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll
                };

            // right panel with details
            var tabsPanel =
                new PanelEmpty(new Vector2(parentPanel.SizeInternal.X - heroesPanel.Size.X, heroesPanel.Size.Y), Anchor.CenterRight);

            // TODO: add reference here
            var heroes = new List<Hero> {new Hero(), new Hero(), new Hero(), new Hero(), new Hero()};

            var panelTabs = new PanelTabs_ButtonPanel(heroesPanel, tabsPanel);
            parentPanel.AddChild(panelTabs);
            // x+2 is as offset for bad drawing of VerticalScrollbar
            var heroPanelSize = new Vector2(heroesPanel.SizeInternal.X + 2, (int)parentPanel.Size.Y / 3);
            foreach (var hero in heroes)
            {
                var heroPanel = new ButtonPanel(heroPanelSize);
                InitHeroPanel(game, heroPanel, hero);
                var detailsPanel = new PanelBlackThin(tabsPanel.SizeInternal - new Vector2(6, 0), Anchor.CenterRight);
                //InitDetailsPanel(game, detailsPanel, hero);
                // Vector2(6, 0) is as offset for bad overlapping of VerticalScrollbar
                panelTabs.AddTab(heroPanel, detailsPanel);
            }
        }

        public static void InitHeroPanel(Game1 game, PanelEmpty parentPanel, Hero hero)
        {
            // hero icon
            // use panel for image to draw borders
            var heroImagePanel =
                new PanelBrownThick(new Vector2((int)(parentPanel.SizeInternal.Y * 0.4f), (int)(parentPanel.SizeInternal.Y * 0.4f)), Anchor.TopLeft);
            var heroImage = new Image(game.Content.Load<Texture2D>("warrior"), heroImagePanel.SizeInternal);
            // inheritParentState = true, to pass click event from image to parent panel
            heroImagePanel.AddChild(heroImage, true);
            parentPanel.AddChild(heroImagePanel, true);

            // object icon
            var objectImagePanel = new PanelBrownThick(heroImagePanel.Size,
                Anchor.TopRight);
            var objectImage = new Image(game.Content.Load<Texture2D>("monk"), objectImagePanel.SizeInternal);
            objectImagePanel.AddChild(objectImage, true);
            parentPanel.AddChild(objectImagePanel, true);

            // stat bars
            {
                var barsPanel = new PanelBlackThin(
                    new Vector2(parentPanel.SizeInternal.X - (heroImagePanel.Size.X + objectImagePanel.Size.X -1),
                        heroImagePanel.Size.Y), Anchor.TopCenter);
                parentPanel.AddChild(barsPanel, true);

                var progressBarSize = new Vector2((int)barsPanel.SizeInternal.X / 2 - 3, (int)barsPanel.Size.Y / 3 - 3);

                var healthBarUnit =
                    new ProgressBar(0, (uint) hero.Stats["Health"].Value, progressBarSize, Anchor.TopLeft)
                    {
                        Caption = {Text = "50/100"}
                    };
                barsPanel.AddChild(healthBarUnit, true);

                var healthBarEnemy =
                    new ProgressBar(0, (uint) hero.Stats["Health"].Value, progressBarSize, Anchor.TopRight);
                healthBarEnemy.Caption.Text = "50/100";
                barsPanel.AddChild(healthBarEnemy, true);

                var energyBarUnit =
                    new ProgressBar(0, (uint) hero.Stats["Health"].Value, progressBarSize, Anchor.CenterLeft);
                energyBarUnit.Caption.Text = "50/100";
                energyBarUnit.ProgressFill.FillColor = Color.DeepSkyBlue;
                barsPanel.AddChild(energyBarUnit, true);

                var energyBarEnemy =
                    new ProgressBar(0, (uint) hero.Stats["Health"].Value, progressBarSize, Anchor.CenterRight);
                energyBarEnemy.Caption.Text = "50/100";
                energyBarEnemy.ProgressFill.FillColor = Color.DeepSkyBlue;
                barsPanel.AddChild(energyBarEnemy, true);

                var actionBarUnit =
                    new ProgressBar(0, (uint) hero.Stats["Health"].Value, progressBarSize, Anchor.BottomLeft);
                actionBarUnit.Caption.Text = "50/100";
                actionBarUnit.ProgressFill.FillColor = Color.LightGoldenrodYellow;
                barsPanel.AddChild(actionBarUnit, true);

                var actionBarEnemy =
                    new ProgressBar(0, (uint) hero.Stats["Health"].Value, progressBarSize, Anchor.BottomRight);
                actionBarEnemy.Caption.Text = "50/100";
                actionBarEnemy.ProgressFill.FillColor = Color.LightYellow;
                barsPanel.AddChild(actionBarEnemy, true);
            }

            // consumables panel
            var maxConsumablesCount = 10;
            // (int) rounding to not miss some pixels due to approximation later
            var consumablePanelWidth = (int) parentPanel.SizeInternal.X / maxConsumablesCount;
            var consumablesPanel = new PanelEmpty(new Vector2(parentPanel.SizeInternal.X, consumablePanelWidth), Anchor.Auto);
            parentPanel.AddChild(consumablesPanel,true);
            for (var i = 0; i < maxConsumablesCount; i++)
            {
                var consumablePanel = new PanelBlackThin(new Vector2(consumablePanelWidth, consumablePanelWidth), Anchor.AutoInline);
                consumablesPanel.AddChild(consumablePanel, true);
                //var skillIcon = new Icon((IconType)i, Anchor.Center, background: true);
                //// NOTE: offset of 12 correlate to the icon border size for 9 skills
                //skillIcon.Size = consumablePanel.Size - new Vector2(12, 12);
                //consumablePanel.AddChild(skillIcon);
            }

            var separator = new HorizontalLine();
            parentPanel.AddChild(separator,true);

            // skills panel
            var maxSkillsCount = 10;
            var skillsPanelHeight = parentPanel.SizeInternal.Y - (heroImagePanel.Size.Y+consumablesPanel.Size.Y + separator.Size.Y);
            var skillsPerRow = (int)Math.Ceiling((float) (maxSkillsCount / 2));
            var skillPanelHeight = (int)(skillsPanelHeight / 2);
            // used to center the skills panels
            var skillsPanel = new PanelEmpty(new Vector2(skillPanelHeight* skillsPerRow, skillsPanelHeight), Anchor.BottomLeft);
            parentPanel.AddChild(skillsPanel, true);
            for (var i = 0; i < maxSkillsCount; i++)
            {
                var skillPanel = new PanelBlackThin(new Vector2(skillPanelHeight, skillPanelHeight),
                    Anchor.AutoInline);
                skillsPanel.AddChild(skillPanel, true);
                var skillIcon = new Icon((IconType)i, Anchor.Center, background: true);
                skillIcon.Size = skillPanel.SizeInternal - new Vector2(6, 6);
                skillPanel.AddChild(skillIcon, true);
            }

            // log panel
            var logPanel = new PanelBrownThin(new Vector2(skillsPanelHeight, skillsPanelHeight-1), Anchor.BottomRight, Vector2.UnitY);
            parentPanel.AddChild(logPanel, true);

            // money panel
            var moneyPanelHeight = (int)skillsPanelHeight / 2;
            var moneyPanel = new PanelBlackThin(new Vector2(parentPanel.SizeInternal.X - (skillsPanel.Size.X + logPanel.Size.X), moneyPanelHeight), Anchor.BottomRight, new Vector2(logPanel.Size.X, moneyPanelHeight+1));
            parentPanel.AddChild(moneyPanel, true);

            // exp bar panel
            var expPanel = new PanelBlackThin(new Vector2(moneyPanel.Size.X, moneyPanelHeight), Anchor.BottomRight, new Vector2(skillsPanelHeight, 1));
            parentPanel.AddChild(expPanel, true);
        }

        public static void InitDetailsPanel(Game1 game, PanelEmpty parentPanel, Hero hero)
        {
        }
    }
}