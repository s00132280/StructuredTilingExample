using AnimatedSprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tiler;
using Tiling;

// created this class 29/11/17 at 13:44

namespace TileBasedPlayer20172018
{
    class Sentry : RotatingSprite
    {
        //List<TileRef> images = new List<TileRef>() { new TileRef(15, 2, 0)};
        //TileRef currentFrame;
        float turnspeed = 0.03f;
        public Vector2 previousPosition;

        public Sentry(Game game, Vector2 userPosition,
            List<TileRef> sheetRefs, int frameWidth, int frameHeight, float layerDepth) 
                : base(game, userPosition, sheetRefs, frameWidth, frameHeight, layerDepth)
        {
            DrawOrder = 1;

        }

        public void Collision(Collider c)
        {
            if (BoundingRectangle.Intersects(c.CollisionField))
                PixelPosition = previousPosition;
        }

        public override void Update(GameTime gameTime)
        {
            

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
