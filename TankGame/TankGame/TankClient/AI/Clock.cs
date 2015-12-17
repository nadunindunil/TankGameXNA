﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankClient.AI
{
    class Clock
    {
        Stopwatch sw;
        public Clock()
        {
            sw = new Stopwatch();
            
        }

        public void startClock() 
        {
            sw.Start();
        }
        public void stopClock() 
        {
            sw.Stop();
        }

        public long getTime() 
        {
            return sw.ElapsedMilliseconds;
        }
    }
}
