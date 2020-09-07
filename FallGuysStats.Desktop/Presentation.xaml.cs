// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using FallGuyStats.Tools;
using System.Linq;
using System.Threading;

namespace FallGuysStats.Desktop
{
    /// <summary>
    /// Interaction logic for Presentation.xaml
    /// </summary>
    public partial class Presentation : Window
    {
        public Presentation()
        {
            InitializeComponent();

            string currentUserAppData = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string playerLogFolder = currentUserAppData + "Low\\Mediatonic\\FallGuys_client\\";
            PlayerLogFile = playerLogFolder + "player.log";

            ReadLog();

            fsWatch = new FileSystemWatcher();
            fsWatch.Path = playerLogFolder;
            fsWatch.Filter = "Player.log";
            fsWatch.NotifyFilter = NotifyFilters.LastWrite;
            fsWatch.Changed += FsWatch_Changed;
            fsWatch.Created += FsWatch_Created;
            fsWatch.Deleted += FsWatch_Deleted;
            fsWatch.Renamed += FsWatch_Renamed;
            fsWatch.EnableRaisingEvents = true;
        }

        private void FsWatch_Renamed(object sender, RenamedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FsWatch_Deleted(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FsWatch_Created(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FsWatch_Changed(object sender, FileSystemEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((o) => ProcessFile(e));
        }

        private void ProcessFile(FileSystemEventArgs e)
        {
            var fs = new FileStream(PlayerLogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(fs))
            {
                if (sr.BaseStream.Length > FileLength + 1024 * 5)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        boxPrimary.Text = "";
                    });
                    List<string> logData = LogParser.ReadLogData();
                    logData.Reverse();
                    string textboxText = "";

                    foreach (string logLine in logData)
                    {
                        textboxText += $"{logLine}\r\n";
                    }
                    this.Dispatcher.Invoke(() =>
                    {
                        boxPrimary.Text = textboxText;
                    });
                    FileLength = (int)sr.BaseStream.Length;
                }

                else
                {

                }
            }
        }

        private static string PlayerLogFile
        { get; set; }

        private FileSystemWatcher fsWatch;

        private static int FileLength
        { get; set; }

        public static void ReadLog()
        {
            var fs = new FileStream(PlayerLogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(fs);
            FileLength = (int)sr.BaseStream.Length;
        }
    }
}
