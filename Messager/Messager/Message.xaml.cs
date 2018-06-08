using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Messager.Client;

namespace Messager
{
    public partial class Message : Page
    {
        private readonly ServerWorks sw = new ServerWorks();
        public static StackPanel GStackPanel;
        public static ScrollViewer GsViewer;
        private readonly CustomMessage cm = new CustomMessage(); 
        public Message()
        {
            InitializeComponent();      
            Inform.Visibility= Visibility.Hidden;
            MessageText.Visibility = Visibility.Hidden;
            Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "icons/page.jpg")));
            Const.mess = this;     
            Box.TextChanged+= Box_TextChanged;
            Box.KeyDown += Box_KeyDown;
            Send.Visibility = Visibility.Hidden;
            Send.MouseLeftButtonDown += Send_MouseLeftButtonDown;
            Send.Cursor = Cursors.Hand;
            Inform.Cursor = Cursors.Hand;
            Inform.MouseLeftButtonDown += InformMouseLeftButtonDown;
            GStackPanel = MessagePanel;
            GsViewer = Scroll;
            Thread t = new Thread(GonewMessage) {IsBackground = true};
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void Box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (Box.Text.Equals("")) return;
            UserC user = sw.GetUserById(Const.session, Const.IdFocus);
            sw.SendMessage(Const.session, user.userName, Box.Text);
            Box.Text = "";
        }

        public  void GonewMessage()
        {
            while (true)
            {
            Thread.Sleep(2000);
                if (Const.IdFocus != -1)
                {
                    sw.UpdateNewMessage(Const.session, Const.IdFocus);
                    
                    Dispatcher.BeginInvoke(new ThreadStart(delegate
                    {
                        if (sw.GetLastIdMessage(Const.session,Const.IdFocus)>Const.LastId)
                        {
                            cm.SetData();
                        }
                    }
                        ));
                }
            }
        }

        private static void InformMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DialogWindow.GlobalCanvas.Visibility = Visibility.Visible;
            DialogWindow.GlobalHelp.Visibility = Visibility.Visible;
            DialogWindow.GlobalOptions.Source = new Uri("Info.xaml",UriKind.Relative);
            DialogWindow.GlobalCanvas.MouseLeftButtonDown += GlobalCanvas_MouseLeftButtonDown;
        }

        public static void GlobalCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DialogWindow.GlobalCanvas.Visibility = Visibility.Hidden;
            DialogWindow.GlobalHelp.Visibility = Visibility.Hidden;
            DialogWindow.GlobalCanvas.MouseLeftButtonDown -= GlobalCanvas_MouseLeftButtonDown;
        }

        private void Send_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserC user = sw.GetUserById(Const.session, Const.IdFocus);
            sw.SendMessage(Const.session, user.userName, Box.Text);
            Box.Text = "";
        }

        private void Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Box.Text != "")
            {
                Send.Visibility = Visibility.Visible;
                lb.Visibility=Visibility.Hidden;
            }
            else
            {
                Send.Visibility = Visibility.Hidden;
                lb.Visibility = Visibility.Visible;
            }
        }

        public void SetData(UserC user)
        {
            Alert.Visibility = Visibility.Hidden;
            Inform.Visibility = Visibility.Visible;
            MessageText.Visibility = Visibility.Visible;
            NameSurname.Content = user.name + " " + user.surname;
        }
    }
}