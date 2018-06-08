using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Messager
{
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
            tPassword.KeyDown += Box_KeyDown;
            tLogin.KeyDown += Box_KeyDown;
            ServerWorks sw = new ServerWorks();
            using (FileStream fstream = File.OpenRead(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "session.txt")))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                if (!string.IsNullOrEmpty(textFromFile))
                {
                    if (sw.GetMyData(textFromFile) != null)
                    {
                        Const.session = textFromFile;
                        MainWindow.GlobalMainFrame.Source = new Uri("DialogWindow.xaml", UriKind.Relative);
                    }
                    else
                    {
                        Label.Content = "Данные сесcии устарели,произведите вход заново.";
                    }
                }
            }
        }

        private void Box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (tLogin.Text != "" && tPassword.Password != "")
                {
                    ServerWorks sw = new ServerWorks();
                    string result = sw.SignIn(tLogin.Text, tPassword.Password);
                    if (result != "CANCEL" && result != "SERVER_ERROR")
                    {
                        Const.session = result;
                        using (FileStream fstream = new FileStream(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "session.txt"), FileMode.Create))
                        {
                            byte[] array = System.Text.Encoding.Default.GetBytes(result);
                            fstream.Write(array, 0, array.Length);
                        }
                        MainWindow.GlobalMainFrame.Source = new Uri("DialogWindow.xaml", UriKind.Relative);
                    }
                    else
                    {
                        Label.Content = result.Equals("SERVER_ERROR") ? "Неполадки с сервером!Возвращайтесь позже!" : "Неверное имя пользователя или пароль";
                    }
                }
                else
                {
                    MessageBox.Show("Заполняйте все поля!");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.GlobalMainFrame.Source= new Uri("Register.xaml", UriKind.Relative);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tLogin.Text != "" && tPassword.Password != "")
            {
                ServerWorks sw = new ServerWorks();
                string result = sw.SignIn(tLogin.Text, tPassword.Password);
                if (result != "CANCEL" && result != "SERVER_ERROR")
                {
                    Const.session = result;
                    using (FileStream fstream =
                        new FileStream(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "session.txt"),
                            FileMode.Create))
                    {
                        byte[] array = System.Text.Encoding.Default.GetBytes(result);
                        fstream.Write(array, 0, array.Length);
                    }

                    MainWindow.GlobalMainFrame.Source = new Uri("DialogWindow.xaml", UriKind.Relative);
                }
                else
                {
                    Label.Content = result.Equals("SERVER_ERROR")
                        ? "Неполадки с сервером!Возвращайтесь позже!"
                        : "Неверное имя пользователя или пароль";
                }
            }
            else
            {
                MessageBox.Show("Заполняйте все поля!");
            }
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, RoutedEventArgs routedEventArgs)
        {
            User.Visibility = tLogin.Text != "" ? Visibility.Hidden : Visibility.Visible;
            Pass.Visibility = tPassword.Password != "" ? Visibility.Hidden : Visibility.Visible;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Всего хорошего!");
            Environment.Exit(0);
        }
    }
}