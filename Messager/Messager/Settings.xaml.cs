using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Messager
{
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            BtnClose.Cursor = Cursors.Hand;
            BtnClose.MouseLeftButtonDown += Message.GlobalCanvas_MouseLeftButtonDown;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ServerWorks server = new ServerWorks();
            string result = server.ChangeSetting(TUser.Text, TPassNew.Password,TPass.Password,tEmail.Text);
            if (result.Equals("OK"))
                MessageBox.Show("Изменения внесены!");
            if (result.Equals("VALID"))
                MessageBox.Show("Проверьте правильность вводимых данных!");
            if (result.Equals("FALSE"))
                MessageBox.Show("Оригинальный пароль и введёный не совпадают!");
            if (result.Equals("NAME"))
                MessageBox.Show("Пользователь имеющий такой Логин уже существует!");
            if (result.Equals("EMAIL"))
                MessageBox.Show("Пользователь имеющий такой Email уже существует!");
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, RoutedEventArgs routedEventArgs)
        {
            User.Visibility = TUser.Text != "" ? Visibility.Hidden : Visibility.Visible;
            Pass.Visibility = TPass.Password != "" ? Visibility.Hidden : Visibility.Visible;
            PassRepeat.Visibility = TPassNew.Password != "" ? Visibility.Hidden : Visibility.Visible;
            Email.Visibility = tEmail.Text != "" ? Visibility.Hidden : Visibility.Visible;
        }
    }
}