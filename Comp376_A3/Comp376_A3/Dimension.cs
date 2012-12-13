using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comp376_A1
{
  class Dimension
  {
    public int height { get; set; }
    public int width{ get; set; }
    public int depth { get; set; }

    public Dimension(int w, int h, int d)
    {
      width= w;
      height = h;
      depth = d;
    }
    public Dimension(float w, float h, float d)
    {
      width = (int)w;
      height = (int)h;
      depth = (int)d;
    }

    public Dimension(Dimension d)
    {
      width = d.width;
      height = d.height;
      depth = d.depth;
    }
  }
}
