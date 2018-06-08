using System.Windows.Controls;
using System.Windows.Input;
using Messager.Client;

namespace Messager
{
    public partial class Info : Page
    {
        public static Page GlobalInfo;
        private ServerWorks sw;
        public Info()
        {
            sw = new ServerWorks();
            InitializeComponent();
            BtnClose.Cursor = Cursors.Hand;
            BtnClose.MouseLeftButtonDown += Message.GlobalCanvas_MouseLeftButtonDown;
            GlobalInfo = this;
            UserC user = sw.GetUserById(Const.session, Const.IdFocus);
            Ellipse.Fill = Const.secondColor;
            NameSurname.Content = user.name + " " + user.surname;
            SubName.Content = (user.name[0] + "" + user.surname[0]).ToUpper();
            UserName.Content = user.userName;
            Phone.Content = user.phone;
            Email.Content = user.email;
        }
    }
}