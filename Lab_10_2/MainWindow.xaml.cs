using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Lab_10_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<int> list;
        private string f, h, g;
        public MainWindow()
        {
            InitializeComponent();
            list = new List<int>();
        }

        private void OpenItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog=new OpenFileDialog();
            openFileDialog.Filter = "Text files(*.txt)|*.txt";
            openFileDialog.Title = "Открытие файла";
            if (openFileDialog.ShowDialog() == false) return;
            f=openFileDialog.FileName;
            ReadAsync(openFileDialog.FileName);
        }
        private async void ReadAsync(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    list.Add(int.Parse(line));
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            h = Environment.CurrentDirectory + "\\h.txt";
            FileInfo file = new FileInfo(h);
            if(!file.Exists) file.Create();
            g = Environment.CurrentDirectory + "\\g.txt";
            file=new FileInfo(g);
            if(!file.Exists)file.Create();
            List<int> lh = list.Where(i=>i%2!=0).ToList();
            OrdAsync(h, g, lh);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<int> lh = list.Where(i => i % 2 == 0).ToList();
            OddAync(h, g, lh);
        }

        private async void OddAync(string h, string g, List<int> l)
        {
            using (StreamWriter writer = new StreamWriter(h, false))
            {
                foreach (int i in l)
                    await writer.WriteLineAsync(i.ToString());
            }
            using (StreamReader reader = new StreamReader(h))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    using (StreamWriter writer = new StreamWriter(g, true))
                    {
                        await writer.WriteLineAsync(line);
                    }
                }
            }
            FileInfo file = new FileInfo(h);
            file.Delete();
        }
        private async void OrdAsync(string h, string g,List<int> l)
        {
            using (StreamWriter writer = new StreamWriter(h, false))
            {
                foreach(int i in l)
                await writer.WriteLineAsync(i.ToString());
            }
            using (StreamReader reader = new StreamReader(h))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    using (StreamWriter writer = new StreamWriter(g, false))
                    {
                       await writer.WriteLineAsync(line);
                    }
                }
            }
        }
    }
}
