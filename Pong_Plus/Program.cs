using System;

namespace Pong_Plus
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                try
                {
                    game.Run();
                }
                catch { }
        }
    }
}
