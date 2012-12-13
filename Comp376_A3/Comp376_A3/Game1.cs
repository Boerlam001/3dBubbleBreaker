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
using System.Runtime.InteropServices;
namespace Comp376_A1
{
  public class Game1 : Microsoft.Xna.Framework.Game
  {
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

    SpriteFont font;
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    KeyboardState oldState;
    Texture2D background;

    int screenWidth;
    int screenHeight;

    BoardGame board;
    bool isFp;

    int CurrentLevel;
    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      oldState = Keyboard.GetState();
      CurrentLevel = 1;
    }

    protected override void Initialize()
    {
      base.Initialize();
    }

    protected void initGame()
    {
      board = new BoardGame(this, CurrentLevel);
      CameraHolder.initCamera();
      Components.Add(board);
      BubbleHolder.score = 0;
      isFp = true;
    }
    protected override void LoadContent()
    {
      spriteBatch = new SpriteBatch(GraphicsDevice);
      Services.AddService(typeof(SpriteBatch),spriteBatch);
      BubbleCreator.setXNA(this);
      font = Content.Load<SpriteFont>("font");
      background = Content.Load<Texture2D>("sky");

      screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
      screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

      initGame();
    }

    protected override void UnloadContent()
    {
      Components.Remove(board);
    }

    protected override void Update(GameTime gameTime)
    {

      handleKeyboard();
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.BlendState = BlendState.Opaque;
      GraphicsDevice.DepthStencilState = DepthStencilState.Default;
      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
      Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
      spriteBatch.Draw(background, screenRectangle, Color.White);

      DrawText(board.getStatus(), 0, 0);
      spriteBatch.End();
      GraphicsDevice.BlendState = BlendState.Opaque;
      GraphicsDevice.DepthStencilState = DepthStencilState.Default;
      GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
      base.Draw(gameTime);
    }
    protected void handleKeyboard()
    {


      KeyboardState keyState = Keyboard.GetState();

      if (keyState.IsKeyDown(Keys.Escape) && !oldState.IsKeyDown(Keys.Escape))
        this.Exit();
      if (keyState.IsKeyDown(Keys.Left))
        board.changeAngle(-1);
      if (keyState.IsKeyDown(Keys.Right))
        board.changeAngle(1);
      if (keyState.IsKeyDown(Keys.Up) && !oldState.IsKeyDown(Keys.Up))
        board.shoot();

      if (keyState.IsKeyDown(Keys.A))
        CameraHolder.cameraFar.RotateY(Angle.toRadians(5));
      if (keyState.IsKeyDown(Keys.S))
        CameraHolder.cameraFar.MoveForwards(-2);
      if (keyState.IsKeyDown(Keys.D))
        CameraHolder.cameraFar.RotateY(Angle.toRadians(-5));
      if (keyState.IsKeyDown(Keys.W))
        CameraHolder.cameraFar.MoveForwards(2);
      if (keyState.IsKeyDown(Keys.Q))
        BubbleHolder.initGrid(0,0,0);
      if (keyState.IsKeyDown(Keys.C) && !oldState.IsKeyDown(Keys.C))
      {
        isFp = !isFp;
        CameraHolder.switchCameras(isFp);
      }

      if (keyState.IsKeyDown(Keys.Enter) && !oldState.IsKeyDown(Keys.Enter))
      {
        proceed();
      }
      if (keyState.IsKeyDown(Keys.R) && !oldState.IsKeyDown(Keys.R))
      {
        changeLevel(CurrentLevel);
      }
      oldState = keyState;
    }

    public static void DrawLine(SpriteBatch batch, Texture2D blank,
              float width, Color color, Vector2 point1, Vector2 point2)
    {
      float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
      float length = Vector2.Distance(point1, point2);

      batch.Draw(blank, point1, null, color,
                 angle, Vector2.Zero, new Vector2(length, width),
                 SpriteEffects.None, 0);
    }

    public void changeLevel(int lvl)
    {
      CurrentLevel = lvl;
      UnloadContent();
      initGame();
    }
    private void DrawText(string str, float x, float y)
    {
      spriteBatch.DrawString(font, str, new Vector2(x, y), Color.White);
    }

    private void proceed()
    {
      int gameStatus = BubbleHolder.checkGameStatus();
      if (gameStatus == 0)
      {
        if (CurrentLevel == 1)
        {
          changeLevel(2);
        }
        else
        {
          Exit();
        }
      }
      else if (gameStatus == 1)
      {
        changeLevel(1);
      }
    }

  }
}
