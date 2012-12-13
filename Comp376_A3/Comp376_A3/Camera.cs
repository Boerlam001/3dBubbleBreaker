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
using System.Runtime.InteropServices;
namespace Comp376_A1
{
  class Camera
  {
    public Point eye;
    public Point at;
    public Point up;
    public Point rotations;
    public Camera()
    {
      eye = new Point(0,0,90);
      at = new Point(0,0,0);
      up = new Point(0,1,0);
      rotations = new Point(0.0f, 0.0f, 0.0f);
    }

    public Camera(Point e, Point a, Point u)
    {
      eye = new Point(e);
      at = new Point(a);
      up = new Point(u);
      rotations = new Point(0.0f,0.0f,0.0f);
    }
    public void RotateY(float angle)
    {
      rotations.y += angle;
    }
    public void MoveForwards(float distance)
    {
      eye.z -= distance;
    }

    public Matrix getView()
    {
      Matrix view = Matrix.CreateLookAt(new Vector3(eye.x, eye.y, eye.z), new Vector3(at.x, at.y, at.z), new Vector3(up.x, up.y, up.z));
      return view;
    }

    public Matrix getProjection()
    {
      Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 600f, 0.001f, 1000f);
      return projection;
    }

    public Matrix getWorldMatrix()
    {
      Matrix world = Matrix.CreateRotationX(rotations.x);
      world *= Matrix.CreateRotationY(rotations.y);
      world *= Matrix.CreateRotationZ(rotations.z);
      return world;
    }
  }
}
