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

      int i = 0;
      int[] sequence = new int[20] { 1, 9, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

      while (!game.GameIsComplete)
      {
        game.Roll(sequence[i]);
        i++;
      }
    }
  }
}
