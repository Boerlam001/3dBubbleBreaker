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
  class Arrow : Drawable
  {
    //Texture2D texture;
     public Arrow(Game g)
      :base(g, 0, 0, new ModelArrow())
    {
      //texture = t;
      //origin = new Vector2(section.Width / 2, section.Height);
      world = Matrix.CreateTranslation(new Vector3(displayable.position.x, displayable.position.y, displayable.position.z));
    }

     public Arrow(Game g, Point p, Angle a)
       : base(g, 0, 0, new ModelArrow(a,p))
     {
       //texture = t;
       //origin = new Vector2(section.Width / 2, section.Height);
       world = Matrix.CreateTranslation(new Vector3(displayable.position.x, displayable.position.y, displayable.position.z));
     }

     public Arrow(Game g, Point p, Angle a, Angle z)
       : base(g, 0, 0, new ModelArrow(a, z, p))
     {
       //texture = t;
       //origin = new Vector2(section.Width / 2, section.Height);
       world = Matrix.CreateTranslation(new Vector3(displayable.position.x, displayable.position.y, displayable.position.z));
     }
     protected override Model LoadTexture()
     {
       Model toLoad;
       toLoad = Game.Content.Load<Model>("Models\\ArrowFull");
       //arrowTip = Game.Content.Load<Model>("Models\\ArrowTip");
       return toLoad;
     }

     public override void Draw(GameTime gameTime)
     {

       world = Matrix.CreateRotationX(MathHelper.ToRadians(getSloap().degrees));
       world *= Matrix.CreateTranslation(new Vector3(displayable.position.x, displayable.position.y-1 + ModelBubble.HEIGHT, displayable.position.z));
       world *= Matrix.CreateRotationZ(-MathHelper.ToRadians(getAngle().degrees));
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
     }

    public Angle getAngle()
    {
      return ((ModelArrow)displayable).angle;
    }

    public void setAngle(Angle angle)
    {
      ((ModelArrow)displayable).angle = angle;
    }

    public Angle getSloap()
    {
      return ((ModelArrow)displayable).angleZ;
    }
    public void setSloap(Angle angle)
    {
      ((ModelArrow)displayable).angleZ = angle;
    }
  }
}
