using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling.App;

namespace Bowling.Tests
{
  [TestFixture]
  public class TestGroup
  {
    private Game _game;

    [SetUp]
    public void SetUp()
    {
      _game = new Game();
    }

    [Test]
    public void TestGutterGame()
    {
      for (int i = 0; i < _game.NrTriesInGame; i++)
      {
        _game.Roll(0);
      }

      _game.Score.Should().Be(0);
    }

    [Test]
    public void TestAllOnes()
    {
      for (int i = 0; i < _game.NrTriesInGame; i++)
      {
        _game.Roll(1);
      }

      _game.Score.Should().Be(20);
    }

    [Test]
    public void TestOneSpare()
    {
      int[] sequence = new int[20] { 1, 9, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
      foreach (var pins in sequence)
      {
        _game.Roll(pins);
      }

      _game.Score.Should().Be(11);
    }

    [Test]
    public void TestACompleteGame()
    {
      int i = 0;
      int[] sequence = new int[20] { 1, 9, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

      while (!_game.GameIsComplete)
      {
        _game.Roll(sequence[i]);
        i++;

        //if (_game.GetLastCompletedFrame().Number == 10)
      }

      _game.Score.Should().Be(12);
    }
  }
}
