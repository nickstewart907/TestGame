using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;

namespace FirstGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        GameContent gameContent;
        private KeyboardState oldKeyboardState;
        public static SpriteBatch spriteBatch;
        public static int screenWidth;
        public static int screenHeight;

        private Player player;
        private List<Enemy> enemies;
        private EnemyFactory enemyFactory;
        private int timer;
        private bool pause, levelPause;
        private string[] levels;
        private int level;
        private int oldLevel;
        private int duration;
        private int levelTimer;
        private int levelStart;

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
            gameContent = new GameContent(Content);

            // TODO: use this.Content to load your game content here
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            //set game to 700x700 or screen max if smaller

            if (screenWidth >= 700) screenWidth = 700;
            if (screenHeight >= 700) screenHeight = 700;

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();


            //initialize everything
            enemyFactory = new EnemyFactory();
            enemies = new List<Enemy>();
            player = new Player(GraphicsDevice, 30, 30, Color.LawnGreen, 4);
            timer = 0;
            ReadFile(ref levels);
            level = levelTimer = levelStart = 0;
            LoadEnemies(enemyFactory, enemies, levels, level++, ref duration);

            oldLevel = level;
            pause = levelPause = false;
            
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

            if (levelTimer >= duration)
            {
                levelStart = timer;
                levelTimer = 0;
                LoadEnemies(enemyFactory, enemies, levels, level++, ref duration);
            }

            if (Pause()) return;
            else timer += gameTime.ElapsedGameTime.Milliseconds;

            levelTimer = timer - levelStart;

            

            // TODO: Add your update logic here
            player.Update();
                

            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].IsActive())
                    enemies.Remove(enemies[i]);
            }

            foreach (Enemy e in enemies)
                e.Update(player);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            player.Draw();

            foreach (Enemy e in enemies)
                e.Draw();

            DrawScore(timer);

            if (levelPause)
                DrawNextLevel(level);
            else if (Pause())
                DrawPause();

            if (oldLevel != level)
            {
                DrawNextLevel(level);
                oldLevel = level;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /****************************** Utility Functions *********************************************/

        public bool Pause()
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            if (newKeyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space))
                pause = !pause;

            oldKeyboardState = newKeyboardState;
             
            if (levelPause) levelPause = false;

            return pause;
        }

        public void DrawPause()
        {
            string msg = "Paused: Press Space to Continue";
            Vector2 space = gameContent.Arial.MeasureString(msg);

            spriteBatch.DrawString(gameContent.Arial, msg, new Vector2((screenWidth - space.X) / 2, screenHeight / 2), Color.White);
        }

        public void DrawNextLevel(int level)
        {
            string msg = "Level " + level.ToString() + "Complete!\nPress Space to Continue";
            Vector2 space = gameContent.Arial.MeasureString(msg);

            spriteBatch.DrawString(gameContent.Arial, msg, new Vector2((screenWidth - space.X) / 2, screenHeight / 2), Color.White);

            pause = true;
            levelPause = true;
        }

        public void DrawScore(int timer)
        {
            string score = "Score: " + timer.ToString();
            Vector2 space = gameContent.Arial.MeasureString(score);

            spriteBatch.DrawString(gameContent.Arial, score, new Vector2((screenWidth - space.X) / 2, screenHeight - 40), Color.White);
        }

        private void ReadFile(ref string[] levels)
        {
            levels = System.IO.File.ReadAllLines("C:/Users/Nick/source/repos/FirstGame/FirstGame/Levels.txt");
        }

        private void LoadEnemies(EnemyFactory enemyFactory, List<Enemy> enemies, string[] levels, int level, ref int duration)
        {
            char type;
            int count = 0, i = 0, num, width, height, speed, x, y, dir, move, edge;
            string[] line;

            //remove all enemies
            if (enemies.Count > 0) enemies.RemoveRange(0, enemies.Count);

            for (i = 0; count < level && i < levels.Length; i++) if (levels[i].Equals("Level")) count++;

            if (i == levels.Length) Exit();

            //for each string, loop until next 'Level' is reached
            //first number, x, is number of simple enemies
            //loop x times to create x enemies
            //next number is sweeper enemies
            //loop x times to create x sweeper enemies etc...
            for (; i < levels.Length; i++)
            {
                if (levels[i].Equals("Level")) return;

                //read in number of enemies to create
                line = levels[i].Split(',');

                if (line.Length == 1) duration = Int32.Parse(line[0]);

                else if (line.Length == 2)
                {
                    num = Int32.Parse(line[1]) + ++i;
                    type = char.Parse(line[0]);

                    switch (type)
                    {
                        case 's':
                            for (; i < num; i++)
                            {
                                line = levels[i].Split(',');
                                width = Int32.Parse(line[0]);
                                height = Int32.Parse(line[1]);
                                speed = Int32.Parse(line[2]);
                                x = Int32.Parse(line[3]);
                                y = Int32.Parse(line[4]);
                                move = Int32.Parse(line[5]);
                                edge = Int32.Parse(line[6]);

                                enemies.Add(enemyFactory.CreateEnemy(GraphicsDevice, width, height, Color.Red, speed, x, y, move, edge));
                            }
                            i--;
                            break;
                        case 'w':
                            for (; i < num; i++)
                            {
                                line = levels[i].Split(',');
                                speed = Int32.Parse(line[0]);
                                dir = Int32.Parse(line[1]);
                                edge = Int32.Parse(line[2]);

                                enemies.Add(enemyFactory.CreateEnemy(GraphicsDevice, Color.DarkGoldenrod, speed, dir, edge));
                            }
                            i--;
                            break;
                    }
                }
            }
        }
    }
}
