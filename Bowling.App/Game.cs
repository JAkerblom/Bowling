using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.App
{
  public class Game
  {
    private int _nrOfFrames = 10;
    private LinkedList<Frame> _frames;
    private Frame currentFrame;

    public int NrTriesInGame { get; private set; }    
    public bool GameIsComplete { get; private set; }

    public Game()
    {
      NrTriesInGame = 20;
      GameIsComplete = false;

      _frames = new LinkedList<Frame>();
      _frames.AddFirst(new Frame(1));
      for (int i = 2; i < _nrOfFrames; i++)
        _frames.AddAfter(_frames.First, new Frame(i));

      _frames.AddLast(new Frame(10) { Next = _frames.First() });
      currentFrame = _frames.First();
    }

    /// <summary>
    ///   Roll is called each time the player rolls a ball. 
    /// </summary>
    /// <param name="pins">
    ///   The argument pins is the number of pins knocked down.
    /// </param>
    public void Roll(int pins)
    {
      currentFrame.AddRoll(pins);

      if (currentFrame.Number < 10)
        if (currentFrame.IsDone) currentFrame = currentFrame.Next;
      else
        if ( (currentFrame.TurnNr == 2 && currentFrame.IsComplete())
          || (currentFrame.TurnNr == 3))
          GameIsComplete = true;
    }

    /// <summary>
    ///   Score is called only at the very end of the game.
    /// </summary>
    /// <returns>
    ///   It returns the total score for that game
    /// </returns>
    public int Score
    {
      get
      {
        return this.GetLastCompletedFrame().TotalFrameScore;
      }
    }

    private Frame GetLastCompletedFrame()
    {
      Frame tmp = _frames.Last();
      bool found = false;
      while (!found)
      {
        if (tmp.IsComplete()) return tmp;

        tmp = tmp.Previous;
      }

      return tmp;
    }
  }
}

//private void CalculateFrameScoresUntil(Frame frame)
//private void CalculateFrameScores()
//{
//  Frame tmp = _frames.First();
//  int lf = GetLastCompletedFrameNumber();
//  //for (int i = 1; i <= frame.Number; i++)
//  int i = 0;
//  while (Turns[i])
//  {
//    tmp.CalculateFrameScore();
//    tmp = tmp.Next;

//  }
//}

//private int GetLastCompletedFrameNumber()
//{
//  int last = (currentFrame.TurnNr);
//  return 0;
//}


//_frames = new List<Frame>(nrOfFrames);
//Frame f = new Frame(1);
//for (int i = 1; i <= nrOfFrames; i++)
//{
//  if (i == 1)
//  {
//    Frame tmp = new Frame(2);
//    tmp.Previous = f;
//    f.Next = tmp;
//  }
//  else
//  {
//  }
//}


//if (currentFrame.IsFirstThrow())
//{
//  currentFrame.Rolls[0] = pins;
//  if (currentFrame.HasStrike)
//  {
//    this.CalculateFrameScoresUntil(currentFrame);
//    currentFrame = currentFrame.Next;
//  }
//}
//else
//{
//  currentFrame.Rolls[1] = pins;
//  this.CalculateFrameScoresUntil(currentFrame);
//}


//if (currentFrame.IsFirstThrow)
//{
//  if (pins == 10 && currentFrame.Number != 10)
//  {
//    currentFrame = currentFrame.Next;
//  }

//  //currentFrame.IsFirstThrow = false;
//}


//Score += pins;
//Turns[] = true;

//int turnNr = currentFrame.GetTurnNr();
//currentFrame.Rolls[turnNr] = pins;

//currentFrame.AddRoll(pins);

//if (currentFrame.IsDone || currentFrame.CanClosePrevious())
//{
//  this.CalculateFrameScoresUntil(currentFrame);
//  currentFrame = currentFrame.Next;
//}

// If frame 1 to 9
//if (currentFrame.Number < 10)
//{
//currentFrame.Rolls[turnNr] = pins;
//this.CalculateFrameScoresUntil(currentFrame);
//}
//else // Is last frame
//{
//  if (true)
//  {

//  }
//}