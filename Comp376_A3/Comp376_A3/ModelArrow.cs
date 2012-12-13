using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comp376_A1
{
  class ModelArrow : MModel
  {
    private static int WIDTH = 1;
    private static int HEIGHT = 4;
    private static int DEPTH = 1;
    //color of the bubble
    public Angle angle{ get; set; }
    public Angle angleZ { get; set; }

    public ModelArrow()
      :base(new Point(0,0,0),new Dimension(WIDTH, HEIGHT, DEPTH))
    {
      angle = new Angle();
      angleZ = new Angle();
    }

    public ModelArrow(Angle a, Point p)
      :base(new Point(p),new Dimension(WIDTH,HEIGHT,DEPTH))
    {
      angle = a;
      angleZ = new Angle();
    }

    public ModelArrow(Angle a, Angle az, Point p)
      : base(new Point(p), new Dimension(WIDTH, HEIGHT, DEPTH))
    {
      angle = a;
      angleZ = new Angle(az.degrees+5);
    }
  }
}
