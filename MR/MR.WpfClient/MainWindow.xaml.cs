using MR.Service;
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
            var path = System.IO.Path.Combine(AppContext.BaseDirectory, "MemoResources", "midwaySystemDetails.docx");
            var wordReader = new WordMemoReader(path);
            MemoManager manager = new MemoManager(wordReader);
            var r = manager.Random();
            this.ShowTitle.Text = r?.Title;
            this.ShowMemo.Text = r?.Text;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            //判断用户的按键是否为Alt+F4
            if (e.KeyStates == Keyboard.GetKeyStates(Key.Space))
            {
                RandMemo();
            }
        }
    }
}