using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Messager
{
    public partial class MainWindow : Window
    {
        public static Frame GlobalMainFrame;
        public ServerWorks sw;
        public MainWindow()
        {
            Thread.Sleep(2500);
            sw = new ServerWorks();
            InitializeComponent();
            GlobalMainFrame = MainFrame;
        }
    }
}