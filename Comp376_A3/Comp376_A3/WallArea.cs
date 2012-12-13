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
  class WallArea : DrawableGameComponent
  {
    List<WallBlock> wallBlocks;
    Point botLeft;
    Point topRight;

    Dimension dimensions { get { return new Dimension(topRight.x - botLeft.x, topRight.y - botLeft.y, topRight.z - botLeft.z); } }
    int cols { get { return dimensions.width / Wall.size.width; } }
    public WallArea(Point BL, Point TR)
      :base(BubbleCreator.game)
    {
      botLeft = new Point(BL);
      topRight = new Point(TR);
      wallBlocks = new List<WallBlock>();
      initWallBlocks();
    }

    private void initWallBlocks()
    {
      for (int i = 0; i < cols;i++)
      {
        Point tempBottom = new Point(botLeft.x + (i*Wall.size.width), botLeft.y, botLeft.z);
        Point tempTop = new Point(botLeft.x + (i*Wall.size.width), topRight.y, topRight.z);
        WallBlock tempWall = new WallBlock(tempBottom, tempTop);
        wallBlocks.Add(tempWall);
      }
    }
    public override void Draw(GameTime gameTime)
    {
      for (int i = 0; i < wallBlocks.Count; i++)
      {
        wallBlocks[i].Draw(gameTime);
      }
      base.Draw(gameTime);
    }
  }
}
