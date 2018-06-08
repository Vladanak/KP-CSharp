using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Messager.Client;
using Brushes = System.Windows.Media.Brushes;

namespace Messager
{
    public partial class Menu : Page
    {
        private ServerWorks sw = new ServerWorks();
        public static Page GMenu;
        public Menu()
        {
            UserC user = (UserC)Unpackage<UserC>.Upackage(sw.GetMyData(Const.session));
            InitializeComponent();
            tName.Content = user.name+" "+user.surname;
            tPhone.Content = user.phone;
            tEmai.Content = user.email;
            elipse.Fill = Brushes.BlueViolet;
            tSubname.Content = ("" + user.name[0] + user.surname[0]).ToUpper();
            GMenu = this;
            LAutor.Cursor = Cursors.Hand;
            Contacts.Cursor = Cursors.Hand;
            Settings.Cursor = Cursors.Hand;
            Exit.Cursor = Cursors.Hand;
            Contacts.MouseEnter += Contacts_MouseEnter;
            Contacts.MouseLeave += Contacts_MouseLeave;
            Settings.MouseEnter += Contacts_MouseEnter;
            Settings.MouseLeave += Contacts_MouseLeave;
            Exit.MouseEnter += Contacts_MouseEnter;
            Exit.MouseLeave += Contacts_MouseLeave;
            Contacts.MouseLeftButtonDown += InformMouseLeftButtonDown;
            LAutor.MouseLeftButtonDown += Settings_MouseLeftButtonDown;
            Exit.MouseLeftButtonDown += Exit_MouseLeftButtonDown;
            Settings.MouseLeftButtonDown += Origin_MouseLeftButtonDown;
        }

        private static void Exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ServerWorks server = new ServerWorks();
            UserC user = (UserC)Unpackage<UserC>.Upackage(server.GetMyData(Const.session));
            using (FileStream fstream = File.Open(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "session.txt"),FileMode.Create))
            { }
            server.RemoveHach(user.userName);
            MessageBox.Show("Всего хорошего!");
            Environment.Exit(0);
        }

        private static void Origin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Panel.SetZIndex(DialogWindow.GlobalFrame, 0);
            DialogWindow.GlobalFrame.Content = AllDialog.GAllDial;
            DialogWindow.GlobalCanvas.Visibility = Visibility.Visible;
            DialogWindow.GlobalHelp.Visibility = Visibility.Visible;
            DialogWindow.GlobalOptions.Source = new Uri("Settings.xaml", UriKind.Relative);
            DialogWindow.GlobalCanvas.MouseLeftButtonDown += GlobalCanvas_MouseLeftButtonDown;
        }

        private static void Settings_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Panel.SetZIndex(DialogWindow.GlobalFrame, 0);
            DialogWindow.GlobalFrame.Content = AllDialog.GAllDial;
            DialogWindow.GlobalCanvas.Visibility = Visibility.Visible;
            DialogWindow.GlobalHelp.Visibility = Visibility.Visible;
            DialogWindow.GlobalOptions.Source = new Uri("Autor.xaml", UriKind.Relative);
            DialogWindow.GlobalCanvas.MouseLeftButtonDown += GlobalCanvas_MouseLeftButtonDown;
        }

        private static void InformMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Panel.SetZIndex(DialogWindow.GlobalFrame, 0);
            DialogWindow.GlobalFrame.Content = AllDialog.GAllDial;
            DialogWindow.GlobalCanvas.Visibility = Visibility.Visible;
            DialogWindow.GlobalHelp.Visibility = Visibility.Visible;
            DialogWindow.GlobalOptions.Source = new Uri("Contacs.xaml", UriKind.Relative);
            DialogWindow.GlobalCanvas.MouseLeftButtonDown += GlobalCanvas_MouseLeftButtonDown;
        }

        public static void GlobalCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DialogWindow.GlobalCanvas.Visibility = Visibility.Hidden;
            DialogWindow.GlobalHelp.Visibility = Visibility.Hidden;
            DialogWindow.GlobalCanvas.MouseLeftButtonDown -= GlobalCanvas_MouseLeftButtonDown;
        }

        private static void Contacts_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Canvas) sender).Background = Brushes.White;
        }

        private static void Contacts_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Canvas)sender).Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(241, 241, 241));
        }
    }
}