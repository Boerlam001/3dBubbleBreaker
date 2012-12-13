using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comp376_A1
{
  abstract class MModel
  {

    //position of the displayable in the window
    public Point position{ get; set; }
    public Velocity velocity { get; set; }
    //dimensions of the displayable in the window
    public Dimension dimensions{ get; set; }

    private Point target;
    public MModel(Point p)
    {
      position = new Point(p);
      velocity = new Velocity(0,0,0);
    }
    public MModel(Point p, Dimension d)
    {
      position = new Point(p);
      dimensions = new Dimension(d);
      velocity = new Velocity(0,0,0);
    }
    public MModel(MModel m)
    {
      position = new Point(m.position);
      dimensions = new Dimension(m.dimensions);
      velocity = new Velocity(m.velocity);
      if (target != null)
      {
        target = new Point(m.target);
      }
    }
    public void setVelocity(Velocity v)
    {
      velocity = new Velocity(v);
    }
    public virtual Point move()
    {
      if (target != null)
      {
        if (target.x <= position.x && target.y <= position.y)
        {
          position = new Point(target);
          target = null;
          this.stop();
        }
      }
      position = velocity.move(position);
      return position;
    }
    public void moveTo(Point p, float speed)
    {
      float x = p.x - position.x;
      float y = p.y - position.y;
      float z = p.z - position.z;
      velocity = new Velocity(x/speed, y/speed, z/speed);
      target = new Point(p);
    }
    public virtual void stop()
    {
      velocity.x = 0;
      velocity.y = 0;
      velocity.z = 0;
    }
  }
}
