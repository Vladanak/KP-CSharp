using System.Windows.Controls;
using System.Windows.Input;

namespace Messager
{
    public partial class Autor : Page
    {
        public Autor()
        {
            InitializeComponent();
            BtnClose.Cursor = Cursors.Hand;
            BtnClose.MouseLeftButtonDown += Message.GlobalCanvas_MouseLeftButtonDown;
        }
    }
}