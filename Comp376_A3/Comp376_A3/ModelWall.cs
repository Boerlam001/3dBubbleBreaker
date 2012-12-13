using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comp376_A1
{
  class ModelWall : MModel
  {
    public ModelWall()
      : base(new Point(0, 0, 0), new Dimension(0,0,0))
    {
    }

    public ModelWall(Point p)
      : base(new Point(p))
    {
    }

    public ModelWall(Point p, Dimension d)
      : base(new Point(p), new Dimension(d))
    {
    }
  }
}
