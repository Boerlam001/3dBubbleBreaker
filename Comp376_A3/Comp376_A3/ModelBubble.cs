using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comp376_A1
{
  public enum BubbleColor{
    blue = 0,
    brown = 1,
    green = 2,
    orange = 3,
    red = 4,
    teal = 5,
    yellow = 6,
    popped = 7
  }

  class ModelBubble : MModel
  {
    public static int WIDTH = 2;
    public static int HEIGHT = 2;
    public static int DEPTH = 2;
    //color of the bubble
    public BubbleColor color { get; set; }
    public Point motionRotation;

    public ModelBubble(BubbleColor c)
      :base(new Point(0,0,0),new Dimension(WIDTH, HEIGHT, DEPTH))
    {
      color = c;
      motionRotation = new Point(0.0f, 0.0f, 0.0f);
    }

    public ModelBubble(BubbleColor c, Point p)
      :base(new Point(p),new Dimension(WIDTH,HEIGHT,DEPTH))
    {
      color = c;
      motionRotation = new Point(0.0f, 0.0f, 0.0f);
    }
    public ModelBubble(ModelBubble b)
      : base(b)
    {
      color = b.color;
      motionRotation = new Point(0.0f, 0.0f, 0.0f);
    }

    public override Point move()
    {
      motionRotation.x -= velocity.y;
      if (velocity.y != 0)
      {
        motionRotation.y = -(float)Math.Atan(velocity.x / velocity.y);
      }
      return base.move();
    }

    public void setColor(BubbleColor color)
    {
      this.color = color;
    }

    public override void  stop()
    {
      motionRotation.x = 0;
      motionRotation.y = 0;
 	    base.stop();
    }
  }
}
