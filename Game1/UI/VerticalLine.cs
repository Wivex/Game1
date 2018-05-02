using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI
{
    public class VerticalLine : HorizontalLine
    {
        public VerticalLine(Anchor anchor = Anchor.Auto, Vector2? offset = null) :
            base(anchor, offset)
        {
        }

        /// <summary>
        /// Draw the entity.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch to draw on.</param>
        protected override void DrawEntity(SpriteBatch spriteBatch)
        {
            //var origin = new Vector2(Resources.HorizontalLineTexture.Width/2, Resources.HorizontalLineTexture.Height / 2);
            //spriteBatch.Draw(Resources.HorizontalLineTexture, destinationRectangle: InternalDestRect, origin: origin,
            //    rotation: (float) Math.PI / 2);

            //spriteBatch.End();
        }
    }
}