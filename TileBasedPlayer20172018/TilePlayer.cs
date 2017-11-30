﻿using AnimatedSprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tiling;

namespace Tiler
{

    public class TilePlayer : RotatingSprite
    {
        //List<TileRef> images = new List<TileRef>() { new TileRef(15, 2, 0)};
        //TileRef currentFrame;
        int speed = 5;
        float turnspeed = 0.03f;
        public Vector2 previousPosition;

        public TilePlayer(Game game, Vector2 userPosition, 
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
            previousPosition = PixelPosition;
            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.PixelPosition += new Vector2(1, 0) * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.PixelPosition += new Vector2(-1, 0) * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.PixelPosition += new Vector2(0, -1) * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                this.PixelPosition += new Vector2(0, 1) * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z) || Keyboard.GetState().IsKeyDown(Keys.Q))
                this.angleOfRotation -= turnspeed;
            if (Keyboard.GetState().IsKeyDown(Keys.X) || Keyboard.GetState().IsKeyDown(Keys.E))
                this.angleOfRotation += turnspeed;

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
   }
}
