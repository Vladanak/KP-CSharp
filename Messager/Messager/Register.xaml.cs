using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Messager
{
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServerWorks server = new ServerWorks();
            if (!tPass.Password.Equals(tPassRepeat.Password) || tPassRepeat.Password.Equals(""))
            {
                MessageBox.Show("Проверьте правильность повтора пароля!");
                return; 
            }
            string result = server.Registration(tUName.Text, tPass.Password, tEmail.Text, tPhone.Text, tName.Text, tSurname.Text);
            if (result.Equals("SERVER_ERROR"))
                MessageBox.Show("Неполадки с сервером!Возвращайтесь позже!");
            if (result.Equals("VALID"))
                MessageBox.Show("Проверьте правильность ввода!");
            if (result.Equals("TRUE1"))
                MessageBox.Show("Пользователь имеющий такой Логин уже существует!");
            if (result.Equals("TRUE2"))
                MessageBox.Show("Пользователь имеющий такой Email уже существует!");
            if (result.Equals("TRUE3"))
                MessageBox.Show("Пользователь имеющий такой Номер уже существует!");
            if (result.Equals("VALIDPass"))
                MessageBox.Show("Пароль должен быть от 6 до 15 символов с обязательными цифрами и большой буквой!");
            if (result.Equals("VALIDEmail"))
                MessageBox.Show("Email Обязательно должен содержать символ @!");
            if (result.Equals("VALIDNumber"))
                MessageBox.Show("Номер телефона обязательно должен содержать символ +! Возможно использование -.");
            if (result.Equals("OK"))
            {
                MessageBox.Show("Регистрация успешна!");
                MainWindow.GlobalMainFrame.Source = new Uri("SignIn.xaml", UriKind.Relative);
            }
        }

        private void Send_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.GlobalMainFrame.Source = new Uri("SignIn.xaml", UriKind.Relative);
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, RoutedEventArgs routedEventArgs)
        {
            if (tUName.Text != "" || User.IsMouseOver)
            {
                User.Visibility = Visibility.Hidden;
            }
            else
            {
                User.Visibility = Visibility.Visible;
            }
            if (tPass.Password != "" || Pass.IsMouseOver)
            {
                Pass.Visibility = Visibility.Hidden;
            }
            else
            {
                Pass.Visibility = Visibility.Visible;
            }
            if (tPassRepeat.Password != "" || Pass_repeat.IsMouseOver)
            {
                Pass_repeat.Visibility = Visibility.Hidden;
            }
            else
            {
                Pass_repeat.Visibility = Visibility.Visible;
            }
            if (tName.Text != "" || name.IsMouseOver)
            {
                name.Visibility = Visibility.Hidden;
            }
            else
            {
                name.Visibility = Visibility.Visible;
            }
            if (tSurname.Text != "" || surname.IsMouseOver)
            {
                surname.Visibility = Visibility.Hidden;
            }
            else
            {
                surname.Visibility = Visibility.Visible;
            }
            if (tEmail.Text != "" || email.IsMouseOver)
            {
                email.Visibility = Visibility.Hidden;
            }
            else
            {
                email.Visibility = Visibility.Visible;
            }
            if (tPhone.Text != "" || number.IsMouseOver)
            {
                number.Visibility = Visibility.Hidden;
            }
            else
            {
                number.Visibility = Visibility.Visible;
            }
        }

        private void TName_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !("0123456789 ,".IndexOf(e.Text) < 0);
        }

        private void TSurname_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !("0123456789 ,".IndexOf(e.Text) < 0);
        }
    }
}