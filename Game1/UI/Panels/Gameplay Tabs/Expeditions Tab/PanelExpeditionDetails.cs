using System;
using System.Collections.Generic;
using Game1.Concepts;
using Game1.Objects.Units;
using Game1.UI.GeonUI_Overrides;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public class PanelExpeditionDetails : PanelBrownExternal
    {
        public PanelExpeditionDetails(Vector2 size, Expedition expedition) : base(size, Anchor.CenterRight)
        {
            // hero details panel (left)
            var heroDetailsPanel =
                new PanelBrownInternal(new Vector2((int)SizeInternal.X / 2, SizeInternal.Y),
                    Anchor.CenterLeft);
            AddChild(heroDetailsPanel);
            InitHeroDetailsPanel(heroDetailsPanel, expedition.Hero);

            // event details panel (right)
            var eventDetailsPanel =
                new PanelBrownInternal(heroDetailsPanel.Size, Anchor.CenterRight);
            AddChild(eventDetailsPanel);
            InitObjectDetailsPanel(eventDetailsPanel, expedition.Hero);
        }

        public void InitHeroDetailsPanel(PanelEmpty parentPanel, Hero hero)
        {
            // equipment panel
            var equipmentPanel =
                new PanelBlack(
                    new Vector2((int)parentPanel.SizeInternal.X * 3 / 5, (int)parentPanel.SizeInternal.Y / 3), Anchor.TopLeft);
            parentPanel.AddChild(equipmentPanel);
            InitEquipmentPanel(equipmentPanel, hero);

            // stats panel
            var heroStatsPanel =
                new PanelBlack(
                    new Vector2(parentPanel.SizeInternal.X - equipmentPanel.Size.X, equipmentPanel.Size.Y),
                    Anchor.TopRight);
            parentPanel.AddChild(heroStatsPanel);
            InitHeroStatsPanel(heroStatsPanel, hero);

            // hero info panel
            var heroInfoPanel =
                new PanelBlack(
                    new Vector2(parentPanel.SizeInternal.X, equipmentPanel.Size.Y));
            parentPanel.AddChild(heroInfoPanel);
            InitHeroInfoPanel(heroInfoPanel, hero);

            // inventory panel
            var inventoryPanel =
                new PanelBlack(
                    new Vector2(parentPanel.SizeInternal.X, heroStatsPanel.Size.Y),
                    Anchor.BottomCenter);
            parentPanel.AddChild(inventoryPanel);
            InitInventoryPanel(inventoryPanel, hero);
        }

        public void InitHeroInfoPanel(PanelEmpty parentPanel, Hero hero)
        {
        }

        public void InitEquipmentPanel(PanelEmpty parentPanel, Hero hero)
        {
            var equipmentPanelWidth = (int)parentPanel.SizeInternal.Y / 4;

            var topPanel = new PanelEmpty(new Vector2(equipmentPanelWidth * 3, equipmentPanelWidth),
                Anchor.TopCenter);
            parentPanel.AddChild(topPanel);
            {
                var headPanel = new PanelBlack(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.AutoInline);
                topPanel.AddChild(headPanel);

                var amuletPanel = new PanelBlack(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.AutoInline);
                topPanel.AddChild(amuletPanel);

                var bodyPanel = new PanelBlack(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.AutoInline);
                topPanel.AddChild(bodyPanel);
            }

            var middlePanel = new PanelEmpty(new Vector2(parentPanel.SizeInternal.X, parentPanel.SizeInternal.Y - equipmentPanelWidth * 2));
            parentPanel.AddChild(middlePanel);
            {
                var hand1Panel = new PanelBlack(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.TopLeft);
                middlePanel.AddChild(hand1Panel);

                var ring1Panel = new PanelBlack(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.BottomLeft);
                middlePanel.AddChild(ring1Panel);

                var heroImagePanel = new PanelEmpty(new Vector2(equipmentPanelWidth * 2, equipmentPanelWidth * 2));
                middlePanel.AddChild(heroImagePanel);
                var heroImage = new ImageNew(hero.Texture, heroImagePanel.SizeInternal);
                heroImagePanel.AddChild(heroImage);

                var hand2Panel = new PanelBlack(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.TopRight);
                middlePanel.AddChild(hand2Panel);

                var ring2Panel = new PanelBlack(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.BottomRight);
                middlePanel.AddChild(ring2Panel);
            }

            var bottomPanel = new PanelEmpty(new Vector2(equipmentPanelWidth * 3, equipmentPanelWidth),
                Anchor.BottomCenter);
            parentPanel.AddChild(bottomPanel);
            {
                var handsPanel = new PanelBlack(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.AutoInline);
                bottomPanel.AddChild(handsPanel);

                var beltPanel = new PanelBlack(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.AutoInline);
                bottomPanel.AddChild(beltPanel);

                var feetPanel = new PanelBlack(new Vector2(equipmentPanelWidth, equipmentPanelWidth),
                    Anchor.AutoInline);
                bottomPanel.AddChild(feetPanel);
            }
        }

        public void InitHeroStatsPanel(PanelEmpty parentPanel, Hero hero)
        {
        }

        public void InitInventoryPanel(PanelEmpty parentPanel, Hero hero)
        {
        }

        public void InitObjectDetailsPanel(PanelEmpty parentPanel, Hero hero)
        {
            // equipment panel
            var equipmentPanel =
                new PanelBlack(
                    new Vector2((int)parentPanel.SizeInternal.X * 3 / 5, (int)parentPanel.SizeInternal.Y / 3), Anchor.TopRight);
            parentPanel.AddChild(equipmentPanel);
            InitEquipmentPanel(equipmentPanel, hero);

            // stats panel
            var heroStatsPanel =
                new PanelBlack(
                    new Vector2(parentPanel.SizeInternal.X - equipmentPanel.Size.X, equipmentPanel.Size.Y),
                    Anchor.TopLeft);
            parentPanel.AddChild(heroStatsPanel);
            InitHeroStatsPanel(heroStatsPanel, hero);

            // hero info panel
            var heroInfoPanel =
                new PanelBlack(
                    new Vector2(parentPanel.SizeInternal.X, equipmentPanel.Size.Y));
            parentPanel.AddChild(heroInfoPanel);
            InitHeroInfoPanel(heroInfoPanel, hero);

            // inventory panel
            var inventoryPanel =
                new PanelBlack(
                    new Vector2(parentPanel.SizeInternal.X, heroStatsPanel.Size.Y),
                    Anchor.BottomCenter);
            parentPanel.AddChild(inventoryPanel);
            InitInventoryPanel(inventoryPanel, hero);
        }
    }
}
