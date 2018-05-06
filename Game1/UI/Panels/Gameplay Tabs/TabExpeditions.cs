using System;
using Game1.Concepts;
using Game1.Objects.Units;
using Game1.UI.GeonUI_Overrides;
using GeonBit.UI.DataTypes;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public class TabExpeditions : PanelTabs_ButtonPanel
    {
        public static TabExpeditions Reference { get; set; }
        public PanelEmpty ParentPanel { get; set; }
        public PanelEmpty NoExpeditionsPanel { get; set; }
        public Vector2 ExpeditionPanelSize { get; set; }

        public TabExpeditions(PanelEmpty parentPanel) : base(parentPanel)
        {
            Reference = this;
            ParentPanel = parentPanel;
            // x+2 is an offset for bad drawing of VerticalScrollbar
            ExpeditionPanelSize = new Vector2(TabSelectionPanel.SizeInternal.X + 2, (int)TabSelectionPanel.SizeInternal.Y / 3);

            // add special panel if no expeditions present
            NoExpeditionsPanel = new PanelBrownThick(parentPanel.SizeInternal)
            {
                Visible = false,
                Identifier = "NoExpeditionsPanel"
            };
            AddChild(NoExpeditionsPanel);
            NoExpeditionsPanel.AddChild(new Header("NO EXPEDITONS", Anchor.Center));
        }

        public void AddExpeditionTab(Expedition expedition)
        {
            var expeditionPanel = new ButtonPanel(ExpeditionPanelSize);
            InitExpeditionPanel(expeditionPanel, expedition);
            // Vector2(6, 0) is an offset for bad overlapping of VerticalScrollbar
            var detailsPanel = new PanelBlackThin(TabAreaPanel.SizeInternal - new Vector2(6, 0), Anchor.CenterRight);
            InitDetailsPanel(detailsPanel, expedition);
            AddTab(expeditionPanel, detailsPanel);
        }

        public override void Update(ref Entity targetEntity, ref Entity dragTargetEntity, ref bool wasEventHandled, Point scrollVal)
        {
            if (Globals.ExpeditionsDict.Count == 0)
            {
                NoExpeditionsPanel.Visible = true;
            }
            else
            {
                if (NoExpeditionsPanel.Visible) NoExpeditionsPanel.Visible = false;

                foreach (var expedition in Globals.ExpeditionsDict)
                {
                    expedition.Value.Update();
                }
            }

            base.Update(ref targetEntity, ref dragTargetEntity, ref wasEventHandled, scrollVal);
        }

        //TODO: figure out how to get rid of PropagateEventsTo(parentPanel) use every time
        #region UI
        public static void InitExpeditionPanel(PanelEmpty parentPanel, Expedition expedition)
        {
            // hero icon
            var heroImagePanel =
                new PanelBrownThick(
                    new Vector2((int) (parentPanel.SizeInternal.Y * 0.4f), (int) (parentPanel.SizeInternal.Y * 0.4f)),
                    Anchor.TopLeft);
            parentPanel.AddChild(heroImagePanel);
            heroImagePanel.PropagateEventsTo(parentPanel);

            var heroImage = new Image(expedition.Hero.Texture, heroImagePanel.SizeInternal);
            heroImagePanel.AddChild(heroImage);
            heroImage.PropagateEventsTo(parentPanel);
            heroImage.WhileMouseHover = e =>
            {
                heroImage.FillColor = Color.Aqua;
            };
            heroImage.OnMouseLeave = e =>
            {
                heroImage.FillColor = Color.White;
            };

            // event icon
            var eventImagePanel = new PanelBrownThick(heroImagePanel.Size,
                Anchor.TopRight);
            parentPanel.AddChild(eventImagePanel);
            eventImagePanel.PropagateEventsTo(parentPanel);
            var eventImage = new Image(expedition.Event.Texture, eventImagePanel.SizeInternal);
            eventImagePanel.AddChild(eventImage);
            eventImage.PropagateEventsTo(parentPanel);

            var middlePanel = new PanelBlackThin(
                new Vector2(parentPanel.SizeInternal.X - (heroImagePanel.Size.X + eventImagePanel.Size.X - 1),
                    heroImagePanel.Size.Y), Anchor.TopCenter);
            parentPanel.AddChild(middlePanel);
            middlePanel.PropagateEventsTo(parentPanel);

            // TODO: event handle this
            if (expedition.Event.GetType() == typeof(EnemyEncounter))
            // init stat bars
            {
                var progressBarSize =
                    new Vector2((int)middlePanel.SizeInternal.X / 2 - 3, (int)middlePanel.Size.Y / 3 - 3);

                var healthBarUnit =
                    new ProgressBar(0, (uint)expedition.Hero.BaseStats["Health"], progressBarSize, Anchor.TopLeft)
                    {
                        Caption = { Text = expedition.Hero.BaseStats["Health"].ToString() },
                    };
                middlePanel.AddChild(healthBarUnit);
                healthBarUnit.PropagateEventsTo(parentPanel);
                healthBarUnit.BeforeUpdate += entity =>
                {
                    healthBarUnit.Value = expedition.Hero.Health;
                };

                var healthBarEnemy =
                    new ProgressBar(0, (uint)expedition.Hero.BaseStats["Health"], progressBarSize,
                        Anchor.TopRight)
                    {
                        Caption = { Text = expedition.Hero.BaseStats["Health"].ToString() }
                    };
                middlePanel.AddChild(healthBarEnemy);
                healthBarEnemy.PropagateEventsTo(parentPanel);
                healthBarEnemy.BeforeUpdate += entity =>
                {
                    healthBarEnemy.Value = expedition.Enemy.Health;
                };

                var energyBarUnit =
                    new ProgressBar(0, (uint)expedition.Hero.BaseStats["Health"], progressBarSize, Anchor.CenterLeft)
                    {
                        Caption = { Text = "50/100" },
                        ProgressFill = { FillColor = Color.DeepSkyBlue }
                    };
                middlePanel.AddChild(energyBarUnit);
                energyBarUnit.PropagateEventsTo(parentPanel);

                var energyBarEnemy =
                    new ProgressBar(0, (uint)expedition.Hero.BaseStats["Health"], progressBarSize,
                        Anchor.CenterRight)
                    {
                        Caption = { Text = "50/100" },
                        ProgressFill = { FillColor = Color.DeepSkyBlue }
                    };
                middlePanel.AddChild(energyBarEnemy);
                energyBarEnemy.PropagateEventsTo(parentPanel);

                var actionBarUnit =
                    new ProgressBar(0, (uint)expedition.Hero.BaseStats["Health"], progressBarSize, Anchor.BottomLeft)
                    {
                        Caption = { Text = "50/100" },
                        ProgressFill = { FillColor = Color.LightGoldenrodYellow }
                    };
                middlePanel.AddChild(actionBarUnit);
                actionBarUnit.PropagateEventsTo(parentPanel);

                var actionBarEnemy =
                    new ProgressBar(0, (uint)expedition.Hero.BaseStats["Health"], progressBarSize,
                        Anchor.BottomRight)
                    {
                        Caption = { Text = "50/100" },
                        ProgressFill = { FillColor = Color.LightYellow }
                    };
                middlePanel.AddChild(actionBarEnemy);
                actionBarEnemy.PropagateEventsTo(parentPanel);
            }
            else
            {
                var EventNameParagraph = new Paragraph(expedition.Event.Name, Anchor.Center);
                middlePanel.AddChild(EventNameParagraph);
                EventNameParagraph.PropagateEventsTo(parentPanel);
            }

            // consumables panel
            var maxConsumablesCount = 10;
            // (int) rounding to not miss some pixels due to approximation later
            var consumablePanelWidth = (int)parentPanel.SizeInternal.X / maxConsumablesCount;
            var consumablesPanel =
                new PanelEmpty(new Vector2(parentPanel.SizeInternal.X, consumablePanelWidth), Anchor.Auto);
            parentPanel.AddChild(consumablesPanel);
            consumablesPanel.PropagateEventsTo(parentPanel);
            for (var i = 0; i < maxConsumablesCount; i++)
            {
                var consumablePanel = new PanelBlackThin(new Vector2(consumablePanelWidth, consumablePanelWidth),
                    Anchor.AutoInline);
                consumablesPanel.AddChild(consumablePanel);
                consumablePanel.PropagateEventsTo(parentPanel);
                var consumableImage = new Icon((IconType)i, Anchor.Center);
                consumablePanel.AddChild(consumableImage);
                consumableImage.PropagateEventsTo(parentPanel);
                consumableImage.Size = consumablePanel.SizeInternal - new Vector2(4, 4);
            }

            var separator = new HorizontalLine();
            parentPanel.AddChild(separator);
            separator.PropagateEventsTo(parentPanel);

            // skills panel
            var maxSkillsCount = 10;
            var skillsPanelHeight = parentPanel.SizeInternal.Y - (heroImagePanel.Size.Y + consumablesPanel.Size.Y + separator.Size.Y);
            var skillsPerRow = (int)Math.Ceiling((float)(maxSkillsCount / 2));
            var skillPanelHeight = (int)(skillsPanelHeight / 2);
            // used to center the skills panels
            var skillsPanel = new PanelEmpty(new Vector2(skillPanelHeight * skillsPerRow, skillsPanelHeight), Anchor.BottomLeft, new Vector2(1,1));
            parentPanel.AddChild(skillsPanel);
            skillsPanel.PropagateEventsTo(parentPanel);
            for (var i = 0; i < maxSkillsCount; i++)
            {
                var skillPanel = new PanelBlackThin(new Vector2(skillPanelHeight, skillPanelHeight),
                    Anchor.AutoInline);
                skillsPanel.AddChild(skillPanel);
                skillPanel.PropagateEventsTo(parentPanel);
                var skillIcon = new Icon((IconType)i, Anchor.Center, background: true);
                skillIcon.Size = skillPanel.SizeInternal - new Vector2(6, 6);
                skillPanel.AddChild(skillIcon);
                skillIcon.PropagateEventsTo(parentPanel);
            }

            // log panel
            var logPanel = new PanelBrownThin(new Vector2(skillsPanelHeight, skillsPanelHeight - 1), Anchor.BottomRight, new Vector2(1, 1));
            parentPanel.AddChild(logPanel);
            logPanel.PropagateEventsTo(parentPanel);

            // money panel
            var moneyPanelHeight = (int)skillsPanelHeight / 2;
            var moneyPanel =
                new PanelBlackThin(
                    new Vector2(parentPanel.SizeInternal.X - (skillsPanel.Size.X + logPanel.Size.X), moneyPanelHeight),
                    Anchor.BottomRight, new Vector2(logPanel.Size.X+2, moneyPanelHeight + 1));
            parentPanel.AddChild(moneyPanel);
            moneyPanel.PropagateEventsTo(parentPanel);

            // exp bar panel
            var expPanel = new PanelBlackThin(new Vector2(moneyPanel.Size.X, moneyPanelHeight), Anchor.BottomRight,
                new Vector2(skillsPanelHeight+2, 1));
            parentPanel.AddChild(expPanel);
            expPanel.PropagateEventsTo(parentPanel);
        }

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