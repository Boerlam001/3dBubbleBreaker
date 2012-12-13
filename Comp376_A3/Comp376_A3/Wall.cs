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
  class Wall : Drawable
  {
    //public Point startPos;
    //public Point endPos;
    Point rotation;
    public static Dimension size = new Dimension(2,4,2);
    public float left { get { return (getPosition().x - size.width); }}
    public float right{ get { return (getPosition().x + size.width); } }
    public Wall(Game g)
      : base(g, 0, 0, new ModelWall())
    {
      /*
      startPos = new Point(0,0,0);
      endPos= new Point(0,0,0);
       * */
    }

    public Wall(Game g, Point p1, Point rot)
      : base(g, 0, 0, new ModelWall(p1))
    {
      rotation = rot;
      /*
      startPos = new Point(p1);
      //endPos = new Point(p2);
       * */
    }
    /*
    public Wall(Game g, Point p1, Point p2)
      : base(g, 0, 0, new ModelWall())
    {
      /*
      startPos = new Point(p1);
      endPos = new Point(p2);
       * 
    }
    */
    protected override Model LoadTexture()
    {
      Model toLoad = Game.Content.Load<Model>("Models\\Wall");
      return toLoad;
    }

    public Matrix getRotation(){
      Matrix rot = Matrix.CreateFromYawPitchRoll(rotation.y, rotation.x, rotation.z);// .CreateRotationX(rotation.x);
      //rot *= Matrix.CreateRotationY(rotation.y);
      //rot *= Matrix.CreateRotationZ(rotation.z);
      return rot;
    }

    public override void Draw(GameTime gameTime)
    {
      
      world = Matrix.CreateTranslation(new Vector3(displayable.position.x, displayable.position.y, displayable.position.z));
      world *= getRotation();
      world *= CameraHolder.camera.getWorldMatrix();
      foreach (ModelMesh mesh in model.Meshes)
      {
        foreach (BasicEffect effect in mesh.Effects)
        {
          //effect.TextureEnabled = false;
          //effect.Texture = tempTexture;
          //effect.Texture = otherTexture;
          effect.EnableDefaultLighting();
          effect.World = transformations[mesh.ParentBone.Index] * world;
          effect.View = CameraHolder.camera.getView();
          effect.Projection = CameraHolder.camera.getProjection();
          effect.Alpha = 1.0f;
        }

        mesh.Draw();
      }
      
      base.Draw(gameTime);
    }

    public bool isColliding(Point p)
    {
      if(p.x > left && p.x <right){
        return true;
      }
      return false;
    }

  }
}
