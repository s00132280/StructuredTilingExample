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
    public class Sentry : RotatingSprite
    {
        public Vector2 previousPosition;
        public static int count;
        public Projectile projectile;
        bool alive = true;
        private float previousAngle;
        TilePlayer player;
        public bool playerTargeted = false;
        public Sentry(Game game, Vector2 userPosition,
            List<TileRef> sheetRefs, int frameWidth, int frameHeight, float layerDepth) 
                : base(game, userPosition, sheetRefs, frameWidth, frameHeight, layerDepth)
        {
            DrawOrder = -4;
            count++;
        }

        public void Collision(Collider c)
        {
            if (BoundingRectangle.Intersects(c.CollisionField))
                PixelPosition = previousPosition;

        }
        // loading method for a projectile to a object.
        public void loadProjectile(Projectile p)
        {
            projectile = p;
            projectile.DrawOrder = 2;
            
        }
        // setting to dead for the draw call.
        public void dead()
        {
            alive = false;
        }

        public void Target(TilePlayer p)
        {
            player = p;
            playerTargeted = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (alive && projectile != null && projectile.ProjectileState == Projectile.PROJECTILE_STATE.STILL)
            {
                projectile.PixelPosition = this.PixelPosition;
                projectile.isHit = false;
                if (chasing && previousAngle == angleOfRotation)
                {
                    previousAngle = angleOfRotation;
                    projectile.fire(player.PixelPosition);
                }
                base.Update(gameTime);
            }
            
        }
        public override void Draw(GameTime gameTime)
        {
            if (alive) //&& projectile != null && projectile.ProjectileState != Projectile.PROJECTILE_STATE.STILL)
            {
                projectile.Draw(gameTime);
                //base.Update(gameTime);
                base.Draw(gameTime);
            }
            else if (!alive)
            {

            }
            //base.Draw(gameTime);
        }
    }
}
