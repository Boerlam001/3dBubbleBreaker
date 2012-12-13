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
  class BoardPlayer : Board
  {
    const float angleIncrement = 0.05f;
    const float velocityIncrement = 0.2f;
    const float angleLimit = 45;
    float zSloap;//-35;
    int numberOfShots;
    public bool isReady;
    const float intervalToShoot=500;
    float currentShootingTime;
    Game game;
    Point[] locations;
    Point boardPosition;
    Bubble[] bubbles;
    Angle currentAngle;

    Arrow arrow;

    public BoardPlayer(Game g)
      : base(g)
    {
      currentShootingTime = 0;
      numberOfShots = 0;
      isReady = true;
      game = g;
      locations = new Point[3];
      boardPosition = new Point(0,0,7.5f);
      bubbles = new Bubble[3];
      currentAngle = new Angle(0);
      zSloap = -MathHelper.ToDegrees((float)Math.Atan((float)(BoardMain.verteces[5] - BoardMain.verteces[4]) / (float)(BoardMain.verteces[3] - BoardMain.verteces[2])));
      Initialize();
    }

    public override void Initialize()
    {
      locations[0] = new Point(boardPosition.x, boardPosition.y, boardPosition.z);
      locations[1] = new Point(boardPosition.x - 4, boardPosition.y, boardPosition.z);
      locations[2] = new Point(boardPosition.x - 8, boardPosition.y, boardPosition.z);

      bubbles[0] = BubbleCreator.createRandomBubble(locations[0]);
      childComponents.Add(bubbles[0]);

      bubbles[1] = BubbleCreator.createRandomBubble(locations[1]);
      childComponents.Add(bubbles[1]);

      bubbles[2] = BubbleCreator.createRandomBubble(locations[2]);
      childComponents.Add(bubbles[2]);

      arrow = new Arrow(Game, new Point(locations[0].x, locations[0].y, boardPosition.z), new Angle(0), new Angle(zSloap));
      childComponents.Add(arrow);
      base.Initialize();
      
    }

    public override void Update(GameTime gameTime)
    {
      currentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
      if (currentTime > interval)
      {
        currentShootingTime++;
        if(currentShootingTime>intervalToShoot)
        {
          shoot();
        }
        childComponents = new List<GameComponent>();
        childComponents.Add(arrow);
        childComponents.Add(bubbles[0]);
        childComponents.Add(bubbles[1]);
        childComponents.Add(bubbles[2]);

        currentTime = 0f;
        base.Update(gameTime);
        checkCollisions();
      }
    }

    public override void Draw(GameTime gameTime)
    {
      
      base.Draw(gameTime);
    }

    public void changeAngle(int direction)
    {
      currentAngle.degrees += Angle.toDegrees(angleIncrement * direction);
      if (currentAngle.degrees > angleLimit)
      {
        currentAngle.degrees = angleLimit;
      }
      else if (currentAngle.degrees < -angleLimit)
      {
        currentAngle.degrees = -angleLimit;
      }
      arrow.setAngle(currentAngle);
    }

    public void setAngle(float a)
    {
      currentAngle.degrees = Angle.toDegrees(a);
      if (currentAngle.degrees > angleLimit)
      {
        currentAngle.degrees = angleLimit;
      }
      else if (currentAngle.degrees < -angleLimit)
      {
        currentAngle.degrees = -angleLimit;
      }
      arrow.setAngle(currentAngle);
    }
    public void setAngle(float x, float y)
    {
      float angle = (y - arrow.getPosition().y)/(x-arrow.getPosition().x);
      angle = (float)Math.Atan(angle);
      currentAngle.degrees = 90- Angle.toDegrees(angle);
      if (currentAngle.degrees > angleLimit)
      {
        currentAngle.degrees = angleLimit;
      }
      else if (currentAngle.degrees < -angleLimit)
      {
        currentAngle.degrees = -angleLimit;
      }
      arrow.setAngle(currentAngle);
    }

    public void shoot()
    {
      isReady = false;
      bubbles[0].setVelocity(currentAngle.getVelocity(velocityIncrement, zSloap + Math.Abs(currentAngle.degrees/7.5f)));
      bubbles[0].applyGravity();
      currentShootingTime = 0;
      numberOfShots++;
    }

    public void checkCollisions()
    {
      if (BubbleHolder.checkColliding(bubbles[0]))
      {
        bubbles[0].stop();
        BubbleHolder.snapToGrid(bubbles[0]);
        changeBubbles();
        isReady = true;
        if (numberOfShots >= 3)
        {
          BubbleHolder.MoveCeilingDown();
          numberOfShots = 0;
        }
      }
      if(BoardMain.checkWallCollision(bubbles[0])){
        Velocity temp = bubbles[0].getVelocity();
        bubbles[0].setVelocity(new Velocity(-temp.x, temp.y, temp.z));
      }
      if (BoardMain.checkReachedLimit(bubbles[0]))
      {
        bubbles[0].stop();
        BubbleHolder.snapToGrid(bubbles[0]);
        //bubbles[0].popBubble();
        changeBubbles();
      }
    }

    public void changeBubbles()
    {
      bubbles[1].moveTo(locations[0], interval);
      bubbles[2].moveTo(locations[1], interval);

      bubbles[0] = bubbles[1];
      bubbles[1] = bubbles[2];

      bubbles[2] = BubbleCreator.createRandomBubble(locations[2]);
      childComponents.Add(bubbles[2]);
    }
  }
}
