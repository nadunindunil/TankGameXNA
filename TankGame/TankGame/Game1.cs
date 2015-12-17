using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TankClient;

namespace TankGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;

        Texture2D carriageTexture;
        Texture2D tankUp;
        Texture2D tankDown;
        Texture2D tankRight;
        Texture2D tankLeft;
        Texture2D bulletUp;
        Texture2D bulletDown;
        Texture2D bulletLeft;
        Texture2D bulletRight;
        Texture2D brick;
        Texture2D stone;
        Texture2D water;
        Texture2D grass;
        Texture2D coin;
        Texture2D medic;
        public static String brickSimbol = "▥";
        public static String stoneSimbol = "▦";
        public static String waterSimbol = "▩";
        public static String blankSimbol = "▢";
        public static String coinSimbol = "◉";
        public static String lifePackSimbol = "☩";
        String[,] map1;
        public static String[] playerDir = { "▲", "►", "▼", "◄" };
        DecodeOperations dec = DecodeOperations.GetInstance();
         

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
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Tank Game";

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
            device = graphics.GraphicsDevice;
            tankUp = Content.Load<Texture2D>("tank_up");
            tankDown = Content.Load<Texture2D>("Tank_down");
            tankLeft = Content.Load<Texture2D>("Tank_left");
            tankRight = Content.Load<Texture2D>("Tank_right");
            bulletUp = Content.Load<Texture2D>("bullet_up");
            bulletDown = Content.Load<Texture2D>("bullet_down");
            bulletLeft = Content.Load<Texture2D>("bullet_left");
            bulletRight = Content.Load<Texture2D>("bullet_right");
            brick = Content.Load<Texture2D>("brick");
            stone = Content.Load<Texture2D>("stone_brick");
            medic = Content.Load<Texture2D>("medic_bag");
            coin = Content.Load<Texture2D>("coin");
            water = Content.Load<Texture2D>("water");
            grass = Content.Load<Texture2D>("grass");
            
        }

        
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            Draw(gameTime);
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
            spriteBatch.Begin();
            //DrawScenery();
            DrawPlayers();

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawPlayers()
        {
            String str=null;
            map1 = dec.getMap();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    if (map1[i,j] != "")
                    {
                        //PlayerData player = new PlayerData();
                        //String str = null;
                        //player.type = net.map[i][j].type;
                        //player.user = net.map[i][j].user;
                        //player.Direction = net.map[i][j].Direction;
                        //player.Position = new Vector2((i * 45) + 4, ((j * 45) + 5));
                        Vector2 Position = new Vector2((i * 45) + 4, ((j * 45) + 5));
                        String init = map1[i, j];
                        if (init == brickSimbol)
                        {
                            str = "brick";
                        }
                        else if (init == stoneSimbol)
                        {
                            str = "stone_brick";
                        }
                        else if (init == waterSimbol)
                        {
                            str = "water";
                        }
                        else if (playerDir.Contains(init))
                        {
                            
                            
                                if (init == playerDir[3])
                                {
                                    str = "Tank_left";
                                }
                                else if (init == playerDir[2])
                                {
                                    str = "Tank_down";
                                }
                                else if (init == playerDir[1])
                                {
                                    str = "Tank_right";
                                }
                                else
                                {
                                    str = "tank_up";
                                }
                            
                            //if (player.user == 0)
                            //{
                            //    if (player.Direction.Equals("West"))
                            //    {
                            //        str = "enemy_left";
                            //    }
                            //    else if (player.Direction.Equals("South"))
                            //    {
                            //        str = "enemy_down";
                            //    }
                            //    else if (player.Direction.Equals("East"))
                            //    {
                            //        str = "enemy_right";
                            //    }
                            //    else
                            //    {
                            //        str = "enemy_up";
                            //    }
                            //}

                        }
                        else if (init == coinSimbol)
                        {
                            str = "coin";
                        }
                        else if (init == lifePackSimbol)
                        {
                            str = "medic_bag";
                        }
                        //else if (init == )
                        //{
                        //    str = "bullet";
                        //}
                        carriageTexture = Content.Load<Texture2D>(str);
                        spriteBatch.Draw(carriageTexture, Position, Color.White);


                    }
                    else
                    {
                        //PlayerData player = new PlayerData();
                        ////String str = null;
                        //player.type = net.map[i][j].type;
                        //player.Position = new Vector2((i * 45) + 4, ((j * 45) + 5));
                        //carriageTexture = Content.Load<Texture2D>("blank");
                        try
                        {
                            //spriteBatch.Begin();
                            //spriteBatch.Draw(carriageTexture, player.Position, Color.White);
                            //spriteBatch.End();
                        }
                        catch (Exception e) { Console.WriteLine(e); }

                    }
                }

            }
        }
    }
}
