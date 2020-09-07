// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FallGuysStats.Desktop
{
    class LogTimer
    { 
        public static void SetupTimer()
        {
            var autoEvent = new AutoResetEvent(false);
            //var logMonitor = new LogMonitor(true);

//            var stateTimer = new Timer(logMonitor.CheckFile, autoEvent, 1000, 7000);
        }
    }
}
