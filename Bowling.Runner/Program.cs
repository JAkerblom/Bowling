using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling.App;

namespace Bowling.Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      Game game = new Game();

      int[] sequence = new int[21] { 1, 4, 4, 5, 6, 4, 5, 5, 10, 0, 0, 1, 7, 3, 6, 4, 10, 0, 2, 8, 6 };

      foreach (var pins in sequence)
      {
        game.Roll(pins);
      }

      var res = game.Score;
      Console.WriteLine("Result is: " + res);
      Console.ReadLine();
    }
  }
}
