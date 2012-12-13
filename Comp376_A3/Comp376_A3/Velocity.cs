using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comp376_A1
{
  class Velocity
  {
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }

    public Velocity(float x, float y, float z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }
    public Velocity(Velocity v)
    {
      x = v.x;
      y = v.y;
      z = v.z;
    }

    public Point move(Point p)
    {
      Point toRet = new Point(p.x+x, p.y+y, p.z+z);
      return toRet;
    }
    
  }
}
