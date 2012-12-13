using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comp376_A1
{
  class BubbleHolder
  {
    public static GridSquare[,] grid;
    private static List<Bubble> popped;
    private static List<Bubble> stuckToWall;
    public static List<Bubble> bubbles;
    public static int score;
    public static List<Bubble> droppingBubbles;
    public static bool checkColliding(Bubble b)
    {
      bool isColliding = false;
      for (int i = 0; i <= grid.GetUpperBound(0); i++)
      {
        for (int j = 0; j <= grid.GetUpperBound(1); j++)
        {
          if (grid[i, j].isColliding(b.getPosition()))
          {
            isColliding = true;
          }
        }
      }
      return isColliding;
    }

    public static void initGrid(float posX, float posY, float posZ)
    {
      score = 0;
      int right = 0, down = 0;
      float increment = 2;
      float zSloap = -1;
      grid = new GridSquare[8, 10];
      for (int j = 0; j <= grid.GetUpperBound(1); j++)
      {
        down--;
        zSloap+=0.5f;
        for (int i = 0; i <= grid.GetUpperBound(0); i++)
        {
          right++;
          if (down % 2 == 0)
          {
            grid[i, j] = new GridSquare(new Point(posX + 1 + (right * 2), posY + (down * increment), posZ + zSloap));
            grid[i, j].x = i;
            grid[i, j].y = j;
          }
          else
          {
            grid[i, j] = new GridSquare(new Point(posX + (right * 2), posY + (down * increment), posZ + zSloap));
            grid[i, j].x = i;
            grid[i, j].y = j;
          }

        }
        right = 0;
      }
    }
    public static float getSnappingScore(Bubble b, GridSquare grid)
    {
      return (Math.Abs(b.getPosition().x - grid.point.x) + Math.Abs(b.getPosition().y - grid.point.y) + Math.Abs(b.getPosition().z - grid.point.z));
    } 
    public static void snapToGrid(Bubble b)
    {
      bool found = false;
      GridSquare best;
      best = grid[0, 0];
      for (int j = 0; j <= grid.GetUpperBound(1); j++)
      {
        for (int i = 0; i <= grid.GetUpperBound(0); i++)
        {
          if (grid[i, j].enoughToSnap(b.getPosition()) && grid[i, j].bubble==null)
          {
            best = grid[i,j];
            found = true;
            break;
          }
          else if(grid[i,j].bubble ==null && getSnappingScore(b, grid[i,j]) < getSnappingScore(b, best))
          {
            best = grid[i, j];
          }
        }
        if (found)
        {
          break;
        }
      }
       
      b.setPosition(new Point(best.point));
      best.bubble = b;
      popped = new List<Bubble>();
      stuckToWall = new List<Bubble>();
      checkPopping(best);
      if (popped.Count > 2)
      {
        popBubbles();
        droppingBubbles = new List<Bubble>();
        checkDropping(grid[0,0]);
        checkDropping(grid[1, 0]);
        checkDropping(grid[2, 0]);
        checkDropping(grid[3, 0]);
        checkDropping(grid[4, 0]);
        checkDropping(grid[5, 0]);
        checkDropping(grid[6, 0]);
        checkDropping(grid[7, 0]);
        getRemainingBubbles();
        dropBubbles();
      }
    }

    private static void checkPopping(GridSquare tempGrid)
    {
      List<int[]> positions = new List<int[]>();
      if (tempGrid.y % 2 == 1)
      {
        positions.Add(new int[] { tempGrid.x, tempGrid.y - 1 });      //top left
        positions.Add(new int[] { tempGrid.x + 1, tempGrid.y - 1 });  //top right
        positions.Add(new int[] { tempGrid.x - 1, tempGrid.y });      //left
        positions.Add(new int[] { tempGrid.x + 1, tempGrid.y });      //right
        positions.Add(new int[] { tempGrid.x, tempGrid.y + 1 });      //bottom left
        positions.Add(new int[] { tempGrid.x + 1, tempGrid.y + 1 });      //bottom right
      }
      else
      {
        positions.Add(new int[] { tempGrid.x - 1, tempGrid.y - 1 });      //top left
        positions.Add(new int[] { tempGrid.x, tempGrid.y - 1 });  //top right
        positions.Add(new int[] { tempGrid.x - 1, tempGrid.y });      //left
        positions.Add(new int[] { tempGrid.x + 1, tempGrid.y });      //right 
        positions.Add(new int[] { tempGrid.x - 1, tempGrid.y + 1 });      //bottom left
        positions.Add(new int[] { tempGrid.x, tempGrid.y + 1 });      //bottom right
      }

      for (int i = 0; i < positions.Count;i++ )
      {
        if(positions[i][0]>=0 && positions[i][0]<= grid.GetUpperBound(0) && positions[i][1]>=0 && positions[i][1]<=grid.GetUpperBound(1))
        {
          Bubble b = grid[positions[i][0], positions[i][1]].bubble;
          if( b != null && b.getColor()==tempGrid.bubble.getColor() && !popped.Contains(b))
          {
            if(!popped.Contains(tempGrid.bubble))
            {
              popped.Add(tempGrid.bubble);
            }
            popped.Add(b);
            checkPopping(grid[positions[i][0], positions[i][1]]);
          }
        }
      }
    }

    private static void checkDropping(GridSquare tempGrid)
    {
      List<int[]> positions = new List<int[]>();
      if(tempGrid.bubble==null ||  tempGrid.bubble.getColor() == BubbleColor.popped)
      {
        return;
      }
      if (tempGrid.y % 2 == 1)
      {
        positions.Add(new int[] { tempGrid.x, tempGrid.y - 1 });      //top left
        positions.Add(new int[] { tempGrid.x + 1, tempGrid.y - 1 });  //top right
        positions.Add(new int[] { tempGrid.x - 1, tempGrid.y });      //left
        positions.Add(new int[] { tempGrid.x + 1, tempGrid.y });      //right
        positions.Add(new int[] { tempGrid.x, tempGrid.y + 1 });      //bottom left
        positions.Add(new int[] { tempGrid.x + 1, tempGrid.y + 1 });      //bottom right
      }
      else
      {
        positions.Add(new int[] { tempGrid.x - 1, tempGrid.y - 1 });      //top left
        positions.Add(new int[] { tempGrid.x, tempGrid.y - 1 });  //top right
        positions.Add(new int[] { tempGrid.x - 1, tempGrid.y });      //left
        positions.Add(new int[] { tempGrid.x + 1, tempGrid.y });      //right 
        positions.Add(new int[] { tempGrid.x - 1, tempGrid.y + 1 });      //bottom left
        positions.Add(new int[] { tempGrid.x, tempGrid.y + 1 });      //bottom right
      }
      for (int i = 0; i < positions.Count; i++)
      {
        if (positions[i][0] >= 0 && positions[i][0] <= grid.GetUpperBound(0) && positions[i][1] >= 0 && positions[i][1] <= grid.GetUpperBound(1))
        {
          Bubble b = grid[positions[i][0], positions[i][1]].bubble;
          if (b != null && !stuckToWall.Contains(b) &&  b.getColor() != BubbleColor.popped)
          {
            stuckToWall.Add(b);
            checkDropping(grid[positions[i][0], positions[i][1]]);
          }
        }
      }
      
    }

    private static void popBubbles()
    {
      for (int i = 0; i < popped.Count;i++ )
      {
        popped[i].popBubble();
        score += 10;
      }
    }

    public static void RemovePopped()
    {
      for (int j = 0; j <= grid.GetUpperBound(1); j++)
      {
        for (int i = 0; i <= grid.GetUpperBound(0); i++)
        {
          if (grid[i,j].bubble!= null && grid[i, j].bubble.isPopped)
          {
            grid[i, j].bubble = null;
          }
        }
      }
    }

    public static List<Bubble> getBubbles()
    {
      List<Bubble> toRet = new List<Bubble>();
      for (int j = 0; j <= grid.GetUpperBound(1); j++)
      {
        for (int i = 0; i <= grid.GetUpperBound(0); i++)
        {
          if (grid[i, j].bubble != null && !grid[i,j].bubble.isPopped)
          {
            toRet.Add(grid[i, j].bubble);
          }
        }
      }
      if (droppingBubbles != null)
      {
        for (int i = 0; i < droppingBubbles.Count; i++)
        {
          toRet.Add(droppingBubbles[i]);
        }
      }
      return toRet;
    }

    public static int checkGameStatus()
    {
      if (getBubbles().Count == 0)
      {
        return 0;
      }
      for(int i=0; i < grid.GetUpperBound(0);i++){
        if(grid[i,grid.GetUpperBound(1)].bubble!=null)
        {
          return 1;
        }
      }
      return 2;

    }

    public static void getRemainingBubbles()
    {
      bubbles = new List<Bubble>();
      for (int j = 0; j <= grid.GetUpperBound(1); j++)
      {
        for (int i = 0; i <= grid.GetUpperBound(0); i++)
        {
          if (grid[i, j].bubble != null && grid[i, j].bubble.getColor() != BubbleColor.popped)
          {
            bubbles.Add(grid[i, j].bubble);
          }
        }
      }
    }
    public static void dropBubbles()
    {
      //Console.WriteLine("Dropping bubbles " + (bubbles.Count - stuckToWall.Count));
      if (bubbles.Count == stuckToWall.Count) return;
      List<Bubble> toDrop = new List<Bubble>();
      
      
          int toScore = 1;
      
      for (int i = 0; i < bubbles.Count;i++ )
      {
        if (!stuckToWall.Contains(bubbles[i]))
        {
          bubbles[i].dropBubble();
          droppingBubbles.Add(bubbles[i]);
          bubbles.RemoveAt(i);
          toScore*=2;
          i--;
        }
      }
      BubbleHolder.score += toScore*10;
      for (int j = 0; j <= grid.GetUpperBound(1); j++)
      {
        for (int i = 0; i <= grid.GetUpperBound(0); i++)
        {
          for (int k = 0; k < droppingBubbles.Count; k++)
          {
            if (grid[i, j].bubble != null && grid[i, j].bubble == droppingBubbles[k])
            {
              grid[i, j].bubble = null;
            }
          }
        }
      }
    }

    public static void MoveCeilingDown()
    {
      grid = resizeGrid(grid, grid.GetUpperBound(0)+1, grid.GetUpperBound(1)+1);
    }

    public static GridSquare[,] resizeGrid(GridSquare[,] original, int cols, int rows)
    {
      var temp = new GridSquare[cols, rows-1];
      for (int j = 0; j < rows-1; j++)
      {
        for (int i = 0; i < cols; i++)
        {
          temp[i, j] = original[i, j];
          temp[i, j].moveDown();
        }
      }
      return temp;
    }
    public static void finalizeDropping()
    {
      if(droppingBubbles!=null)
      {
        for (int i = 0; i < droppingBubbles.Count; i++)
        {
          if (BoardMain.checkOutOfBounds(droppingBubbles[i]))
          {
            droppingBubbles.RemoveAt(i);
          }
        }
      }
    }

  }
}
