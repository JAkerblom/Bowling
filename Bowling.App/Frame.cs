﻿using System;
using System.Linq;

namespace Bowling.App
{
  public class Frame
  {
    private int _frameNumber;
    private int _turn;
    private bool _thirdTurnAllowed;

    public Frame(int nr)
    {
      _frameNumber = nr;
      _thirdTurnAllowed = false;
      _turn = 0;

      TotalFrameScore = 0;
      Rolls = (nr == 10) ? new int[3] { 0, 0, 0 } : new int[2] { 0, 0 };
    }

    public Frame Next { get; internal set; }
    public Frame Previous { get; internal set; }
    public int Number { get { return _frameNumber; } }
    public int[] Rolls { get; set; }
    public int FirstRollScore { get; set; }
    public int SecondRollScore { get; set; }
    public int TotalFrameScore { get; private set; }
    public int LocalFrameScore { get { return Rolls.Sum(); } }
    public int TurnNr { get { return _turn; } }
    public bool IsDone { get { return _turn == 2; } } // Only applies to 1-9

    public bool FrameHasSpare
    {
      get
      {
        // Rolls sum to 10 through 2 rolls
        return ((Rolls[0] + Rolls[1]) == 10) && (Rolls[1] != 0);
      }
    }
    public bool FrameHasStrike
    {
      get
      {
        // If 1-9 frame
        //   -> Strike can only happen in first throw
        // Otherwise (which is not of interest since it 
        //  doesn't affect points in the same way as frame 1-9.
        if (_frameNumber < 10)
          return Rolls[0] == 10;
        else
          return Rolls[0] == 10 || Rolls[1] == 10 || Rolls[2] == 10;
      } 
    }

    /// <summary>
    ///   Adds the rolled pins of the current game to the frame
    ///   Also makes sure to keep track of what turn number is used in the frame
    ///   Furthermore it flags a third chance when in frame 10 depending on two rules
    ///     1. Either a strike is hit the first turn
    ///     2. Or a spare is hit in the second turn
    /// </summary>
    /// <param name="pins">The rolled number of pins from current throw in game</param>
    public void AddRoll(int pins)
    {
      Rolls[_turn] = pins;
      if (_turn == 0)
        FirstRollScore = pins;
      else if (_turn == 1)
        SecondRollScore = pins;

      _turn++;

      if (_frameNumber == 10)
      {
        if (_turn == 1)
        {
          if (Rolls[0] == 10)
            _thirdTurnAllowed = true;
        }
        else if (_turn == 2)
        {
          if (Rolls[0] + Rolls[1] == 10)
            _thirdTurnAllowed = true;
        }
      }
    }

    /// <summary>
    ///   Checks if a frame (1-9 or 10) is a complete frame.
    ///     That is - 
    ///       all turns have been used 
    ///       and a score is calcuable
    /// </summary>
    /// <returns>Returns whether or not a frame has a calcuable score</returns>
    /// <exception cref="_frameNumber=10">
    ///   The tenth frame is only complete if (three turns allowed) three rolls have been made
    ///   or (three turns not allowed) two rolls have been made
    /// </exception>
    /// <exception cref="this.FrameHasStrike">
    ///   If not in tenth frame and the frame has a strike
    ///     if the next frame also has a strike, 
    ///       this frame is only complete if the next frame after that has made one roll or more.
    ///     otherwise the frame is calcuable as long as the next frame is done. 
    /// </exception>
    public bool IsComplete()
    {
      if (_frameNumber == 10)
      {
        if (_thirdTurnAllowed)
          return _turn == 3;
        else
          return _turn == 2;
      }
      else
      {
        if (!this.FrameHasSpare && !this.FrameHasStrike)
          return _turn == 2;
        else if (this.FrameHasSpare)
          return Next.TurnNr > 0;
        else if (this.FrameHasStrike)
        {
          if (Next.FrameHasStrike)
            return Next.Next.TurnNr > 0;
          else
            return Next.TurnNr > 0;
        }
      }

      return false;
    }

    /// <summary>
    ///   This calculates the total score, i.e. base score
    ///    of the frame. Used in the board for showing results 
    ///    of each frame.
    /// </summary>
    /// <example>
    ///   If this frame has neither spare or strike
    ///    score is the score of current frame
    ///   
    ///   Else if it has a spare
    ///    score is this frame's score 
    ///    + the score of the next frame's first roll
    ///    
    ///   Else if this frame has a strike
    ///    score is this frame's score
    ///    + the next frame's score
    /// </example>
    /// <exception cref="_frameNumber=10">
    ///   The score of the tenth frame is always the sum of the frame's rolls
    /// </exception>
    /// <exception cref="_frameNumber=9">
    ///   If there is a strike in the ninth frame 
    ///   the point logic is always dependent on just the first and second throw of the tenth frame.
    /// </exception>
    public void CalculateFrameScore()
    {
      int baseScore = (Number == 1) ? 0 : Previous.TotalFrameScore;
      int score = 0;

      if (_frameNumber == 10)
        score = Rolls.Sum();
      else
      {
        if (!this.FrameHasSpare && !this.FrameHasStrike)
          score = LocalFrameScore;
        else if (this.FrameHasSpare)
          score = LocalFrameScore + Next.FirstRollScore;
        else if (this.FrameHasStrike)
        {
          if (_frameNumber == 9)
            score = LocalFrameScore + Next.FirstRollScore + Next.SecondRollScore;
          else
          {
            if (Next.FrameHasStrike)
              score = LocalFrameScore + Next.FirstRollScore + Next.Next.FirstRollScore;
            else
              score = LocalFrameScore + Next.LocalFrameScore;
          }
        }
      }

      TotalFrameScore = baseScore + score;
    }
  }
}

