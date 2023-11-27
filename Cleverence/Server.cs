using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Console;

namespace Cleverence
{
    public static class Server
    {
        private static int _count;

        public static int GetCount()
        {
            lock (new object())
            {
                Console.WriteLine(_count);
                return _count;
            }
        }

        public static void AddToCount(int value)
        {
            lock (new object ())
            {
                _count += value;
                Thread.Sleep(300000);
            }
        }
    }
}
