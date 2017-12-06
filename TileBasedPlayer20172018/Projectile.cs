using AnimatedSprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Tiling;
using Microsoft.Xna.Framework.Graphics;
using Tiler;

namespace TileBasedPlayer20172018
{
    class Projectile : RotatingSprite
    {
        //List<TileRef> images = new List<TileRef>() { new TileRef(15, 2, 0)};
        //TileRef currentFrame;
        int speed = 5;
        float turnspeed = 0.03f;
        public Vector2 previousPosition;

        public enum PROJECTILE_STATE { STILL, FIRING, EXPOLODING };
        PROJECTILE_STATE projectileState = PROJECTILE_STATE.STILL;
        protected Game myGame;
        protected float RocketVelocity = 4.0f;
        Vector2 textureCenter;
        Vector2 Target;
        RotatingSprite explosion;
        float ExplosionTimer = 0;
        float ExplosionVisibleLimit = 1000;
        Vector2 StartPosition;

        public PROJECTILE_STATE ProjectileState
        {
            get { return projectileState; }
            set { projectileState = value; }
        }

        public RotatingSprite Explosion
        {
            get { return explosion; }
            set { explosion = value; }
        }

        public Projectile(Game game, Vector2 userPosition,
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
