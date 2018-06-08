using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Messager.Client;

namespace Messager
{
    public partial class AllDialog : Page
    {
        private bool _flag = false;
        private readonly ServerWorks sw;
        private readonly ServerWorksHelper swh;
        public static AllDialog GAllDial;
        public static StackPanel GPanel;
        public static ScrollViewer GsViewer;
        public AllDialog()
        {
            sw = new ServerWorks();
            swh=new ServerWorksHelper();
            InitializeComponent();
            settings.MouseLeftButtonDown += Settings_MouseLeftButtonDown;
            tBox.TextChanged += tBox_TextChanged;
            tBox.GotFocus += TBox_GotFocus;
            tBox.LostFocus += TBox_LostFocus;
            GAllDial = this;
            GsViewer = Scroll;
            GPanel = StackPanel;
            tBox.TextChanged += TBox_TextChanged;
            Thread t = new Thread(UpdateMessage) {IsBackground = true};
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void TBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tBox.Text))
            {
                var children = StackPanel.Children.OfType<UIElement>().ToList();
                foreach (UIElement s in children)
                {
                    if (((CustomButton) s).TextMessage.Contains(tBox.Text) ||
                        ((CustomButton) s).TextName.Contains(tBox.Text))
                    {
                    }
                    else
                    {
                        StackPanel.Children.Remove(((CustomButton) s));
                    }
                }
            }
            else
            {
                List<LastMessageC> list =
                    Unpackage<LastMessageC>.Upackage(sw.GetLastMessageUser(Const.session));
                if (list != null)
                {
                    StackPanel.Children.Clear();
                    foreach (var l in list)
                    {
                        UserC user = new UserC();
                        user = sw.GetUserById(Const.session, swh.GetIdSecondUser(l));
                        DateTime dt = DateTime.Parse(l.time);
                        CustomButton bt = new CustomButton
                        {
                            IdUser = swh.GetIdSecondUser(l),
                            Template = (ControlTemplate) this.TryFindResource("BtTemplate")
                        };
                        if (l.idUserMessage == l.idSender)
                        {
                            bt.TextMessage = "Вы: " + l.message;
                            bt.StatusMessage = -1;
                        }
                        else
                        {
                            bt.TextMessage = l.message;
                            bt.StatusMessage = l.status;
                        }

                        bt.TextName = user.name + " " + user.surname;
                        bt.TextSubname = ("" + user.name[0] + user.surname[0]).ToUpper();
                        bt.TextTime = dt.TimeOfDay.ToString();
                        bt.ColorMessage =
                            new SolidColorBrush(System.Windows.Media.Color.FromRgb(168, 168, 168));
                        bt.ColorName = new SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 34, 34));
                        bt.ColorTime =
                            new SolidColorBrush(System.Windows.Media.Color.FromRgb(168, 168, 168));
                        bt.CountNewMessage = l.CountNewMessage.ToString();
                        StackPanel.Children.Add(bt);
                        Const.LastIdLastUser = sw.GetLastIdLastUser(Const.session);
                    }
                }
            }
        }

        private void TBox_LostFocus(object sender, RoutedEventArgs e)
        {
            tBox.BorderThickness = new Thickness(0);
            tBox.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(241, 241, 241)); 
        }

        private void TBox_GotFocus(object sender, RoutedEventArgs e)
        {
            tBox.BorderThickness = new Thickness(2);
            tBox.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(84, 195, 243));
            tBox.Background = Brushes.White;
        }

        public void UpdateMessage()
        {
            while (true)
            {
                Thread.Sleep(2000);
                    Dispatcher.BeginInvoke(new ThreadStart(delegate
                    {
                        if (sw.GetLastIdLastUser(Const.session) > Const.LastIdLastUser && string.IsNullOrEmpty(tBox.Text))
                        {
                            List<LastMessageC> list =
                                Unpackage<LastMessageC>.Upackage(sw.GetLastMessageUser(Const.session));
                            if (list != null)
                            {
                                StackPanel.Children.Clear();
                                foreach (var l in list)
                                {
                                    UserC user = new UserC();
                                    user = sw.GetUserById(Const.session, swh.GetIdSecondUser(l));
                                    DateTime dt = DateTime.Parse(l.time);
                                    CustomButton bt = new CustomButton
                                    {
                                        IdUser = swh.GetIdSecondUser(l),
                                        Template = (ControlTemplate) this.TryFindResource("BtTemplate")
                                    };
                                    if (l.idUserMessage == l.idSender)
                                    {
                                        bt.TextMessage = "Вы: " + l.message;
                                        bt.StatusMessage = -1;
                                    }
                                    else
                                    {
                                        bt.TextMessage = l.message;
                                        bt.StatusMessage = l.status;
                                    }
                                    bt.TextName = user.name + " " + user.surname;
                                    bt.TextSubname = ("" + user.name[0] + user.surname[0]).ToUpper();
                                    bt.TextTime = dt.TimeOfDay.ToString();
                                    bt.ColorMessage =
                                        new SolidColorBrush(System.Windows.Media.Color.FromRgb(168, 168, 168));
                                    bt.ColorName = new SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 34, 34));
                                    bt.ColorTime =
                                        new SolidColorBrush(System.Windows.Media.Color.FromRgb(168, 168, 168));
                                    bt.CountNewMessage = l.CountNewMessage.ToString();
                                    StackPanel.Children.Add(bt);
                                    Const.LastIdLastUser = sw.GetLastIdLastUser(Const.session);
                                }
                            }
                        }
                    }));
            }
        }

        private void GlobalBodyFrame_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Panel.SetZIndex(DialogWindow.GlobalFrame, 0);
            if (_flag)
            {
                DialogWindow.GlobalFrame.Content = AllDialog.GAllDial;
                DialogWindow.GlobalCanvas.Visibility = Visibility.Hidden;
                _flag = false;
                DialogWindow.GlobalCanvas.MouseLeftButtonDown -= GlobalBodyFrame_MouseLeftButtonDown;
            }
        }

        private void Settings_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(Menu.GMenu != null)
            DialogWindow.GlobalFrame.Content = Menu.GMenu;
            else DialogWindow.GlobalFrame.Source=new Uri("Menu.xaml",UriKind.Relative);
            Panel.SetZIndex(DialogWindow.GlobalFrame, 2);
            DialogWindow.GlobalCanvas.Visibility = Visibility.Visible;
            _flag = true;
            DialogWindow.GlobalCanvas.MouseLeftButtonDown += GlobalBodyFrame_MouseLeftButtonDown;
        }

        private void tBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Change.Visibility = tBox.Text != "" ? Visibility.Hidden : Visibility.Visible;
        }
    }
}