using System;
using System.Collections.Generic;
using Game1.Concepts;
using Game1.Objects.Units;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public static class TabMilitary
    {
        /// <summary>
        /// Updates everything on military Tab
        /// </summary>
        public static void Update()
        {
            foreach (var expedition in Globals.Expeditions)
            {
                expedition.Value.Update();
            }
        }

        #region UI
        public static void Init(PanelEmpty parentPanel)
        {
            // left panel with hero selection
            var heroesPanel =
                new PanelEmpty(new Vector2((int) parentPanel.SizeInternal.X / 3, parentPanel.SizeInternal.Y),
                    Anchor.CenterLeft)
                {
                    PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll
                };

            // right panel with details
            var tabsPanel =
                new PanelEmpty(new Vector2(parentPanel.SizeInternal.X - heroesPanel.Size.X, heroesPanel.Size.Y),
                    Anchor.CenterRight);

            // TODO: add reference here
            var heroes = new List<Hero> {new Hero("John","Warrior"), new Hero("Igor", "Monk") };
            var enemies = new List<Hero> { new Hero("Bandit Warrior", "Warrior"), new Hero("Bandit Monk", "Monk") };

            var panelTabs = new PanelTabs_ButtonPanel(heroesPanel, tabsPanel);
            parentPanel.AddChild(panelTabs);
            // x+2 is an offset for bad drawing of VerticalScrollbar
            var heroPanelSize = new Vector2(heroesPanel.SizeInternal.X + 2, (int) parentPanel.Size.Y / 3);
            for (var i = 0; i < heroes.Count; i++)
            {
                var heroPanel = new ButtonPanel(heroPanelSize);
                InitHeroPanel(heroPanel, heroes[i], enemies[i]);
                // Vector2(6, 0) is an offset for bad overlapping of VerticalScrollbar
                var detailsPanel = new PanelBlackThin(tabsPanel.SizeInternal - new Vector2(6, 0), Anchor.CenterRight);
                panelTabs.AddTab(heroPanel, detailsPanel);
                InitDetailsPanel(detailsPanel, heroes[i], enemies[i]);
            }
        }

        public static void InitHeroPanel(PanelEmpty parentPanel, Hero hero, Hero enemy)
        {
            // hero icon
            // use panel for image to draw borders
            var heroImagePanel =
                new PanelBrownThick(
                    new Vector2((int) (parentPanel.SizeInternal.Y * 0.4f), (int) (parentPanel.SizeInternal.Y * 0.4f)),
                    Anchor.TopLeft);
            var heroImage = new Image(hero.Texture, heroImagePanel.SizeInternal);
            // inheritParentState = true, to pass click event from image to parent panel
            heroImagePanel.AddChild(heroImage, true);
            parentPanel.AddChild(heroImagePanel, true);

            // object icon
            var objectImagePanel = new PanelBrownThick(heroImagePanel.Size,
                Anchor.TopRight);
            var objectImage = new Image(enemy.Texture, objectImagePanel.SizeInternal);
            objectImagePanel.AddChild(objectImage, true);
            parentPanel.AddChild(objectImagePanel, true);

            // stat bars
            {
                var barsPanel = new PanelBlackThin(
                    new Vector2(parentPanel.SizeInternal.X - (heroImagePanel.Size.X + objectImagePanel.Size.X - 1),
                        heroImagePanel.Size.Y), Anchor.TopCenter);
                parentPanel.AddChild(barsPanel, true);

                var progressBarSize =
                    new Vector2((int) barsPanel.SizeInternal.X / 2 - 3, (int) barsPanel.Size.Y / 3 - 3);

                var healthBarUnit =
                    new ProgressBar(0, (uint) hero.Stats["Health"], progressBarSize, Anchor.TopLeft)
                    {
                        Caption = {Text = hero.Stats["Health"].ToString()}
                    };
                barsPanel.AddChild(healthBarUnit, true);

                var healthBarEnemy =
                    new ProgressBar(0, (uint) enemy.Stats["Health"], progressBarSize, Anchor.TopRight);
                healthBarEnemy.Caption.Text = enemy.Stats["Health"].ToString();
                barsPanel.AddChild(healthBarEnemy, true);

                var energyBarUnit =
                    new ProgressBar(0, (uint) hero.Stats["Health"], progressBarSize, Anchor.CenterLeft);
                energyBarUnit.Caption.Text = "50/100";
                energyBarUnit.ProgressFill.FillColor = Color.DeepSkyBlue;
                barsPanel.AddChild(energyBarUnit, true);

                var energyBarEnemy =
                    new ProgressBar(0, (uint) hero.Stats["Health"], progressBarSize, Anchor.CenterRight);
                energyBarEnemy.Caption.Text = "50/100";
                energyBarEnemy.ProgressFill.FillColor = Color.DeepSkyBlue;
                barsPanel.AddChild(energyBarEnemy, true);

                var actionBarUnit =
                    new ProgressBar(0, (uint) hero.Stats["Health"], progressBarSize, Anchor.BottomLeft);
                actionBarUnit.Caption.Text = "50/100";
                actionBarUnit.ProgressFill.FillColor = Color.LightGoldenrodYellow;
                barsPanel.AddChild(actionBarUnit, true);

                var actionBarEnemy =
                    new ProgressBar(0, (uint) hero.Stats["Health"], progressBarSize, Anchor.BottomRight);
                actionBarEnemy.Caption.Text = "50/100";
                actionBarEnemy.ProgressFill.FillColor = Color.LightYellow;
                barsPanel.AddChild(actionBarEnemy, true);
            }

            // consumables panel
            var maxConsumablesCount = 10;
            // (int) rounding to not miss some pixels due to approximation later
            var consumablePanelWidth = (int) parentPanel.SizeInternal.X / maxConsumablesCount;
            var consumablesPanel =
                new PanelEmpty(new Vector2(parentPanel.SizeInternal.X, consumablePanelWidth), Anchor.Auto);
            parentPanel.AddChild(consumablesPanel, true);
            for (var i = 0; i < maxConsumablesCount; i++)
            {
                var consumablePanel = new PanelBlackThin(new Vector2(consumablePanelWidth, consumablePanelWidth),
                    Anchor.AutoInline);
                consumablesPanel.AddChild(consumablePanel, true);
                var consumableImage = new Icon((IconType)i, Anchor.Center);
                consumableImage.Size = consumablePanel.SizeInternal - new Vector2(4, 4);
                consumablePanel.AddChild(consumableImage, true);
            }

            var separator = new HorizontalLine();
            parentPanel.AddChild(separator, true);

            // skills panel
            var maxSkillsCount = 10;
            var skillsPanelHeight = parentPanel.SizeInternal.Y - (heroImagePanel.Size.Y + consumablesPanel.Size.Y + separator.Size.Y);
            var skillsPerRow = (int) Math.Ceiling((float) (maxSkillsCount / 2));
            var skillPanelHeight = (int) (skillsPanelHeight / 2);
            // used to center the skills panels
            var skillsPanel = new PanelEmpty(new Vector2(skillPanelHeight * skillsPerRow, skillsPanelHeight),
                Anchor.BottomLeft);
            parentPanel.AddChild(skillsPanel, true);
            for (var i = 0; i < maxSkillsCount; i++)
            {
                var skillPanel = new PanelBlackThin(new Vector2(skillPanelHeight, skillPanelHeight),
                    Anchor.AutoInline);
                skillsPanel.AddChild(skillPanel, true);
                var skillIcon = new Icon((IconType) i, Anchor.Center, background: true);
                skillIcon.Size = skillPanel.SizeInternal - new Vector2(6, 6);
                skillPanel.AddChild(skillIcon, true);
            }

            // log panel
            var logPanel = new PanelBrownThin(new Vector2(skillsPanelHeight, skillsPanelHeight - 1), Anchor.BottomRight,
                Vector2.UnitY);
            parentPanel.AddChild(logPanel, true);

            // money panel
            var moneyPanelHeight = (int) skillsPanelHeight / 2;
            var moneyPanel =
                new PanelBlackThin(
                    new Vector2(parentPanel.SizeInternal.X - (skillsPanel.Size.X + logPanel.Size.X), moneyPanelHeight),
                    Anchor.BottomRight, new Vector2(logPanel.Size.X, moneyPanelHeight + 1));
            parentPanel.AddChild(moneyPanel, true);

            // exp bar panel
            var expPanel = new PanelBlackThin(new Vector2(moneyPanel.Size.X, moneyPanelHeight), Anchor.BottomRight,
                new Vector2(skillsPanelHeight, 1));
            parentPanel.AddChild(expPanel, true);
        }

        public static void InitDetailsPanel(PanelEmpty parentPanel, Hero hero, Hero enemy)
        {
            // hero details panel (left)
            var heroDetailsPanel =
                new PanelBrownThin(new Vector2((int) parentPanel.SizeInternal.X / 2, parentPanel.SizeInternal.Y),
                    Anchor.CenterLeft);
            parentPanel.AddChild(heroDetailsPanel);
            InitHeroDetailsPanel(heroDetailsPanel, hero);

            // object details panel (right)
            var objectDetailsPanel =
                new PanelBrownThin(heroDetailsPanel.Size, Anchor.CenterRight);
            parentPanel.AddChild(objectDetailsPanel);
            InitObjectDetailsPanel(objectDetailsPanel, hero);
        }

        public static void InitHeroDetailsPanel(PanelEmpty parentPanel, Hero hero)
        {
            // equipment panel
            var equipmentPanel =
                new PanelBlackThin(
                    new Vector2((int)parentPanel.SizeInternal.X * 3 / 5, (int)parentPanel.SizeInternal.Y / 3), Anchor.TopLeft);
            parentPanel.AddChild(equipmentPanel);
            InitEquipmentPanel(equipmentPanel, hero);

            // stats panel
            var heroStatsPanel =
                new PanelBlackThin(
                    new Vector2(parentPanel.SizeInternal.X - equipmentPanel.Size.X, equipmentPanel.Size.Y),
                    Anchor.TopRight);
            parentPanel.AddChild(heroStatsPanel);
            InitHeroStatsPanel(heroStatsPanel, hero);

            // hero info panel
            var heroInfoPanel =
                new PanelBlackThin(
                    new Vector2(parentPanel.SizeInternal.X, equipmentPanel.Size.Y));
            parentPanel.AddChild(heroInfoPanel);
            InitHeroInfoPanel(heroInfoPanel, hero);

            // inventory panel
            var inventoryPanel =
                new PanelBlackThin(
                    new Vector2(parentPanel.SizeInternal.X, heroStatsPanel.Size.Y),
                    Anchor.BottomCenter);
            parentPanel.AddChild(inventoryPanel);
            InitInventoryPanel(inventoryPanel, hero);
        }

        public static void InitHeroInfoPanel(PanelEmpty parentPanel, Hero hero)
        {
        }

        public static void InitEquipmentPanel(PanelEmpty parentPanel, Hero hero)
        {
            var equipmentPanelWidth = (int)parentPanel.SizeInternal.Y / 4;

            var topPanel = new PanelEmpty(new Vector2(equipmentPanelWidth*3, equipmentPanelWidth),
                Anchor.TopCenter);
            parentPanel.AddChild(topPanel);
            {
                var headPanel = new PanelBlackThin(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.AutoInline);
                topPanel.AddChild(headPanel);

                var amuletPanel = new PanelBlackThin(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.AutoInline);
                topPanel.AddChild(amuletPanel);

                var bodyPanel = new PanelBlackThin(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.AutoInline);
                topPanel.AddChild(bodyPanel);
            }

            var middlePanel = new PanelEmpty(new Vector2(parentPanel.SizeInternal.X, parentPanel.SizeInternal.Y - equipmentPanelWidth*2));
            parentPanel.AddChild(middlePanel);
            {
                var hand1Panel = new PanelBlackThin(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.TopLeft);
                middlePanel.AddChild(hand1Panel);

                var ring1Panel = new PanelBlackThin(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.BottomLeft);
                middlePanel.AddChild(ring1Panel);

                var heroImagePanel = new PanelEmpty(new Vector2(equipmentPanelWidth*2, equipmentPanelWidth*2));
                middlePanel.AddChild(heroImagePanel);
                var heroImage = new Image(hero.Texture, heroImagePanel.SizeInternal);
                heroImagePanel.AddChild(heroImage);

                var hand2Panel = new PanelBlackThin(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.TopRight);
                middlePanel.AddChild(hand2Panel);

                var ring2Panel = new PanelBlackThin(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.BottomRight);
                middlePanel.AddChild(ring2Panel);
            }

            var bottomPanel = new PanelEmpty(new Vector2(equipmentPanelWidth * 3, equipmentPanelWidth),
                Anchor.BottomCenter);
            parentPanel.AddChild(bottomPanel);
            {
                var handsPanel = new PanelBlackThin(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.AutoInline);
                bottomPanel.AddChild(handsPanel);

                var beltPanel = new PanelBlackThin(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.AutoInline);
                bottomPanel.AddChild(beltPanel);

                var feetPanel = new PanelBlackThin(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.AutoInline);
                bottomPanel.AddChild(feetPanel);
            }
        }

        public static void InitHeroStatsPanel(PanelEmpty parentPanel, Hero hero)
        {
        }

        public static void InitInventoryPanel(PanelEmpty parentPanel, Hero hero)
        {
        }

        public static void InitObjectDetailsPanel(PanelEmpty parentPanel, Hero hero)
        {
            // equipment panel
            var equipmentPanel =
                new PanelBlackThin(
                    new Vector2((int)parentPanel.SizeInternal.X * 3 / 5, (int)parentPanel.SizeInternal.Y / 3), Anchor.TopRight);
            parentPanel.AddChild(equipmentPanel);
            InitEquipmentPanel(equipmentPanel, hero);

            // stats panel
            var heroStatsPanel =
                new PanelBlackThin(
                    new Vector2(parentPanel.SizeInternal.X - equipmentPanel.Size.X, equipmentPanel.Size.Y),
                    Anchor.TopLeft);
            parentPanel.AddChild(heroStatsPanel);
            InitHeroStatsPanel(heroStatsPanel, hero);

            // hero info panel
            var heroInfoPanel =
                new PanelBlackThin(
                    new Vector2(parentPanel.SizeInternal.X, equipmentPanel.Size.Y));
            parentPanel.AddChild(heroInfoPanel);
            InitHeroInfoPanel(heroInfoPanel, hero);

            // inventory panel
            var inventoryPanel =
                new PanelBlackThin(
                    new Vector2(parentPanel.SizeInternal.X, heroStatsPanel.Size.Y),
                    Anchor.BottomCenter);
            parentPanel.AddChild(inventoryPanel);
            InitInventoryPanel(inventoryPanel, hero);
        }
        #endregion
    }
}