// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FallGuysStats.Desktop
{
    class LogMonitor
    {
        //TODO move to tools project
        public static async Task<string> ReadTail(string filename, CancellationToken token)
        {
            using (FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                while (!token.IsCancellationRequested)
                {
                    // Seek 1024 bytes from the end of the file
                    fs.Seek(-1024, SeekOrigin.End);
                    // read 1024 bytes
                    byte[] bytes = new byte[1024];
                    fs.Read(bytes, 0, 1024);
                    // Convert bytes to string
                    string s = Encoding.Default.GetString(bytes);
                    // or string s = Encoding.UTF8.GetString(bytes);
                    await Task.Delay(5000);
                    return s;
                }
                return "Monitoring stopped.";
            }
        }


        public static async Task<string> MonitorLog(string filename, CancellationToken token)
        {
            using (FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                await Task.Delay(5000);
                return "balh";
            }
        }


        /* public static async void ReadPlayerLog()
        {


            ReadTail(playerLogDataPath);
            await Task.Delay(10000);
        }*/
    }
}
