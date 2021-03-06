﻿using System;
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
using TankClient.AI;
using TankClient.Players;
using System.Threading;
using System.Threading.Tasks;

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

        private Communicator com;
        private Boolean isJoined = false;
        private DecodeOperations dec = DecodeOperations.GetInstance();
        private Controller con = null;
        private String[,] mainmap;

        Texture2D carriageTexture;
        Texture2D tankUp;
        Texture2D tankDown;
        Texture2D tankRight;
        Texture2D tankLeft;
        Texture2D etankUp;
        Texture2D etankDown;
        Texture2D etankRight;
        Texture2D etankLeft;
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
        Texture2D[,] tex;
        public static String brickSimbol = "B";
        public static String stoneSimbol = "S";
        public static String waterSimbol = "W";
        public static String blankSimbol = "N";
        public static String coinSimbol = "C";
        public static String lifePackSimbol = "M";
        String[,] map1;
        public static String[] playerDir = { "up", "right", "down", "left" };
        public static String[] playerDir1 = { "up1", "right1", "down1", "left1" };

       


         

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            Console.WriteLine("test in construct null =" + null);
            this.com = Communicator.GetInstance();
            this.con = Controller.GetInstance();
            join();
            //aiwork();
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
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 510;
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
            //tex=new Texture2D[10,10];
            //for (int i = 0; i < 10; i++)
            //{
            //    for (int y = 0; y< 10; y++)
            //    {
            //        tex[i, y] = Content.Load<Texture2D>("grass");
            //    }
            //}
            tankUp = Content.Load<Texture2D>("tankup1");
            tankDown = Content.Load<Texture2D>("tankdown1");
            tankLeft = Content.Load<Texture2D>("tankleft1");
            tankRight = Content.Load<Texture2D>("tankright1");
            etankUp = Content.Load<Texture2D>("etu");
            etankDown = Content.Load<Texture2D>("etd");
            etankLeft = Content.Load<Texture2D>("etl");
            etankRight = Content.Load<Texture2D>("etr");
            bulletUp = Content.Load<Texture2D>("bu");
            bulletDown = Content.Load<Texture2D>("bd");
            bulletLeft = Content.Load<Texture2D>("bl");
            bulletRight = Content.Load<Texture2D>("br");
            brick = Content.Load<Texture2D>("brick1");
            stone = Content.Load<Texture2D>("stone1");
            medic = Content.Load<Texture2D>("medic1");
            coin = Content.Load<Texture2D>("coin1");
            water = Content.Load<Texture2D>("water1");
            grass = Content.Load<Texture2D>("grass1");
            
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
            DrawPlayers(mainmap);

            //for (int x = 0; x< 10; x++)
            //{
            //    for (int y = 0; y < 10; y++)
            //    {
            //        Rectangle rectangle = new Rectangle(x*50,y*50,50,50);
            //        spriteBatch.Draw(tex[x,y],rectangle,Color.White);

            //    }
            //}

            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void join()
        {
            if (!isJoined)
            {
                try
                {
                    com.sendData(Constants.JOIN);
                    Console.WriteLine("Join");
                    Thread thread = new Thread(() => com.readAndSetData(this));
                    Thread threadDo = new Thread(() => con.controll());
                    Thread threadShoot = new Thread(() => con.shoot());
                    thread.Start();
                    //threadDo.Start();
                    //threadShoot.Start();
                    this.isJoined = true;
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                Console.WriteLine("Already Joined");
            }


        }

        public void displaymap(String[,] map2)
        {

            mainmap = map2;
            Console.WriteLine("map value  " + map2[0,0]);
            Console.WriteLine("test in display");
        
        }

        private void aiwork()
        {
            DecodeOperations dc = DecodeOperations.GetInstance();

            Controller con = Controller.GetInstance();
            Player myPlayer = dc.getPlayer(dc.PlayerNo + 1);
            Console.WriteLine("A");
            MapItem[,] itList = con.createMapItemList(dc.getMap());
            Console.WriteLine("B");

            String whatToFind = null;
            switch ("L")
            {
                case "Br": whatToFind = "B"; break;
                case "S": whatToFind = "S"; break;
                case "W": whatToFind = "W"; break;
                case "B": whatToFind = "N"; break;
                case "C": whatToFind = "C"; break;
                case "L": whatToFind = "L"; break;
            }

            Thread threadDo = new Thread(() => con.controll());

            threadDo.Start();

        }
        private void DrawPlayers(String[,] map1)
        {
            

            Console.WriteLine(map1);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    String str = "grass1";
                    Console.WriteLine("loop val  " + map1[i, j]);
                    if (map1[i, j] != null)
                    {
                        //PlayerData player = new PlayerData();
                        //String str = null;
                        //player.type = net.map[i][j].type;
                        //player.user = net.map[i][j].user;
                        //player.Direction = net.map[i][j].Direction;
                        //player.Position = new Vector2((i * 45) + 4, ((j * 45) + 5));
                        Vector2 Position = new Vector2(((j * 50) + 5), ((i * 50) + 5));

                        String init = map1[i, j];

                        if (init == brickSimbol)
                        {
                            str = "brick1";
                        }
                        else if (init == stoneSimbol)
                        {
                            str = "stone1";
                        }
                        else if (init == waterSimbol)
                        {
                            str = "water1";
                        }
                        else if (playerDir.Contains(init) || playerDir1.Contains(init))
                        {


                            if (init == playerDir[3])
                            {
                                str = "tankleft1";
                            }
                            else if (init == playerDir[2])
                            {
                                str = "tankdown1";
                            }
                            else if (init == playerDir[1])
                            {
                                str = "tankright1";
                            }
                            else
                            {
                                str = "tankup1";
                            }

                            //if (playerDir1.Contains(init))
                            //{
                            //    if (init == playerDir1[3])
                            //    {
                            //        str = "etl";
                            //    }
                            //    else if (init == playerDir1[2])
                            //    {
                            //        str = "etd";
                            //    }
                            //    else if (init == playerDir1[1])
                            //    {
                            //        str = "etr";
                            //    }
                            //    else
                            //    {
                            //        str = "etu";
                            //    }
                            //}

                        }
                        else if (init == coinSimbol)
                        {
                            str = "coin1";
                        }
                        else if (init == lifePackSimbol)
                        {
                            str = "medic1";
                        }
                        //else if (init == blankSimbol )
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
                        Vector2 Position = new Vector2(((j * 50)+5), ((i * 50)+5));
                        //carriageTexture = Content.Load<Texture2D>("blank");
                        try
                        {
                            //spriteBatch.Begin();
                            spriteBatch.Draw(carriageTexture, Position, Color.White);
                            //spriteBatch.End();
                        }
                        catch (Exception e) { Console.WriteLine(e); }

                    }
                }

            }
        }
    }
}
