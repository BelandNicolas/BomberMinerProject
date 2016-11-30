using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BomberMinerV2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        SpriteFont debugFont;
        string debugTexte = "";

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keys = new KeyboardState();
        KeyboardState previousKeys = new KeyboardState();


        // Fond de tuiles
        GameObjectTile fond;
        GameObjectPlayer heros;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = (int)(Settings.SCREEN_WIDTH * Settings.PIXEL_RATIO);
            this.graphics.PreferredBackBufferHeight = (int)(Settings.SCREEN_HEIGHT * Settings.PIXEL_RATIO);
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

            debugFont = Content.Load<SpriteFont>("DebugFont");

            // TODO: use this.Content to load your game content here
            fond = new GameObjectTile();
            fond.texture = Content.Load<Texture2D>("TileSet.png");

            heros = new GameObjectPlayer();
            heros.estVivant = true;
            heros.vitesse = new Vector2(5, 5);
            heros.direction = Vector2.Zero;
            heros.objetState = GameObjectPlayer.etats.attenteDroite;
            heros.position = new Rectangle(350, 250, 128, 128);   //Position initiale de Rambo
            heros.sprite = Content.Load<Texture2D>("Bomberman.png");

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
            keys = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            heros.position.X += (int)(heros.vitesse.X * heros.direction.X);
            heros.position.Y += (int)(heros.vitesse.Y * heros.direction.Y);

            if (keys.IsKeyDown(Keys.D))
            {
                heros.direction.X = 2;
                heros.objetState = GameObjectPlayer.etats.runDroite;
            }
            if (keys.IsKeyUp(Keys.D) && previousKeys.IsKeyDown(Keys.D))
            {
                heros.direction.X = 0;
                heros.objetState = GameObjectPlayer.etats.attenteDroite;
            }
            if (keys.IsKeyDown(Keys.A))
            {
                heros.direction.X = -2;
                heros.objetState = GameObjectPlayer.etats.runGauche;
            }
            if (keys.IsKeyUp(Keys.A) && previousKeys.IsKeyDown(Keys.A))
            {
                heros.direction.X = 0;
                heros.objetState = GameObjectPlayer.etats.attenteGauche;
            }
            if (keys.IsKeyDown(Keys.W))
            {
                heros.direction.Y = -2;
                heros.objetState = GameObjectPlayer.etats.runHaut;
            }
            if (keys.IsKeyUp(Keys.W) && previousKeys.IsKeyDown(Keys.W))
            {
                heros.direction.Y = 0;
                heros.objetState = GameObjectPlayer.etats.attenteHaut;
            }
            if (keys.IsKeyDown(Keys.S))
            {
                heros.direction.Y = 2;
                heros.objetState = GameObjectPlayer.etats.runBas;
            }
            if (keys.IsKeyUp(Keys.S) && previousKeys.IsKeyDown(Keys.S))
            {
                heros.direction.Y = 0;
                heros.objetState = GameObjectPlayer.etats.attenteBas;
            }


            // TODO: Add your update logic here

            for (int ligne = 0; ligne < fond.map.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < fond.map.GetLength(1); colonne++)
                {
                    Rectangle tuile = new Rectangle();
                    tuile.X = colonne * GameObjectTile.LARGEUR_TUILE - (int)(heros.vitesse.X * heros.direction.X);
                    tuile.Y = ligne * GameObjectTile.HAUTEUR_TUILE - (int)(heros.vitesse.Y * heros.direction.Y);
                    tuile.Width = GameObjectTile.LARGEUR_TUILE;
                    tuile.Height = GameObjectTile.HAUTEUR_TUILE;
                    if (tuile.Intersects(heros.position))
                    {
                        switch (fond.map[ligne, colonne])
                        {
                            case 1:          // ne rien faire
                                debugTexte = "Collision";
                                break;
                            case 2:        // empêcher le mouvement
                                break;
                            case 3:        // faire une autre action...
                                debugTexte = "Vide";
                                break;
                        }
                    }
                }
            }

            heros.Update(gameTime);
            previousKeys = keys;

            base.Update(gameTime);
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


            fond.Draw(spriteBatch);
            spriteBatch.Draw(heros.sprite, heros.position, heros.spriteAfficher, Color.White);

            spriteBatch.DrawString(debugFont, debugTexte, new Vector2(100, 100), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
