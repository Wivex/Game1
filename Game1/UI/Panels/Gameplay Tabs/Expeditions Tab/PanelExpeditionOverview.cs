using System;
using System.Collections.Generic;
using Game1.Concepts;
using Game1.Objects.Units;
using Game1.UI.GeonUI_Overrides;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public class PanelExpeditionOverview : ButtonPanel
    {
        public Expedition Expedition { get; set; }

        public PanelEmpty NoEnemyPanel { get; set; }
        public PanelEmpty HeroImagePanel { get; set; }
        public PanelEmpty EventImagePanel { get; set; }
        public List<PanelEmpty> SkillPanels { get; set; } = new List<PanelEmpty>(10);
        public List<PanelEmpty> ConsumablePanels { get; set; } = new List<PanelEmpty>(10);

        public PanelExpeditionOverview(Vector2 size, Expedition expedition) : base(size)
        {
            Expedition = expedition;
            Expedition.ExpeditionOverviewPanel = this;

            // hero icon
            HeroImagePanel =
                new PanelBrownInternal(
                    new Vector2((int)(SizeInternal.Y * 0.4f), (int)(SizeInternal.Y * 0.4f)),
                    Anchor.TopLeft);
            AddChild(HeroImagePanel);
            var heroImage = new ImageNew(expedition.Hero.Texture, HeroImagePanel.SizeInternal)
            {
                ToolTipText = $@"{expedition.Hero.Name} the {expedition.Hero.XMLData.Name}"
            };
            HeroImagePanel.AddChild(heroImage);

            // event icon
            EventImagePanel = new PanelBrownInternal(HeroImagePanel.Size,
                Anchor.TopRight);
            AddChild(EventImagePanel);
            var eventImage = new ImageNew(expedition.Event.Texture, EventImagePanel.SizeInternal);
            eventImage.BeforeDraw += e =>
            {
                eventImage.Texture = expedition.Event.Texture;
                eventImage.ToolTipText = Expedition.Enemy == null ? expedition.Event.Name : Expedition.Enemy.Name;
            };
            EventImagePanel.AddChild(eventImage);

            var barsPanel = new PanelBlack(
                new Vector2(SizeInternal.X - (HeroImagePanel.Size.X + EventImagePanel.Size.X),
                    HeroImagePanel.Size.Y), Anchor.TopCenter);
            AddChild(barsPanel);
            InitBarsPanel(barsPanel);

            var separator = new HorizontalLine();
            AddChild(separator);

            // consumables panel
            // (int) rounding to not miss some pixels due to approximation later
            var consumablePanelWidth = (int)SizeInternal.X / ConsumablePanels.Capacity;
            var consumablesPanel =
                new PanelEmpty(new Vector2(SizeInternal.X, consumablePanelWidth), Anchor.AutoCenter);
            AddChild(consumablesPanel);
            for (var i = 0; i < ConsumablePanels.Capacity; i++)
            {
                var consumablePanel = new PanelBlack(new Vector2(consumablePanelWidth, consumablePanelWidth), Anchor.AutoInline);
                consumablesPanel.AddChild(consumablePanel);
                ConsumablePanels.Add(consumablePanel);

                var consumableImage = new ImageZooming(consumablePanel.SizeInternal);
                consumableImage.BeforeDraw += e =>
                {
                    var c = ConsumablePanels.IndexOf(consumablePanel);
                    if (c < Expedition.Hero.Consumables.Count)
                    {
                        consumableImage.Texture = Expedition.Hero.Consumables[c].Texture;
                        consumableImage.ToolTipText = Expedition.Hero.Consumables[c].Name;
                    }
                };
                consumablePanel.AddChild(consumableImage);
            }

            AddChild(new HorizontalLine());

            // skills panel
            var skillsPanelHeight = SizeInternal.Y -
                                    (HeroImagePanel.Size.Y + consumablesPanel.Size.Y +
                                     separator.Size.Y * 2);
            var skillsPerRow = (int)Math.Ceiling((float)(SkillPanels.Capacity / 2));
            var skillPanelHeight = (int)(skillsPanelHeight / 2);
            var skillsPanel = new PanelEmpty(new Vector2(skillPanelHeight * skillsPerRow, skillsPanelHeight), Anchor.BottomLeft, -Vector2.UnitY);
            AddChild(skillsPanel);
            for (var i = 0; i < SkillPanels.Capacity; i++)
            {
                var skillPanel = new PanelEmpty(new Vector2(skillPanelHeight, skillPanelHeight),
                    Anchor.AutoInline);
                skillsPanel.AddChild(skillPanel);
                SkillPanels.Add(skillPanel);

                var skillImage = new ImageZooming(skillPanel.SizeInternal, true);
                skillImage.BeforeDraw += e =>
                {
                    var s = SkillPanels.IndexOf(skillPanel);
                    if (s < Expedition.Hero.Abilities.Count)
                    {
                        skillImage.Texture = Expedition.Hero.Abilities[s].Texture;
                        skillImage.ToolTipText = Expedition.Hero.Abilities[s].Name;
                    }
                };
                skillPanel.AddChild(skillImage);
            }

            // log panel
            var logPanel = new PanelBrownInternal(new Vector2(skillsPanelHeight, skillsPanelHeight-1), Anchor.BottomRight);
            AddChild(logPanel);

            // money panel
            var moneyPanelHeight = (int)skillsPanelHeight / 2;
            var moneyPanel =
                new PanelBlack(
                    new Vector2(SizeInternal.X - (skillsPanel.Size.X + logPanel.Size.X)+1, moneyPanelHeight),
                    Anchor.BottomRight, new Vector2(logPanel.Size.X, moneyPanelHeight));
            AddChild(moneyPanel);

            // exp bar panel
            var expPanel = new PanelBlack(new Vector2(moneyPanel.Size.X, moneyPanelHeight), Anchor.BottomRight,
                new Vector2(skillsPanelHeight, 0));
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

        public void InitBarsPanel(PanelEmpty parentPanel)
        {
            var progressBarSize =
                new Vector2((int)parentPanel.SizeInternal.X / 2 - 3, (int)parentPanel.Size.Y / 3 - 3);

            var healthBarHero =
                new ProgressBarNew(progressBarSize, Anchor.TopLeft)
                {
                    ToolTipText = "Hero health"
                };
            healthBarHero.BeforeDraw += e =>
            {
                if (Expedition.Enemy != null)
                {
                    healthBarHero.Value = Expedition.Hero.Health;
                    healthBarHero.Max = (uint)Expedition.Hero.XMLData.Stats[Stat.Health];
                    healthBarHero.Caption.Text = $"{healthBarHero.Value}/{healthBarHero.Max}";
                }
            };
            parentPanel.AddChild(healthBarHero);

            var healthBarEnemy =
                new ProgressBarNew(progressBarSize, Anchor.TopRight)
                {
                    ToolTipText = "Enemy health"
                };
            healthBarEnemy.BeforeDraw += e =>
            {
                if (Expedition.Enemy != null)
                {
                    healthBarEnemy.Value = Expedition.Enemy.Health;
                    healthBarEnemy.Max = (uint)Expedition.Enemy.XMLData.Stats[Stat.Health];
                    healthBarEnemy.Caption.Text = $"{healthBarEnemy.Value}/{healthBarEnemy.Max}";
                }
            };
            parentPanel.AddChild(healthBarEnemy);

            //var energyBarUnit =
            //    new ProgressBarNew(progressBarSize, Anchor.CenterLeft)
            //    {
            //        ProgressFill = { FillColor = Color.DeepSkyBlue }
            //    };
            //parentPanel.AddChild(energyBarUnit);
            //energyBarUnit.BeforeDraw += e =>
            //{
            //    if (Expedition.Enemy != null)
            //    {
            //        energyBarUnit.Value = (int)Expedition.Enemy.ActionPoints;
            //        energyBarUnit.Caption.Text = $"{Expedition.Enemy.ActionPoints}/{Expedition.Enemy.ActionCost}";
            //    }
            //};

            //var energyBarEnemy =
            //    new ProgressBarNew(progressBarSize,
            //        Anchor.CenterRight)
            //    {
            //        ProgressFill = { FillColor = Color.DeepSkyBlue }
            //    };
            //parentPanel.AddChild(energyBarEnemy);
            //energyBarEnemy.BeforeDraw += e =>
            //{
            //    if (Expedition.Enemy != null)
            //    {
            //        energyBarEnemy.Value = (int)Expedition.Enemy.ActionPoints;
            //        energyBarEnemy.Caption.Text = $"{Expedition.Enemy.ActionPoints}/{Expedition.Enemy.ActionCost}";
            //    }
            //};

            var actionBarHero =
                new ProgressBarNew(progressBarSize, Anchor.BottomLeft)
                {
                    ProgressFill = { FillColor = Color.LightGoldenrodYellow },
                    ToolTipText = "Hero action bar"
                };
            actionBarHero.BeforeDraw += e =>
            {
                if (Expedition.Enemy != null)
                {
                    actionBarHero.Value = (int)Expedition.Hero.ActionPoints;
                    actionBarHero.Max = (uint)Expedition.Hero.ActionCost;
                    actionBarHero.Caption.Text = $"{actionBarHero.Value}/{actionBarHero.Max}";
                }
            };
            parentPanel.AddChild(actionBarHero);

            var actionBarEnemy =
                new ProgressBarNew(progressBarSize, Anchor.BottomRight)
                {
                    ProgressFill = { FillColor = Color.LightGoldenrodYellow },
                    ToolTipText = "Enemy action bar"
                };
            parentPanel.AddChild(actionBarEnemy);
            actionBarEnemy.BeforeDraw += e =>
            {
                if (Expedition.Enemy != null)
                {
                    actionBarEnemy.Value = (int)Expedition.Enemy.ActionPoints;
                    actionBarEnemy.Max = (uint)Expedition.Enemy.ActionCost;
                    actionBarEnemy.Caption.Text = $"{actionBarEnemy.Value}/{actionBarEnemy.Max}";
                }
            };

            // special case if no need in stat bars
            NoEnemyPanel = new PanelBrownExternal(parentPanel.SizeInternal);
            parentPanel.AddChild(NoEnemyPanel);
            var message = new Paragraph(Expedition.Event.Name, Anchor.Center);
            NoEnemyPanel.AddChild(message);
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
