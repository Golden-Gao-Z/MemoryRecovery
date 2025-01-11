using MR.Model;
using MR.Service;
using NPOI.Util;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MR.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SuperMemoManager manager;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RandMemo();
        }
        private void RandMemo()
        {
            var split = this.levelRange.Text.Split([',', '，'])
                                            .Select(tt => tt.Trim())
                                            .Where(tt => int.TryParse(tt, out int _))
                                            .Select(tt => int.Parse(tt))
                                            .ToArray();
            SingleMemo sm;
            if (split.Length == 2)
                sm = manager.Random(split[0], split[1]);
            else
                sm = manager.Random();

            this.ShowTitle.Text = sm?.Title;
            this.ShowMemo.Text = sm?.Text;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key is not Key.Space) return;

            RandMemo();
        }

        private string cachePath = System.IO.Path.Combine(AppContext.BaseDirectory, "MemoResources");
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ReloadAllFiles();
        }
        private void ReloadAllFiles()
        {
            ClearCache();
            var files = new List<string>();
            foreach (var item in TempConfig.GetInstance().FilesSources)
            {
                if (!File.Exists(item))
                {
                    Debug.WriteLine($"No such file: {item}");
                    continue;
                }
                var cc = System.IO.Path.Combine(cachePath, Guid.NewGuid().ToString() + ".docx");
                File.Copy(item, cc);
                files.Add(cc);
            }

            this.Load(files.ToArray());
        }
        private void Load(params string[] paths)
        {
            this.manager = new SuperMemoManager();
            foreach (var path in paths)
            {
                var wordReader = new WordMemoReader(path);
                var manager = new MemoManager(wordReader);
                this.manager.LoadManager(manager);
            }


            this.ShowContent.Text = string.Join(Environment.NewLine, this.manager.GetOverView());
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.ReloadAllFiles();
        }
        private void ClearCache()
        {
            Directory.Delete(cachePath, true);
            Directory.CreateDirectory(cachePath);
        }
    }
}