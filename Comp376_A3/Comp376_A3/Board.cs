using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Comp376_A1
{
  class Board : Microsoft.Xna.Framework.DrawableGameComponent
  {
    public List<GameComponent> childComponents {get; set;}
    public const int interval = 10;
    public float currentTime;
    public Board(Game game)
      :base(game)
    {
      childComponents = new List<GameComponent>();
      currentTime = 0;
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    protected override void LoadContent()
    {
      base.LoadContent();
    }
    public override void Update(GameTime gameTime)
    {
      foreach (GameComponent child in childComponents)
      {
        if (child is DrawableGameComponent)
        {
          ((DrawableGameComponent)child).Update(gameTime);
        }
      }
      base.Update(gameTime);
    }


    public override void Draw(GameTime gameTime)
    {
      foreach (GameComponent child in childComponents)
      {
        if (child is DrawableGameComponent)
        {
          ((DrawableGameComponent)child).Draw(gameTime);
        }
      }
      base.Draw(gameTime);
    }


  }
}
