using System.Windows.Controls;

namespace Messager
{
    public partial class DialogWindow : Page
    {
        public static Frame GlobalFrame;
        public static Frame GlobalBodyFrame;
        public static Canvas GlobalCanvas;
        public static Border GlobalHelp;
        public static Frame GlobalOptions;
        public DialogWindow()
        {      
            InitializeComponent();
            GlobalFrame = MenuFrame;
            GlobalBodyFrame = BodyFrame;
            GlobalCanvas = Canvas;
            GlobalHelp = Help;
            GlobalOptions = Options;
        }
    }
}