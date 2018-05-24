using System;
using Game1.Concepts;
using Game1.Objects.Units;
using Game1.UI.GeonUI_Overrides;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public class TabExpeditions : ButtonPanelTabs
    {
        public PanelEmpty NoExpeditionsPanel { get; set; }
        public Vector2 ExpeditionPanelSize { get; set; }

        public TabExpeditions(Vector2 size) : base(size)
        {
            Globals.TabExpeditions = this;
            // x+2 is an offset for bad drawing of VerticalScrollbar
            ExpeditionPanelSize = new Vector2(SelectionPanel.SizeInternal.X + 2, (int)SelectionPanel.SizeInternal.Y / 3);

            // add special panel if no expeditions present
            NoExpeditionsPanel = new PanelBrownThick(SizeInternal);
            AddChild(NoExpeditionsPanel);
            NoExpeditionsPanel.AddChild(new Header("NO EXPEDITONS", Anchor.Center));
        }

        public void AddExpeditionTab(Expedition expedition)
        {
            var expeditionPanel = new PanelExpedition(ExpeditionPanelSize, expedition);
            // Vector2(6, 0) is an offset for bad overlapping of VerticalScrollbar
            var detailsPanel = new PanelBlackThin(AreaPanel.SizeInternal - new Vector2(6, 0), Anchor.CenterRight);
            InitDetailsPanel(detailsPanel, expedition);
            AddTab(expeditionPanel, detailsPanel);
        }

        /// <summary>
        /// Depends on Globals.GameSpeed
        /// </summary>
        public override void UpdateChildrenVisibility()
        {
            // special case if no expeditions
            switch (Globals.ExpeditionsDict.Count)
            {
                case 0:
                    NoExpeditionsPanel.Visible = true;
                    break;
                default:
                    if (NoExpeditionsPanel.Visible) NoExpeditionsPanel.Visible = false;
                    break;
            }

            //foreach (var tab in Tabs)
            //{
            //    tab.ButtonPanel.UpdateChildrenVisibility();
            //}
        }

        /// <summary>
        /// Depends on Globals.GameSpeed
        /// </summary>
        public void UpdateData()
        {
            foreach (var expedition in Globals.ExpeditionsDict)
            {
                expedition.Value.Update();
            }
        }

        #region UI
        public static void InitDetailsPanel(PanelEmpty parentPanel, Expedition expedition)
        {
            // hero details panel (left)
            var heroDetailsPanel =
                new PanelBrownThin(new Vector2((int) parentPanel.SizeInternal.X / 2, parentPanel.SizeInternal.Y),
                    Anchor.CenterLeft);
            parentPanel.AddChild(heroDetailsPanel);
            InitHeroDetailsPanel(heroDetailsPanel, expedition.Hero);

            // event details panel (right)
            var eventDetailsPanel =
                new PanelBrownThin(heroDetailsPanel.Size, Anchor.CenterRight);
            parentPanel.AddChild(eventDetailsPanel);
            InitObjectDetailsPanel(eventDetailsPanel, expedition.Hero);
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
                var heroImage = new ImageNew(hero.Texture, heroImagePanel.SizeInternal);
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