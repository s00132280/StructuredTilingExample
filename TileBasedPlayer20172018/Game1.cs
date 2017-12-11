using CameraNS;
using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Tiler;
using Tiling;
using Helpers;
using AnimatedSprite;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace TileBasedPlayer20172018
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        SoundEffect bulletExplosion;
        SoundEffect Shooting;
        Song backingTrack;
        SoundEffect gameOver;

        bool musicPlaying = false;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sentry sentrys;
        int tileWidth = 64;
        int tileHeight = 64;
        TilePlayer player { get; set; }
        Explosion bullet;
        Projectile explosion;
        
        List<Sentry> sentryList = new List<Sentry>();
        List<TileRef> TileRefs = new List<TileRef>();
        List<Collider> colliders = new List<Collider>();
        string[] backTileNames = { "blue box", "pavement", "blue steel", "green box", "home" };
        public enum TileType { BLUEBOX, PAVEMENT, BLUESTEEL, GREENBOX, HOME };
        int[,] tileMap = new int[,]
    {
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2},
        {2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2},
        {2,1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,1,1,0,0,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,1,2},
        {2,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,3,3,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},
        {2,1,1,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,1,1,0,0,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,1,1,2},
        {2,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,2},
        {2,1,1,0,0,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,0,1,1,2},
        {2,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,2},
        {2,1,1,0,0,0,1,1,1,3,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,3,1,1,1,0,0,0,1,1,2},
        {2,1,1,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,1,1,2},
        {2,1,1,0,0,0,1,1,1,3,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,3,1,1,1,0,0,0,1,1,2},
        {2,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,2},
        {2,1,1,0,0,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,0,1,1,2},
        {2,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,2},
        {2,1,1,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,1,1,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,1,1,2},
        {2,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,3,3,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},
        {2,1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,1,1,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,1,2},
        {2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2},
        {2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2}
    };

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            new Camera(this, Vector2.Zero,
                new Vector2(tileMap.GetLength(1) * tileWidth, tileMap.GetLength(0) * tileHeight));
            new InputEngine(this);
             Services.AddService(new TilePlayer(this, new Vector2(64, 128), new List<TileRef>()
            {
                new TileRef(15, 2, 0),
                new TileRef(15, 3, 0),
                new TileRef(15, 4, 0),
                new TileRef(15, 5, 0),
                new TileRef(15, 6, 0),
                new TileRef(15, 7, 0),
                new TileRef(15, 8, 0),
            }, 64, 64, 0f));

            player = (TilePlayer)Services.GetService(typeof(TilePlayer));
            player.AddHealthBar(new HealthBar(this, player.PixelPosition));

            SetColliders(TileType.BLUESTEEL);
            SetColliders(TileType.BLUEBOX);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(spriteBatch);
            Services.AddService(Content.Load<Texture2D>(@"Tiles/tank tiles 64 x 64"));

            // Tile References to be drawn on the Map corresponding to the entries in the defined 
            // Tile Map
            // "free", "pavement", "blue steel", "green box", "home" 
            TileRefs.Add(new TileRef(4, 2, 0));
            TileRefs.Add(new TileRef(3, 3, 1));
            TileRefs.Add(new TileRef(6, 3, 2));
            TileRefs.Add(new TileRef(6, 2, 3));
            TileRefs.Add(new TileRef(0, 2, 4));
            // Names fo the Tiles

            new SimpleTileLayer(this, backTileNames, tileMap, TileRefs, tileWidth, tileHeight);
            List<Tile> found = SimpleTileLayer.getNamedTiles(backTileNames[(int)TileType.GREENBOX]);

            Projectile playerBullet = new Projectile(this, new List<TileRef>()
            {
                new TileRef(5,0,0)
            },
            new AnimateSheetSprite(this, player.PixelPosition, new List<TileRef>()
            {
                new TileRef(7,0,0),
                new TileRef(8,0,0),
                new TileRef(0,0,0),
                new TileRef(1,0,0),
                new TileRef(2,0,0)
            }, tileWidth, tileHeight, 1), player.PixelPosition, 1);


            player.LoadProjectile(playerBullet);

            
            //List<Tile> found = SimpleTileLayer.getNamedTiles("green box");
            // added a senytry gameservice similer to a player. 29/11/17 at 13:44
            for (int i = 0; i < found.Count; i++)
            {
                sentrys = new Sentry(this,new Vector2(found[i].X * tileWidth,found[i].Y * tileHeight), new List<TileRef>()
                {
                    new TileRef(20, 2, 0),
                    new TileRef(20, 3, 0),
                    new TileRef(20, 4, 0),
                    new TileRef(20, 5, 0),
                    new TileRef(20, 6, 0),
                    new TileRef(20, 7, 0),
                    new TileRef(20, 8, 0),
                }, 64, 64, 0f);
                sentryList.Add(sentrys);
                
                Projectile projectile = new Projectile(this, new List<TileRef>()
                {
                    new TileRef(0,0,0)
                },
                new AnimateSheetSprite(this, player.PixelPosition, new List<TileRef>()
                {
                    new TileRef(7,0,0),
                    new TileRef(8,0,0),
                    new TileRef(0,0,0),
                    new TileRef(1,0,0),
                    new TileRef(2,0,0)
                }, tileWidth, tileHeight, 1), player.PixelPosition, 1);

                sentryList[i].loadProjectile(projectile);
                sentryList[i].Health = 2;
            }

            player.playerCrossHair = new CrossHair(this, new List<TileRef>()
            {
                new TileRef(11,6,0)
            }, player.PixelPosition, 1);
            // insert soundEffects and songs!
            bulletExplosion = Content.Load<SoundEffect>("SoundEffects/atari_boom");
            Shooting = Content.Load<SoundEffect>("SoundEffects/atari_boom2");

            backingTrack = Content.Load<Song>("Songs/bgm_action_1");
            

        }

        public void SetColliders(TileType t)
        {
            for (int x = 0; x < tileMap.GetLength(1); x++)
                for (int y = 0; y < tileMap.GetLength(0); y++)
                {
                    if (tileMap[y, x] == (int)t)
                    {
                        colliders.Add(new Collider(this,
                            Content.Load<Texture2D>(@"Tiles/collider"),
                            x, y
                            ));
                    }

                }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            for (int i = 0; i < sentryList.Count; i++)
            {
                sentryList[i].follow(player);

                
                if (sentryList[i].projectile.ProjectileState == Projectile.PROJECTILE_STATE.EXPOLODING && sentryList[i].projectile.collisionDetect(player))
                {
                    if (sentryList[i].projectile.isHit)
                    {
                        player.Health -= 10;
                        sentryList[i].projectile.isHit = true;
                    }
                }

                if (player.projectile.ProjectileState == Projectile.PROJECTILE_STATE.EXPOLODING && player.projectile.collisionDetect(sentryList[i]))
                {
                    if (!player.projectile.isHit)
                    {
                        sentryList[i].dead();
                        player.projectile.isHit = true;
                    }
                }
            }

            if (!musicPlaying)
            {
                MediaPlayer.Play(backingTrack);
                MediaPlayer.IsRepeating = true;
                musicPlaying = true;
            }
            else if (player.Health <= 0)
            {
                musicPlaying = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            base.Draw(gameTime);
        }
    }
}
