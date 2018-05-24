using System;
using System.Collections.Generic;
using Game1.Concepts;
using Game1.UI.GeonUI_Overrides;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI.Panels
{
    public class PanelExpedition : ButtonPanel
    {
        public Expedition Expedition { get; set; }
        public PanelEmpty NoEnemyPanel { get; set; }
        public PanelBrownThick HeroImagePanel { get; set; }
        public PanelBrownThick EventImagePanel { get; set; }
        public List<PanelEmpty> SkillPanels { get; set; } = new List<PanelEmpty>(10);
        public List<PanelEmpty> ConsumablePanels { get; set; } = new List<PanelEmpty>(10);

        public PanelExpedition(Vector2 size, Expedition expedition) : base(size)
        {
            Expedition = expedition;
            Expedition.ExpeditionPanel = this;

            // hero icon
            HeroImagePanel =
                new PanelBrownThick(
                    new Vector2((int)(SizeInternal.Y * 0.4f), (int)(SizeInternal.Y * 0.4f)),
                    Anchor.TopLeft);
            AddChild(HeroImagePanel);
            var heroImage = new ImageNew(expedition.Hero.Texture, HeroImagePanel.SizeInternal);
            HeroImagePanel.AddChild(heroImage);

            // event icon
            EventImagePanel = new PanelBrownThick(HeroImagePanel.Size,
                Anchor.TopRight);
            AddChild(EventImagePanel);
            var eventImage = new ImageNew(expedition.Event.Texture, EventImagePanel.SizeInternal);
            EventImagePanel.AddChild(eventImage);
            eventImage.BeforeDraw += e =>
            {
                eventImage.Texture = expedition.Event.Texture;
            };

            var middlePanel = new PanelBlackThin(
                new Vector2(SizeInternal.X - (HeroImagePanel.Size.X + EventImagePanel.Size.X - 1),
                    HeroImagePanel.Size.Y), Anchor.TopCenter);
            AddChild(middlePanel);

            #region StatBars
            var progressBarSize =
                new Vector2((int)middlePanel.SizeInternal.X / 2 - 3, (int)middlePanel.Size.Y / 3 - 3);

            //var healthBarUnit =
            //    new ProgressBarNew(0, expedition.Hero.XMLData.Stats[Stat.Health], progressBarSize, Anchor.TopLeft);
            //middlePanel.AddChild(healthBarUnit);
            //healthBarUnit.BeforeDraw += e =>
            //{
            //    if (expedition.Enemy != null)
            //    {
            //        healthBarUnit.Value = expedition.Hero.Health;
            //        healthBarUnit.Caption.Text = $"{expedition.Hero.Health}/{expedition.Hero.XMLData.Stats["Health"]}";
            //    }
            //};

            var healthBarEnemy =
                new ProgressBarNew(0, 100, progressBarSize,
                    Anchor.TopRight);
            middlePanel.AddChild(healthBarEnemy);
            healthBarEnemy.BeforeDraw += e =>
            {
                if (expedition.Enemy != null)
                {
                    healthBarEnemy.Max = (uint)expedition.Enemy.XMLData.Stats[Stat.Health];
                    healthBarEnemy.Value = expedition.Enemy.Health;
                    healthBarEnemy.Caption.Text = $"{expedition.Enemy.Health}/{expedition.Enemy.XMLData.Stats["Health"]}";
                }
            };

            //var energyBarUnit =
            //    new ProgressBarNew(0, 100, progressBarSize, Anchor.CenterLeft)
            //    {
            //        ProgressFill = { FillColor = Color.DeepSkyBlue }
            //    };
            //middlePanel.AddChild(energyBarUnit);
            //energyBarUnit.BeforeDraw += e =>
            //{
            //    if (expedition.Enemy != null)
            //    {
            //        energyBarUnit.Value = expedition.Enemy.ActionPoints;
            //        energyBarUnit.Caption.Text = $"{expedition.Enemy.ActionPoints}/{expedition.Enemy.ActionCost}";
            //    }
            //};

            //var energyBarEnemy =
            //    new ProgressBarNew(0, 100, progressBarSize,
            //        Anchor.CenterRight)
            //    {
            //        ProgressFill = { FillColor = Color.DeepSkyBlue }
            //    };
            //middlePanel.AddChild(energyBarEnemy);
            //energyBarEnemy.BeforeDraw += e =>
            //{
            //    if (expedition.Enemy != null)
            //    {
            //        energyBarEnemy.Value = expedition.Enemy.ActionPoints;
            //        energyBarEnemy.Caption.Text = $"{expedition.Enemy.ActionPoints}/{expedition.Enemy.ActionCost}";
            //    }
            //};

            var actionBarHero =
                new ProgressBarNew(0, expedition.Hero.ActionCost, progressBarSize, Anchor.BottomLeft)
                {
                    ProgressFill = { FillColor = Color.LightGoldenrodYellow }
                };
            middlePanel.AddChild(actionBarHero);
            actionBarHero.BeforeDraw += e =>
            {
                if (expedition.Enemy != null)
                {
                    actionBarHero.Max = (uint)expedition.Hero.ActionCost;
                    actionBarHero.Value = (int)expedition.Hero.ActionPoints;
                    actionBarHero.Caption.Text = $"{(int)expedition.Hero.ActionPoints}/{expedition.Hero.ActionCost}";
                }
            };

            var actionBarEnemy =
                new ProgressBarNew(0, 100, progressBarSize, Anchor.BottomRight)
                {
                    ProgressFill = { FillColor = Color.LightGoldenrodYellow }
                };
            middlePanel.AddChild(actionBarEnemy);
            actionBarEnemy.BeforeDraw += e =>
            {
                if (expedition.Enemy != null)
                {
                    actionBarEnemy.Max = (uint)expedition.Enemy.ActionCost;
                    actionBarEnemy.Value = (int)expedition.Enemy.ActionPoints;
                    actionBarEnemy.Caption.Text = $"{(int)expedition.Enemy.ActionPoints}/{expedition.Enemy.ActionCost}";
                }
            };
            #endregion

            // special case if no need in stat bars
            NoEnemyPanel = new PanelBrownThick(middlePanel.SizeInternal);
            middlePanel.AddChild(NoEnemyPanel);
            var message = new Paragraph(expedition.Event.Name, Anchor.Center);
            NoEnemyPanel.AddChild(message);

            // consumables panel
            // (int) rounding to not miss some pixels due to approximation later
            var consumablePanelWidth = (int)SizeInternal.X / ConsumablePanels.Capacity;
            var consumablesPanel =
                new PanelEmpty(new Vector2(SizeInternal.X, consumablePanelWidth), Anchor.Auto);
            AddChild(consumablesPanel);
            for (var i = 0; i < ConsumablePanels.Capacity; i++)
            {
                var consumablePanel = new PanelBlackThin(new Vector2(consumablePanelWidth, consumablePanelWidth),
                    Anchor.AutoInline);
                consumablesPanel.AddChild(consumablePanel);
                var consumableImage = new Icon((IconType)i, Anchor.Center);
                consumablePanel.AddChild(consumableImage);
                consumableImage.Size = consumablePanel.SizeInternal - new Vector2(4, 4);
            }

            var separator = new HorizontalLine();
            AddChild(separator);

            // skills panel
            var skillsPanelHeight = SizeInternal.Y -
                                    (HeroImagePanel.Size.Y + consumablesPanel.Size.Y + separator.Size.Y);
            var skillsPerRow = (int)Math.Ceiling((float)(SkillPanels.Capacity / 2));
            var skillPanelHeight = (int)(skillsPanelHeight / 2);
            // used to center the skills panels
            var skillsPanel = new PanelEmpty(new Vector2(skillPanelHeight * skillsPerRow, skillsPanelHeight),
                Anchor.BottomLeft, new Vector2(1, 1));
            AddChild(skillsPanel);
            for (var i = 0; i < SkillPanels.Capacity; i++)
            {
                var skillPanel = new PanelBlackThin(new Vector2(skillPanelHeight, skillPanelHeight),
                    Anchor.AutoInline);
                skillsPanel.AddChild(skillPanel);
                SkillPanels.Add(skillPanel);

                var skillImage = new IconNew(skillPanel.SizeInternal);
                skillPanel.AddChild(skillImage);
                skillImage.BeforeDraw += e =>
                {
                    var s = SkillPanels.IndexOf(skillPanel);
                    if (s < Expedition.Hero.Abilities.Count)
                    {
                        skillImage.Texture = Expedition.Hero.Abilities[s].Texture;
                        ToolTipText = Expedition.Hero.Abilities[s].Name;
                    }
                };
            }

            // log panel
            var logPanel = new PanelBrownThin(new Vector2(skillsPanelHeight, skillsPanelHeight - 1), Anchor.BottomRight,
                new Vector2(1, 1));
            AddChild(logPanel);

            // money panel
            var moneyPanelHeight = (int)skillsPanelHeight / 2;
            var moneyPanel =
                new PanelBlackThin(
                    new Vector2(SizeInternal.X - (skillsPanel.Size.X + logPanel.Size.X), moneyPanelHeight),
                    Anchor.BottomRight, new Vector2(logPanel.Size.X + 2, moneyPanelHeight + 1));
            AddChild(moneyPanel);

            // exp bar panel
            var expPanel = new PanelBlackThin(new Vector2(moneyPanel.Size.X, moneyPanelHeight), Anchor.BottomRight,
                new Vector2(skillsPanelHeight + 2, 1));
            AddChild(expPanel);

            // propagate all internal children invents to this
            foreach (var ent in GetChildren())
            {
                ent.PropagateEventsTo(this);
                foreach (var entInternal in ent.GetChildren())
                {
                    entInternal.PropagateEventsTo(this);
                    foreach (var entInternal2 in entInternal.GetChildren())
                    {
                        entInternal2.PropagateEventsTo(this);
                    }
                }
            }
        }

        public override void UpdateChildrenVisibility()
        {
            // special case if no expeditions
            switch (Expedition.Event.GetType().Name)
            {
                case "EnemyEncounter":
                    NoEnemyPanel.Visible = false;
                    break;
                default:
                    NoEnemyPanel.Visible = true;
                    break;
            }
        }
    }
}
