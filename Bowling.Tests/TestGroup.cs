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
      int i = 0;
      var sequence = Enumerable.Repeat(0, 20);

      while (!_game.GameIsComplete)
      {
        _game.Roll(sequence.ElementAt(i));
        i++;
      }

      _game.Score.Should().Be(0);
    }

    [Test]
    public void TestAllOnes()
    {
      int i = 0;
      var sequence = Enumerable.Repeat(1, 20);

      while (!_game.GameIsComplete)
      {
        _game.Roll(sequence.ElementAt(i));
        i++;
      }

      _game.Score.Should().Be(20);
    }

    [Test]
    [TestCase(new int[3] { 1, 9, 1 }, 11)]
    [TestCase(new int[4] { 1, 9, 1, 0 }, 12)]
    public void TestOneSpare(int[] sequence, int expectedResult)
    {
      foreach (var pins in sequence)
      {
        _game.Roll(pins);
      }

      _game.Score.Should().Be(expectedResult);
    }

    [Test]
    [TestCase(new int[3] { 10, 0, 1 }, 11)]
    [TestCase(new int[4] { 10, 0, 1, 4 }, 20)]
    [TestCase(new int[4] { 10, 0, 10, 0 }, 0)]
    [TestCase(new int[6] { 10, 0, 10, 0, 10, 0 }, 30)]
    public void TestOneStrike(int[] sequence, int expectedResult)
    {
      foreach (var pins in sequence)
      {
        _game.Roll(pins);
      }

      _game.Score.Should().Be(expectedResult);
    }

    [Test]
    [TestCase(new int[20] { 1, 9, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0 }, 22)]
    [TestCase(new int[21] { 1, 9, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0 }, 22)]
    public void TestACompleteGame(int[] sequence, int expectedResult)
    {
      int i = 0;
      while (!_game.GameIsComplete)
      {
        _game.Roll(sequence[i]);
        i++;
      }

      _game.Score.Should().Be(expectedResult);
    }

    [Test]
    public void TestAPerfectGame()
    {
      int i = 0;
      int[] sequence = new int[21] { 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 10, 10 };

      while (!_game.GameIsComplete)
      {
        _game.Roll(sequence.ElementAt(i));
        i++;
      }

      _game.Score.Should().Be(300);
    }

    [Test]
    [TestCase(new int[20] { 1, 9, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 12)]
    [TestCase(new int[21] { 1, 4, 4, 5, 6, 4, 5, 5, 10, 0, 0, 1, 7, 3, 6, 4, 10, 0, 2, 8, 6 }, 133)]
    public void TestSequence(int[] sequence, int expectedResult)
    {
      int i = 0;

      while(!_game.GameIsComplete)
      {
        _game.Roll(sequence[i]);
        i++;
      }

      _game.Score.Should().Be(expectedResult);
    }
  }
}
