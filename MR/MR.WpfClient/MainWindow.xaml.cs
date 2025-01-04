using MR.Service;
using NPOI.Util;
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
        private MemoManager manager;
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
            try
            {
                var split = this.levelRange.Text.Split([',','，']).Select(tt => int.Parse(tt.Trim())).ToArray();
                if (split.Length != 2) throw new Exception();
                var r = manager.Random(split[0], split[1]);
                this.ShowTitle.Text = r?.Title;
                this.ShowMemo.Text = r?.Text;
            }
            catch (Exception)
            {
                var r = manager.Random();
                this.ShowTitle.Text = r?.Title;
                this.ShowMemo.Text = r?.Text;
            }
           
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            //判断用户的按键是否为Alt+F4
            //if (e.KeyStates == Keyboard.GetKeyStates(Key.Space))
            //{
            //    RandMemo();
            //}
        }

        private readonly string[] onedrivePath = [
                "D:\\OneDrive\\Documents\\CompanyWork\\系统机制细节解读.docx" ,
                //"D:\\OneDrive\\Documents\\CompanyWork\\csTerms.docx"
            ];
        private string cachePath = System.IO.Path.Combine(AppContext.BaseDirectory, "MemoResources");
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearCache();
            var cc = System.IO.Path.Combine(cachePath, Guid.NewGuid().ToString() + ".docx");
            //var path = System.IO.Path.Combine(AppContext.BaseDirectory, "MemoResources", "midwaySystemDetails.docx");
            File.Copy(TempConfig.GetInstance().FilesSources.Last(), cc);
            this.Load(cc);
        }
        private void Load(string path)
        {
            var wordReader = new WordMemoReader(path);
            this.manager = new MemoManager(wordReader);
            this.ShowContent.Text = string.Join("\n", this.manager.GetOverView());
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ClearCache();
            var cc = System.IO.Path.Combine(cachePath, Guid.NewGuid().ToString() + ".docx");
            //var path = System.IO.Path.Combine(AppContext.BaseDirectory, "MemoResources", "midwaySystemDetails.docx");
            File.Copy(TempConfig.GetInstance().FilesSources.Last(), cc);
            this.Load(cc);
        }
        private void ClearCache()
        {
            Directory.Delete(cachePath, true);
            Directory.CreateDirectory(cachePath);
        }
    }
}