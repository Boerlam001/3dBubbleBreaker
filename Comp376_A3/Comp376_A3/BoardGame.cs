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
  class BoardGame : Board
  {
    BoardPlayer playerBoard;
    BoardMain bubbleBoard;

    bool gameOver{get{return (BubbleHolder.checkGameStatus()==0 || BubbleHolder.checkGameStatus()==1);}}
    int CurrentLevel;
    public BoardGame(Game game, int lvl)
      : base(game)
    {
      bubbleBoard = new BoardMain(game, lvl);
      playerBoard = new BoardPlayer(game);
    }

    public void changeAngle(int direction)
    {
      playerBoard.changeAngle(direction);
    }
    public void shoot()
    {
      if(playerBoard.isReady)
      {
        playerBoard.shoot();
      }
    }

    public override void Update(GameTime gameTime)
    {
      if (!gameOver)
      {
        playerBoard.Update(gameTime);
        bubbleBoard.Update(gameTime);
        base.Update(gameTime);
      }
    }

    public string getStatus()
    {
      int gameStatus = BubbleHolder.checkGameStatus();
      string toRet;
      if (gameStatus == 0)
      {
        //Console.WriteLine("You have won the game");//win
        toRet = ("You have won the game! with score:" + BubbleHolder.score);
        if (CurrentLevel == 1)
        {
          //changeLevel();
        }
        else
        {
          //Game.Exit();
        }
      }
      else if (gameStatus == 1)
      {
        //Console.WriteLine("You have lost the game!!");//lose
        toRet = "You have lost the game!"+ BubbleHolder.score;
      }
      else
      {
        toRet = "score: "+ BubbleHolder.score;
      }
      return toRet;
    }

    public override void Draw(GameTime gameTime)
    {
      //GraphicsDevice.BlendState = BlendState.Opaque;
      //GraphicsDevice.DepthStencilState = DepthStencilState.Default;
      //GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

      bubbleBoard.Draw(gameTime);
      playerBoard.Draw(gameTime);
      base.Draw(gameTime);
    }
  }
}
