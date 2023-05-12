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
                dlg.Filter = "Xaml|*.xaml|TXT|*.txt"; // Фильтр файлов в проводнике
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
            string path = GetPath();
            speek = File.ReadAllText(path);
        }

        private void Speek_Play(object sender, RoutedEventArgs e)
        {
            try
            {
                Stop.Visibility = Visibility.Visible;
                synth.SetOutputToDefaultAudioDevice();
                synth.SpeakAsync(speek);
            }catch (Exception E) { }
        }

        private void Speek_Close(object sender, RoutedEventArgs e)
        {
            synth.SpeakAsyncCancelAll();
           
            Stop.Visibility = Visibility.Hidden;
            Play.Visibility = Visibility.Visible;
        }
    }
}
