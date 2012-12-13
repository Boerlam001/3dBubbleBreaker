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
  class BoardMain : Board
  {
    Game game;
    Point boardPosition;
    List<WallBlock> walls;
    WallArea background;
    public static int[] verteces = { -10, 10, -2, 30, 0, 10 };
    public SpriteBatch spriteBatch { get; set; }
    private int depthOfBoard { get { return (verteces[5] - verteces[4])/2; } }

    int currentLevel;

    public BoardMain(Game g, int currentLevel)
      : base(g)
    {
      this.currentLevel = currentLevel;
      game = g;
      boardPosition = new Point(verteces[0],verteces[3], 0);
      spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
      walls = new List<WallBlock>();
      Initialize();
    }

    public override void Initialize()
    {
      BubbleHolder.initGrid(boardPosition.x, boardPosition.y, boardPosition.z);
      if (currentLevel == 2)
      {
        /*
        for (int j = 0; j < 4; j++)
        {
          for (int i = 0; i < 8; i++)
          {
            BubbleHolder.grid[i, j].bubble = BubbleCreator.createRandomBubble(BubbleHolder.grid[i, j].point);
            childComponents.Add(BubbleHolder.grid[i, j].bubble);
          }
        }
         * */
        createLevel2();
      }
      else
      {
        /*
        int[] coordinates = new int[]{ 0, 0 };
        BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, BubbleColor.red, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
        childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

        coordinates = new int[] { 0, 1 };
        BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, BubbleColor.red, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
        childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

        coordinates = new int[] { 0, 2 };
        BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, BubbleColor.red, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
        childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);
         * */
        createLevel1();
      }

      WallBlock w1 = new WallBlock(new Point(verteces[0], verteces[2], verteces[5]), new Point(verteces[0], verteces[3]+3, verteces[4])); 
      walls.Add(w1);

      //WallBlock w2 = new WallBlock(new Point(verteces[0], verteces[2], verteces[5]), new Point(verteces[1] + 2, verteces[2], verteces[5]));
      //walls.Add(w2);

      WallBlock w3 = new WallBlock(new Point(verteces[1], verteces[2], verteces[5]), new Point(verteces[1], verteces[3] + 3, verteces[4]));
      walls.Add(w3);

      WallBlock w4 = new WallBlock(new Point(verteces[0]+2, verteces[3], verteces[4]), new Point(verteces[1] + 2, verteces[3], verteces[4]));
      walls.Add(w4);

      WallBlock w5 = new WallBlock(new Point(verteces[0], verteces[2], verteces[5] - Wall.size.depth), new Point(verteces[0], verteces[3] + 3, verteces[4] - Wall.size.depth));
      walls.Add(w5);

      //WallBlock w2 = new WallBlock(new Point(verteces[0], verteces[2], verteces[5]), new Point(verteces[1] + 2, verteces[2], verteces[5]));
      //walls.Add(w2);

      WallBlock w6 = new WallBlock(new Point(verteces[1], verteces[2], verteces[5] - Wall.size.depth), new Point(verteces[1], verteces[3] + 3, verteces[4] - Wall.size.depth));
      walls.Add(w6);

      WallBlock w7 = new WallBlock(new Point(verteces[0] + 2, verteces[3], verteces[4] - Wall.size.depth), new Point(verteces[1] + 2, verteces[3], verteces[4] - Wall.size.depth));
      walls.Add(w7);

      WallBlock w8 = new WallBlock(new Point(verteces[0], verteces[2], verteces[5] - 2 * Wall.size.depth), new Point(verteces[0], verteces[3] + 3, verteces[4] - 2 * Wall.size.depth));
      walls.Add(w8);

      //WallBlock w2 = new WallBlock(new Point(verteces[0], verteces[2], verteces[5]), new Point(verteces[1] + 2, verteces[2], verteces[5]));
      //walls.Add(w2);

      WallBlock w9 = new WallBlock(new Point(verteces[1], verteces[2], verteces[5] - 2 * Wall.size.depth), new Point(verteces[1], verteces[3] + 3, verteces[4] - 2 * Wall.size.depth));
      walls.Add(w9);

      WallBlock w10 = new WallBlock(new Point(verteces[0] + 2, verteces[3], verteces[4] - 2 * Wall.size.depth), new Point(verteces[1] + 2, verteces[3], verteces[4] - 2 * Wall.size.depth));
      walls.Add(w10);

      background = new WallArea(new Point(verteces[0]+2, verteces[2], verteces[5]-5), new Point(verteces[1], verteces[3]+4, verteces[4]-5));
       

      base.Initialize();
      
    }

    public override void Update(GameTime gameTime)
    {
      childComponents = new List<GameComponent>();
      BubbleHolder.finalizeDropping();
      List<Bubble> temp = BubbleHolder.getBubbles();
      for (int i = 0; i < temp.Count; i++)
      {
          childComponents.Add(temp[i]);
      }
      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      for (int i = 0; i < walls.Count; i++)
      {
        walls[i].Draw(gameTime);
      }
      background.Draw(gameTime);
      base.Draw(gameTime);
      BubbleHolder.RemovePopped();
    }
    
    public static bool checkOutOfBounds(Bubble bubble)
    {
      
      if (bubble.getPosition().y < (verteces[2]))
      {
        return true;
      }
      return false;
       
    }
 
    public static bool checkWallCollision(Bubble bubble)
    {
      if (bubble.getPosition().x < (verteces[0] + 2) || bubble.getPosition().x > (verteces[1]-2))
      {
        return true;
      }
      return false;
    }

    public static bool checkReachedLimit(Bubble bubble)
    {
      if (bubble.getPosition().y > verteces[3])
      {
        return true;
      }
      return false;
    }

    public void createLevel2()
    {
      int[] coordinates = new int[] { 0, 0 };
      BubbleColor c = BubbleColor.red;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble.isConnected = true;
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 1, 0 };
      c = BubbleColor.red;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble.isConnected = true;
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 0, 1 };
      c = BubbleColor.red;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 2, 0 };
      c = BubbleColor.blue;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble.isConnected = true;
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 1, 1 };
      c = BubbleColor.blue;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 2, 1 };
      c = BubbleColor.blue;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 3, 0 };
      c = BubbleColor.green;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble.isConnected = true;
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 4, 0 };
      c = BubbleColor.green;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble.isConnected = true;
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 3, 1 };
      c = BubbleColor.green;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 5, 0 };
      c = BubbleColor.teal;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble.isConnected = true;
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 4, 1 };
      c = BubbleColor.teal;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 5, 1 };
      c = BubbleColor.teal;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 6, 0 };
      c = BubbleColor.brown;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble.isConnected = true;
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 7, 0 };
      c = BubbleColor.brown;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble.isConnected = true;
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 6, 1 };
      c = BubbleColor.brown;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 7, 1 };
      c = BubbleColor.brown;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      // --------------------------------------- SECOND ROW --------------------------------------------

      coordinates = new int[] { 0, 2 };
      c = BubbleColor.brown;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 1, 2 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 0, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 2, 2 };
      c = BubbleColor.orange;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 1, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 2, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 3, 2 };
      c = BubbleColor.yellow;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 4, 2 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 3, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 5, 2 };
      c = BubbleColor.red;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 4, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 5, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 6, 2 };
      c = BubbleColor.blue;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 7, 2 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 6, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 7, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);
    }
    public void createLevel1()
    {
      int[] coordinates = new int[] { 0, 0 };
      BubbleColor c = BubbleColor.red;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 1, 0 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);
      
      coordinates = new int[] { 0, 1 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 1, 1 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 2, 0 };
      c = BubbleColor.blue;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 2, 1 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 3, 0 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 3, 1 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 4, 0 };
      c = BubbleColor.green;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 5, 0 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 4, 1 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 5, 1 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 6, 0 };
      c = BubbleColor.brown;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 7, 0 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 6, 1 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 7, 1 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      // --------------------------------------- SECOND ROW --------------------------------------------

      coordinates = new int[] { 0, 2 };
      c = BubbleColor.blue;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 1, 2 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 0, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 1, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 2, 2 };
      c = BubbleColor.orange;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 2, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 3, 2 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 3, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 4, 2 };
      c = BubbleColor.yellow;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 5, 2 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 4, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 5, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 6, 2 };
      c = BubbleColor.red;
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 7, 2 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 6, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

      coordinates = new int[] { 7, 3 };
      BubbleHolder.grid[coordinates[0], coordinates[1]].bubble = new Bubble(Game, c, BubbleHolder.grid[coordinates[0], coordinates[1]].point);
      childComponents.Add(BubbleHolder.grid[coordinates[0], coordinates[1]].bubble);

    }
  }
}
