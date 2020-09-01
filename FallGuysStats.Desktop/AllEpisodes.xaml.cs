using FallGuyStats.Objects.Entities;
using FallGuyStats.Tools;
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

namespace FallGuysStats.Desktop
{
    /// <summary>
    /// Interaction logic for AllEpisodes.xaml
    /// </summary>
    public partial class AllEpisodesWindow : Window
    {
        private List<EpisodeEntity> Episodes
        { get; set; }

        public AllEpisodesWindow()
        {
            InitializeComponent();
            Episodes = LogParser.GetEpisodesFromLog();

        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
