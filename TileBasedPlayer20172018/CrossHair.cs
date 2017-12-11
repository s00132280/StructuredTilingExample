using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Tiling;

namespace AnimatedSprite
{
    public class CrossHair : AnimateSheetSprite
    {
        private Game myGame;
        private float CrossHairVelocity = 5.0f;
        private static int tileWidth = 64;
        private static int tileHeight = 64;
        int mapWidth = 52, mapHeight = 21;
        public CrossHair(Game g, List<TileRef>texture, Vector2 userPosition, int framecount) : base(g,userPosition,texture,tileWidth,tileHeight,0)
        {
                myGame = g;
        }

        public override void Update(GameTime gametime)
        {
            Viewport gameScreen = myGame.GraphicsDevice.Viewport;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                this.PixelPosition += new Vector2(1, 0) * CrossHairVelocity;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                this.PixelPosition += new Vector2(-1, 0) * CrossHairVelocity;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                this.PixelPosition += new Vector2(0, -1) * CrossHairVelocity;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                this.PixelPosition += new Vector2(0, 1) * CrossHairVelocity;
            PixelPosition = Vector2.Clamp(PixelPosition, Vector2.Zero,
                                            new Vector2(mapWidth * tileWidth - tileWidth,
                                                        mapHeight * tileHeight - tileHeight));
            
            base.Update(gametime);
        }

        public override void Draw(GameTime gametime)
        {

            base.Draw(gametime);
        }
    }
}
