using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MinoMus.TwoDimensional;
using The_Legend_Of_Kynortas.Objects;
namespace The_Legend_Of_Kynortas
{
    public enum GameState
    {
        Playing, Dialog,Fading, Pause
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static NPC tempNpc;
        public static SpriteFont font32;
        public static SpriteFont font12;
        static Character player;
        Camera cam;
        public static NPC currentSpeaker;
        public static GameState state = GameState.Playing;
        public static string currentDialog;
        private string dialogLeftovers ="";

        public static TileMap currentMap;

        public static List<TileMap> maps;

        public static Texture2D boxTexture;
        public Texture2D DialogTexture;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //graphics.PreferredBackBufferHeight = 600;
            //graphics.PreferredBackBufferWidth = 800;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            
            IsMouseVisible = true;
            Tile.Initialize(Content);
            ObjectCreator.Initialize(Content);
            DialogTexture = Content.Load<Texture2D>("DialogBox");
            
            font32 = Content.Load<SpriteFont>("font32");
            font12 = Content.Load<SpriteFont>("font12");
            player = new Character("Jimmy", Content.Load<Texture2D>("PlayerWalk"), new Vector2(200, 0), 4, Direction.Down, new Vector2(22, 0), new Vector2(25, 22));

            List<NPC> check = new List<NPC>();


            maps = new List<TileMap>();
            maps.Add(MapCreator.CreatePlayerHome());
            maps.Add(MapCreator.CreatePlayerBedRoom());
            PortalConnector.ConnectAll();
            currentMap = maps[0];
            //currentMap.AddCharacter(tempNpc);

            

            boxTexture = new Texture2D(GraphicsDevice, 1, 1);
            boxTexture.SetData<Color>(new Color[] { Color.White });

            cam = new Camera(GraphicsDevice.Viewport);
            cam.Zoom = 1f;
            // TODO: use this.Content to load your game content here
        }

        static Portal nextPortal;
        public static void SetCurrentMap(Portal dest)
        {
            nextPortal = dest;
            state = GameState.Fading;
        }

        bool Up = true;
        Color color = new Color(0,0,0,0);
        
        public void Fade()
        {
            byte steps = 15;
            if (Up && color.A > 255 - steps) steps = (byte)(255 - color.A);
            else if (!Up && color.A < steps) steps = (byte)(color.A); 
            if (Up) color.A += steps;
            else color.A -= steps;
            if (color.A == 255 || color.A == 0)
            {
                Up = !Up;
                currentMap = nextPortal.Map;
                SetPlayerLocation(nextPortal.PortalRow, nextPortal.PortalColumn);
            }
            if (color.A == 0) state = GameState.Playing;
        }
        protected override void UnloadContent()
        {
        }

        KeyboardState lastState;
        GameState lastGameState;
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            bool pausedNow = false;

            if (state == GameState.Playing)
            {
                if (state != GameState.Pause && lastState.IsKeyUp(Keys.P) && Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    lastGameState = state;
                    state = GameState.Pause;
                    pausedNow = true;
                }
                player.CheckIfCanLook();
                if (lastState.IsKeyUp(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    stringIndex = 0;
                    player.UseNearest();
                    if (state == GameState.Dialog)
                    {
                        if (currentSpeaker != null) currentDialog = currentSpeaker.Speak();
                        NormalizeDialog();
                    }
                }
                player.Update(gameTime);
                currentMap.Update(gameTime);
            }
            else if (state == GameState.Dialog)
            {
                if (lastState.IsKeyUp(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (stringIndex == currentDialog.Length)
                    {
                        stringIndex = 0;
                        if (dialogLeftovers != "")
                        {
                            currentDialog = dialogLeftovers;
                            NormalizeDialog();
                        }
                        else
                        {
                            state = GameState.Playing;
                        }
                    }
                    else stringIndex = currentDialog.Length;
                }
            }
            else if(state== GameState.Fading)
            {
                Fade();
            }
            else if (state == GameState.Pause)
            {
                if (!pausedNow &&  lastState.IsKeyUp(Keys.P) && Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    state = lastGameState;
                }
            }

            cam.Update(player.Position);
            lastState = Keyboard.GetState();
            base.Update(gameTime);
        }

        private void NormalizeDialog()
        {
            int margins = 20;
            char[] clone;
            int textCapacity = (int)(DialogTexture.Width* 0.9f);
            dialogLeftovers = "";
            int i = 0,end = 0;
            for (int lines = 0; lines < 3 && i < currentDialog.Length; lines++)
            {
                end = i;
                while (end < currentDialog.Length && font32.MeasureString(currentDialog.Substring(i, end - i)).X < textCapacity) end++;
                while (end < currentDialog.Length && end > 0 && currentDialog[end] != ' ' ) end--;
                clone = currentDialog.ToCharArray();
                if(end >= currentDialog.Length) break;
                clone[end] = '\n';
                currentDialog = new string(clone);
                i = end + 1;
            }
            if (end < currentDialog.Length)
            {
                end++;
                while (end < currentDialog.Length && font32.MeasureString(currentDialog.Substring(i, end - i)).X < textCapacity) end++;
                while (end < currentDialog.Length && end > 0 && currentDialog[end] != ' ') end--;
                clone = currentDialog.ToCharArray();
                char[] tmp = new char[currentDialog.Length - i];
                for (int j = 0; j < currentDialog.Length - i; j++)
                {
                    tmp[j] = currentDialog[j + i];
                    clone[j + i] = ' ';
                }
                currentDialog = new string(clone);
                dialogLeftovers = (new string(tmp));
            }

            //int margins = 20;
            //char[] clone;
            //int textCapacity = (int)(DialogTexture.Width / (font32.MeasureString("T").X * 0.75));
            //dialogLeftovers = "";
            //int i = textCapacity - 1;
            //for (int lines = 0; lines < 2 && i < currentDialog.Length ; lines++)
            //{
            //    while (currentDialog[i] != ' ' && i > 0) i--;
            //    clone = currentDialog.ToCharArray();
            //    clone[i] = '\n';
            //    currentDialog = new string(clone);
            //    i += textCapacity;
            //}
            
            
            //if (i < currentDialog.Length)
            //{
            //    while (currentDialog[i] != ' ' && i > 0) i--;
            //    i++;
            //    clone = currentDialog.ToCharArray();
            //    char[] tmp = new char[currentDialog.Length - i];
            //    for (int j = 0; j < currentDialog.Length - i; j++)
            //    {
            //        tmp[j] = currentDialog[j + i];
            //        clone[j + i] = ' ';
            //    }
            //    currentDialog = new string(clone);
            //    dialogLeftovers = (new string(tmp));
            //}
        }

        int stringIndex = 0;
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,cam.Transform);
            
            //string str = "";
            //str += " Jimmy " + c.CollisionRectangle.Bottom + " \n";
            //for (int i = 0; i < check.Count; i++)
            //{
            //    str+= check[i].Name + " " + check[i].CollisionRectangle.Bottom + " \n";
            //}
            //spriteBatch.DrawString(font, str, Vector2.Zero, Color.Black);
            //foreach (var npc in check)
            //{
            //    npc.DrawByY();    
            //}

            player.DrawByY();
            currentMap.Draw(spriteBatch);
            CharacterManager.DrawAll(spriteBatch);
            CharacterManager.DrawTops(spriteBatch);

            if (state == GameState.Fading)
            {
                spriteBatch.Draw(boxTexture, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), color);
            }

            if (state == GameState.Pause)
            {
                spriteBatch.Draw(boxTexture, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), new Color(0,0,0,75));
            }

            spriteBatch.End();

            spriteBatch.Begin();

            if (state == GameState.Dialog)
            {
                int margins = 20;
                Vector2 pos = new Vector2(0, graphics.PreferredBackBufferHeight - DialogTexture.Height);
                spriteBatch.Draw(DialogTexture, new Vector2(0, graphics.PreferredBackBufferHeight - DialogTexture.Height), Color.White);
                string tmp = currentDialog.Substring(0, stringIndex);
                if (stringIndex < currentDialog.Length) stringIndex++;
                spriteBatch.DrawString(font32, tmp, pos + Vector2.One * margins, Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        internal static void SetPlayerLocation(float i, float j)
        {
            player.Position = new Vector2(j * Tile.TileSize.X, i * Tile.TileSize.Y);
        }
    }
}
