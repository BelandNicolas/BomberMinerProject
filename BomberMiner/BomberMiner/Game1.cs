using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BomberMiner
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public const float gravity = -1.7f;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        EntityMobs heros;
        GameObject block;
        BombeItems bombe;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = Settings.SCREEN_WIDTH * (int)Settings.PIXEL_RATIO;
            this.graphics.PreferredBackBufferHeight = Settings.SCREEN_HEIGHT * (int)Settings.PIXEL_RATIO;
            this.graphics.IsFullScreen = Settings.IS_FULLSCREEN;
            this.IsMouseVisible = Settings.IS_MOUSE_VISIBLE;
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
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;

            //this.graphics.ToggleFullScreen();
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
            font = Content.Load<SpriteFont>("Score");
            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = Settings.SCREEN_WIDTH;
            fenetre.Height = Settings.SCREEN_HEIGHT;

            //Chargement héros
            heros = new EntityMobs();
            heros.estVivant = true;
            heros.isFlying = true;
            heros.vitesse = 5;
            heros.sprite = Content.Load<Texture2D>("BomberManIdleFront");
            heros.position = heros.sprite.Bounds;
            heros.acceleration = 0;
            heros.life = 100;
            heros.quantityBombes = 2;

            //Chargement block
            block = new GameObject();
            block.estVivant = true;
            block.vitesse = 0;
            block.sprite = Content.Load<Texture2D>("WallBlock.png");
            block.position = block.sprite.Bounds;
            block.position.X = fenetre.Width / 2;
            block.position.Y = fenetre.Height / 2;

            //Chargement bombe
            bombe = new BombeItems();
            bombe.estVivant = false;
            bombe.sprite = Content.Load<Texture2D>("Bomb.png");
            bombe.IdleExplosion = Content.Load<Texture2D>("BombsAnimations/IdleExplosion.png");
            bombe.UpExplosion = Content.Load<Texture2D>("BombsAnimations/UpExplosion.png");
            bombe.RightExplosion = Content.Load<Texture2D>("BombsAnimations/RightExplosion.png");
            bombe.LeftExplosion = Content.Load<Texture2D>("BombsAnimations/LeftExplosion.png");
            bombe.DownExplosion = Content.Load<Texture2D>("BombsAnimations/DownExplosion.png");
            bombe.isExploded = false;
            bombe.position = bombe.sprite.Bounds;
            bombe.scale.X = 20;
            bombe.scale.Y = 20;
            bombe.position.Size = bombe.scale;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            #region DeplacementHeros
            if (heros.estVivant == true)
            {
                if (heros.isFlying == true)
                {
                    heros.acceleration += (int)gravity;
                    heros.position.Y -= heros.acceleration;

                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        heros.position.X += heros.vitesse;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        heros.position.X -= heros.vitesse;
                    }
                    if (heros.position.Y + heros.sprite.Bounds.Height > fenetre.Bottom)
                    {
                        heros.position.Y = fenetre.Bottom - heros.sprite.Bounds.Height;
                    }
                }
                else if (heros.isFlying == false)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        heros.position.X += heros.vitesse;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        heros.position.X -= heros.vitesse;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        heros.acceleration = 20;
                        heros.position.Y -= heros.vitesse;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        heros.position.Y -= heros.vitesse;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        heros.position.Y += heros.vitesse;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                    {
                        if (bombe.estVivant == false && bombe.isExploded == false)
                        {
                            bombe.estVivant = true;
                            bombe.position.X = heros.position.X + heros.sprite.Bounds.Width / 2 - bombe.position.Width / 2;
                            bombe.position.Y = heros.position.Bottom - bombe.position.Height;
                            heros.quantityBombes--; 
                        }
                    }

                }
            } 
            #endregion

            IsFlying();
            UpdateHeros();
            Explosion();
            base.Update(gameTime);
        }

        protected void UpdateHeros()
        {
            //Colision bord de fenêtre
            if (heros.position.X < fenetre.Left)
            {
                heros.position.X = fenetre.Left;
            }
            if (heros.position.X + heros.sprite.Bounds.Width > fenetre.Right)
            {
                heros.position.X = fenetre.Right - heros.sprite.Bounds.Width;
            }
            if (heros.position.Y < fenetre.Top)
            {
                heros.position.Y = fenetre.Top;
            }
            if (heros.position.Y + heros.sprite.Bounds.Height > fenetre.Bottom)
            {
                heros.position.Y = fenetre.Bottom - heros.sprite.Bounds.Height;
            }
            //Collision block

            if (heros.position.Intersects(block.position))
            {
                if (heros.position.Bottom > block.position.Top && heros.position.Top < block.position.Top)
                {
                    //Collision face haut
                    heros.position.Y = block.position.Y - heros.sprite.Bounds.Height;
                }
                 if (heros.position.Right > block.position.Left && heros.position.Left < block.position.Left)
                {
                    //Collision face gauche
                    heros.position.X = block.position.X - heros.sprite.Width;
                }
                 if (heros.position.Left < block.position.Right && heros.position.Right > block.position.Right)
                {
                    //Collision face droite
                    heros.position.X = block.position.Right;
                }
                 if (heros.position.Y < block.position.Bottom && heros.position.Bottom > block.position.Bottom)
                {
                    //Collision face bas
                    heros.position.Y = block.position.Y + block.sprite.Bounds.Height;
                }
            }
        }
        protected void IsFlying()
        {
            if (heros.position.Intersects(block.position))
            {
                heros.isFlying = false;
                heros.acceleration = 0;
            }
            else if (heros.position.Y + heros.sprite.Bounds.Height != fenetre.Bottom)
            {
                heros.isFlying = true;
            }
            else
            {
                heros.isFlying = false;
            }

        }
        protected void Explosion()
        {
            if (bombe.estVivant == true)
            {
                bombe.timeToExplode++;
                if (bombe.timeToExplode == 20)
                {
                    bombe.isExploded = true;
                    bombe.estVivant = false;
                    bombe.timeToExplode = 0;
                }
            }
            if (bombe.isExploded == true)
            {
                bombe.timeToDispel++;
                if (bombe.timeToDispel == 20)
                {
                    bombe.isExploded = false;
                    bombe.timeToDispel = 0;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(Settings.PIXEL_RATIO));
            spriteBatch.DrawString(font, "Life : " + heros.life, new Vector2(10, 10), Color.White);
            if (heros.estVivant == true)
            {
                spriteBatch.Draw(heros.sprite, heros.position, Color.White); 
            }
            if (block.estVivant == true)
            {
                spriteBatch.Draw(block.sprite, block.position, Color.White); 
            }
            if (bombe.estVivant == true)
            {
                spriteBatch.Draw(bombe.sprite, bombe.position, Color.White);
            }
            if (bombe.isExploded == true)
            {
                spriteBatch.Draw(bombe.IdleExplosion, bombe.position, Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}
