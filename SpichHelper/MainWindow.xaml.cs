using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Synthesis;
using Microsoft.Win32;
using System.IO;
using System.Web;
using System.Net.Http;
using HtmlAgilityPack;

namespace SpichHelper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string speek;
        public SpeechSynthesizer synth = new SpeechSynthesizer();
        public MainWindow()
        {
            InitializeComponent();
            Stop.Visibility = Visibility.Hidden;
        }

        public string GetPath()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "TXT|*.txt"; // Фильтр файлов в проводнике
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    return dlg.FileName;
                }
                return null;
            }
            catch (Exception F) { return null; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = GetPath();
                speek = File.ReadAllText(path);
                pole.Text = speek;
                Stop.Visibility = Visibility.Visible;
                synth.SetOutputToDefaultAudioDevice();
                synth.SpeakAsync(speek);
            }
            catch (Exception F) { return; }
        }

        private async void Speek_Play(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient request = new HttpClient();
                string page = await request.GetStringAsync("https://world-weather.ru/pogoda/russia/moscow/");
                HtmlDocument dok = new HtmlDocument();
                dok.LoadHtml(page);
                HtmlNodeCollection nodes = dok.DocumentNode.SelectNodes("//ul");
                int i = 0;
                //if (nodes != null)
                //    foreach (HtmlNode node in nodes)
                //    {
                //        i++;                                                  //щарим по всем тегам div
                //        pole.Text += node.InnerText + i.ToString();
                //    }
                speek = nodes[2].InnerText + " " + nodes[4].InnerText;
                pole.Text = nodes[2].InnerText + " " + nodes[4].InnerText;
                Stop.Visibility = Visibility.Visible;
                synth.SetOutputToDefaultAudioDevice();
                synth.SpeakAsync(speek);
            }
            catch (Exception E) { }
        }

        private void Speek_Close(object sender, RoutedEventArgs e)
        {
            synth.SpeakAsyncCancelAll();
           
            Stop.Visibility = Visibility.Hidden;
            Play.Visibility = Visibility.Visible;
        }
    }
}
