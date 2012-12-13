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
  class WallBlock : DrawableGameComponent
  {
    List<Wall> walls;
    Point start;
    Point end;
    Point rotation;
    int distance;
    public WallBlock(Point startPoint, Point endPoint)
      :base(BubbleCreator.game)
    {
      start = startPoint;
      end = endPoint;
      calcRotation();
      calcDistance();
      initWall();
    }
    private void calcRotation()
    {
      float z = end.z - start.z;
      float y = end.y - start.y;
      float x = end.x - start.x;
      rotation = new Point(0f, 0f, 0f);
      if (y != 0)
      {
        rotation.x = (float)Math.Atan(z / y);
        
      }
      if (x != 0)
      {
        rotation.z = (float)Math.Atan(y / x);
        rotation.y = (float)Math.Atan(z / x);
      }
      if(y==0){
        rotation.z = MathHelper.ToRadians(-90);
      }
      
    }

    private void calcDistance()
    {
      float z = end.z - start.z;
      float y = end.y - start.y;
      float x = end.x - start.x;
      double x2 = Math.Pow(x, 2);
      double y2 = Math.Pow(y, 2);
      double z2 = Math.Pow(z, 2);
      distance = (int)(Math.Sqrt(x2 + y2 + z2));
      distance /= Wall.size.height;
    }

    private void initWall()
    {
      walls = new List<Wall>();
      
      for(int i=0; i<distance; i++)
      {
        Point p = new Point(getStartPoint());
        //p.x += i * (Wall.size.width/2);
        p.y += i*Wall.size.height;
        //p.z -= i * (Wall.size.depth/2);
        Wall temp = new Wall(Game, p,rotation);
        walls.Add(temp);
      }
    }

    public override void Draw(GameTime gameTime)
    {
      foreach(Wall wall in walls)
      {
        wall.Draw(gameTime);
      }
      base.Draw(gameTime);
    }

    public Point getStartPoint()
    {
      Point toRet = new Point(0f,0f,0f);
      toRet.y = start.x * (rotation.z / MathHelper.ToRadians(-90)) + start.z * (rotation.x / MathHelper.ToRadians(90)) +(start.y - (start.y * (rotation.x / MathHelper.ToRadians(90))) - (start.y * (rotation.z / MathHelper.ToRadians(-90))));
      toRet.x = start.y * (rotation.z / MathHelper.ToRadians(90)) + start.z * (rotation.y / MathHelper.ToRadians(90)) + (start.x - (start.x * (rotation.y / MathHelper.ToRadians(90))) - (start.x * (rotation.z / MathHelper.ToRadians(-90))));
      toRet.z = start.x * (rotation.y / MathHelper.ToRadians(90)) + start.y * (rotation.x / MathHelper.ToRadians(90)) + (start.z - (start.z * (rotation.x / MathHelper.ToRadians(-90))) - (start.z * (rotation.y / MathHelper.ToRadians(90))));
      return toRet;
    }
  }
}
