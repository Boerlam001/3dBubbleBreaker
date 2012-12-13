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
  class Bubble : Drawable
  { 
    //x and y locations of all the items in the image file
    private static int[] locationX = { 0, 22, 44, 66, 88, 110, 132, 154 };
    private static int[] locationY = { 0, 0, 0, 0, 0, 0, 0, 0 };

    public bool isPopped;
    public bool isDropped;
    public Matrix currentRotation;
    public bool isConnected;
    public float currentTime = 0;
    public float interval = 100;
    public bool isFreeFalling = false;
    public float gravity = 0.0025f;
    public Bubble(Game g, BubbleColor c)
      :base(g, locationX[(int)c], locationY[(int)c], new ModelBubble(c))
    {
      isPopped = false;
      isDropped = false;
      isConnected = false;
      currentRotation = Matrix.Identity;
    }
    public Bubble(Game g, BubbleColor c, Point p)
      :base(g, locationX[(int)c], locationY[(int)c], new ModelBubble(c, p))
    {
      isPopped = false;
      isDropped = false;
      isConnected = false;
      currentRotation = Matrix.Identity;
    }

    public Bubble(Bubble b)
      : base(b, new ModelBubble((ModelBubble)b.displayable))
    {
      isPopped = false;
      isDropped = false;
      isConnected = false;
      currentRotation = Matrix.Identity;
    }
    protected override Model LoadTexture()
    {
      Model toLoad = Game.Content.Load<Model>("Models\\"+getFileName());
      return toLoad;
    }

    protected string getFileName()
    {
      ModelBubble bubble = (ModelBubble) displayable;
      switch (bubble.color)
      {
        case BubbleColor.red:
          return "RedBubble";
        case BubbleColor.blue:
          return "BlueBubble";
        case BubbleColor.brown:
          return "BrownBubble";
        case BubbleColor.green:
          return "GreenBubble";
        case BubbleColor.orange:
          return "OrangeBubble";
        case BubbleColor.teal:
          return "TealBubble";
        case BubbleColor.yellow:
          return "YellowBubble";
      }
      return "ball";
    }

    public BubbleColor getColor()
    {
      return ((ModelBubble) displayable).color;
    }

    public Matrix getBallRotation()
    {
      ModelBubble bubble = (ModelBubble) displayable;

      Matrix rotations = Matrix.CreateRotationX(bubble.motionRotation.x);
      //rotations *= Matrix.CreateRotationY(bubble.motionRotation.y);
      rotations *= Matrix.CreateRotationZ(bubble.motionRotation.y);
      return rotations;
    }
    public void applyGravity()
    {
      isFreeFalling = true;
    }
    public override void Update(GameTime gameTime)
    {
      currentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
      if (currentTime > interval)
      {
        if (isFreeFalling)
        {
          Velocity tempVelocity = getVelocity();
          tempVelocity.y -= gravity;
          tempVelocity.z += gravity/4;
          setVelocity(tempVelocity);
        }
        currentTime = 0f;
      }
      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      ModelBubble bubble = (ModelBubble) displayable;
      world = getBallRotation();
      world *= Matrix.CreateTranslation(new Vector3(displayable.position.x, displayable.position.y, displayable.position.z));
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
    public override bool Equals(Object obj)
    {
      Bubble p = (Bubble)obj;
      return (getPosition().Equals(p.getPosition()));
    }

    public void popBubble()
    {
      changeColor(BubbleColor.popped);
      model = Game.Content.Load<Model>("Models\\" + getFileName());
      isPopped = true;
    }
    public void dropBubble()
    {
      //changeColor(BubbleColor.popped);
      isDropped = true;
      //setVelocity(new Velocity(0, -0.1f, 0));
      applyGravity();
    }
    public void changeColor(BubbleColor color)
    {
      ModelBubble b = (ModelBubble)this.displayable;
      b.setColor(color);
      section = new Rectangle(locationX[(int)color], locationY[(int)color], dimensions.width, dimensions.height);
    }
    public override void stop()
    {
      isFreeFalling = false;
      base.stop();
    }
  }
}
