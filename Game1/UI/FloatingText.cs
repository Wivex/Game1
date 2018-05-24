using System;
using System.Collections.Generic;
using Game1.Concepts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI
{
    public class FloatingText
    {
        public static List<FloatingText> Texts = new List<FloatingText>();

        public string Text { get; set; }
        public float LifeTimePercent { get; set; }
        public TimeSpan LifeTime { get; set; }
        public TimeSpan ElapsedTime { get; set; } = TimeSpan.Zero;
        public PanelEmpty Panel { get; set; }
        public Vector2 FloatingDestination { get; set; }
        public Vector2 CurrentOffset { get; set; }

        public FloatingText(string text, PanelEmpty panel, TimeSpan lifeTime)
        {
            Text = text;
            LifeTime = lifeTime;
            Panel = panel;

            FloatingDestination = new Vector2(Globals.RNGesus.Next(-50, 50), Globals.RNGesus.Next(-50, 0));

            Texts.Add(this);
        }

        public static void DrawAllTexts()
        {
            if (Texts.Count > 0)
            {
                for (var i = 0; i < Texts.Count; i++)
                {
                    Texts[i].DrawText();
                }
            }
        }

        public void DrawText()
        {
            ElapsedTime += Globals.Game.TargetElapsedTime;
            if (ElapsedTime < LifeTime)
            {
                LifeTimePercent = (float) (ElapsedTime.TotalMilliseconds / LifeTime.TotalMilliseconds);
                CurrentOffset = new Vector2(FloatingDestination.X * LifeTimePercent,
                    FloatingDestination.Y * LifeTimePercent);
                // binds floating text to the panel, not entire UI
                Panel.AfterDraw = e =>
                {
                    var panelCenter = Panel.CalcInternalRect().Center.ToVector2();
                    Globals.Game.SpriteBatch.Begin(blendState:BlendState.NonPremultiplied);
                    Globals.Game.SpriteBatch.DrawString(GeonBit.UI.Resources.Fonts[1], Text,
                        panelCenter + CurrentOffset,
                        new Color(Color.Red, 1-LifeTimePercent));
                    Globals.Game.SpriteBatch.End();
                };
            }
            else
            {
                Texts.Remove(this);
            }
        }
    }
}
