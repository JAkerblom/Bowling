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
    private Frame currentFrame;
    private Frame _last;

    public bool GameIsComplete { get; private set; }

    public Game()
    {
      GameIsComplete = false;

      Frame previous = new Frame(1);
      Frame next;
      currentFrame = previous;
      for (int i = 2; i <= _nrOfFrames; i++)
      {
        next = new Frame(i);
        next.Previous = previous;
        previous.Next = next;
        previous = next;
      }
      _last = previous;
      _last.Next = currentFrame;
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
      {
        if (currentFrame.IsDone)
          currentFrame = currentFrame.Next;
      }
      else
      {
        if ( (currentFrame.TurnNr == 2 && currentFrame.IsComplete())
          || (currentFrame.TurnNr == 3))
          GameIsComplete = true;
      }
    }

    /// <summary>
    ///   Score is mainly called only at the very end of the game.
    ///     Logic has been implemented so that it can show the current true score
    ///     throughout the game (including any chance of there being a strike or spare).
    /// </summary>
    /// <returns>
    ///   It can return the true total score for the game at any given state of rolls in the game as it progresses.
    /// </returns>
    public int Score
    {
      get
      {
        this.CalculateAllFrames();
        Frame lastCompleted = GetLastCompletedFrame();
        return (lastCompleted == null) ? 0 : lastCompleted.TotalFrameScore;
      }
    }

    /// <summary>
    ///   Start with the first frame and calculate their respective total frame score
    ///     If the frame is not complete the score will not be correct
    ///     but the GetLastCompletedFrame() will find the last element 
    ///     that has a correct score (i.e. spares or strikes have been 
    ///     fulfilled by next frames scores)
    /// </summary>
    private void CalculateAllFrames()
    {
      Frame tmp = _last.Next;
      for (int i = 1; i <= _nrOfFrames; i++)
      {
        tmp.CalculateFrameScore();
        tmp = tmp.Next;
      }
    }

    /// <summary>
    ///   Starts with the last frame and looks backwards. 
    ///     It looks to see if a frame is complete, i.e. the total frame score
    ///     can be calculated from next frames' scores according to the score rules of bowling
    /// </summary>
    /// <returns>Returns the last completed frame that has a calcuable score</returns>
    private Frame GetLastCompletedFrame()
    {
      Frame tmp = _last;
      bool found = false;
      while (!found)
      {
        if (tmp.IsComplete())
          return tmp;
        else if (tmp.Number == 1)
          return null;

        tmp = tmp.Previous;
      }

      return tmp;
    }
  }
}

//_frames = new LinkedList<Frame>();
//_frames.AddFirst(new Frame(1));
//for (int i = 2; i < _nrOfFrames; i++)
//  _frames.AddLast(new Frame(i));
//_frames.AddAfter(_frames.First, new Frame(i));

//_frames.AddLast(new Frame(10) { Next = _frames.First() });
//currentFrame = _frames.First();


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