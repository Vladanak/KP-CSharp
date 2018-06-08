using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Messager.Client;

namespace Messager
{
    public partial class Contacs : Page
    {
        private ServerWorks sw;
        public Contacs()
        {
            sw= new ServerWorks();
            InitializeComponent();
            tBox.TextChanged += tBox_TextChanged;
            BtnClose.Cursor = Cursors.Hand;
            BtnClose.MouseLeftButtonDown += Message.GlobalCanvas_MouseLeftButtonDown;
        }

        private void tBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tBox.Text != "")
            {
                StackPanel.Children.Clear();
                List<UserC> list = sw.GetSearchResult(Const.session, tBox.Text);
                if (list!=null)
                {
                    foreach (var l in list)
                    {
                        CustomButton bt = new CustomButton {Cursor = Cursors.Hand};
                        bt.Click += Bt_MouseLeftButtonDown;
                        bt.IdUser = sw.GetIdByUserName(Const.session, l.userName);
                        bt.Template = (ControlTemplate) this.TryFindResource("Bt1Template");
                        bt.TextMessage = "online";
                        bt.TextName = l.name + " " + l.surname;
                        bt.TextSubname = ("" + l.name[0] + l.surname[0]).ToUpper();
                        StackPanel.Children.Add(bt);
                    }
                }
                Change.Visibility = Visibility.Hidden;
            }
            else
            {
                StackPanel.Children.Clear();
                Change.Visibility = Visibility.Visible;
            }
        }

        private void Bt_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            sw.AddToContact(Const.session,((CustomButton)sender).IdUser);
            DialogWindow.GlobalCanvas.Visibility = Visibility.Hidden;
            DialogWindow.GlobalHelp.Visibility = Visibility.Hidden;
        }
    }
}