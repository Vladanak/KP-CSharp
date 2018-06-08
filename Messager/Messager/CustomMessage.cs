using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Messager.Client;

namespace Messager
{
    class CustomMessage:Button
    {
        private readonly ServerWorks sw;
        private readonly ServerWorksHelper swh;
        public CustomMessage()
        {
            DefaultStyleKey = typeof(CustomMessage);
            sw= new ServerWorks();
            swh = new ServerWorksHelper();
        }

        public void SetData()
        {
            Message.GStackPanel.Children.Clear();
            SolidColorBrush scbS = swh.RandomColor();
            SolidColorBrush scbR = swh.RandomColor();
            sw.UpdateNewMessage(Const.session, Const.IdFocus);
            List<MessageC> list = sw.GetAllMessageUser(Const.session, Const.IdFocus);
            if (list != null)
            {
                ServerWorksHelper.GetMessageLastId(list);
                foreach (var m in list)
                {
                    UserC c = sw.GetUserById(Const.session, Const.IdFocus);
                    UserC user = (UserC)Unpackage<UserC>.Upackage(sw.GetMyData(Const.session));
                    CustomMessage bt = new CustomMessage();
                    DateTime dt = DateTime.Parse(m.time);
                    bt.TextTime = dt.TimeOfDay.ToString();
                    if (m.idReseiver == Const.IdFocus)
                    {
                        bt.ColorBackgroundMessage = new SolidColorBrush(System.Windows.Media.Color.FromRgb(239, 253, 222));
                        bt.ColorBackgroundCircle = scbS;
                        bt.TextSub = ("" + user.name[0] + user.surname[0]).ToUpper();
                    }
                    else
                    {
                        bt.ColorBackgroundMessage = Brushes.White;
                        bt.ColorBackgroundCircle = scbR;
                        bt.TextSub = ("" + c.name[0] + c.surname[0]).ToUpper();
                    }    
                    bt.TextMessage = m.message;
                    bt.CheckImageSource = m.status==1 ? new BitmapImage(new Uri("/icons/statusone.png", UriKind.Relative)) : new BitmapImage(new Uri("/icons/statustwo.png", UriKind.Relative));
                    bt.Template = (ControlTemplate)(this.TryFindResource("Bt2Template"));
                    Message.GStackPanel.Children.Add(bt);
                } 
                Message.GsViewer.ScrollToEnd();
            }
        }

        public string TextMessage
        {
            get => (string)GetValue(TextMessageProperty);
            set
            {
                string full = "";
                string s = value;
                if (value.Length > 50)
                {
                    for (int k = 0; k <= value.Length; k++)
                    {
                        int count = 50;
                        if (s.Length >= 49)
                        {
                            for (int i = 0; i <= 50; i++)
                            {
                                if (value[i] == ' ')
                                {
                                    count = i;
                                }
                            }
                            if (s.Length<50)
                            {
                                break;
                            }
                            var buffer = s.Substring(0, count);
                            s = s.Remove(0, count);
                            full = full + buffer + "\n";
                        }
                        else
                        {
                            full = full + s;
                            value = full;
                            break;
                        }
                    }
                }
                    SetValue(TextMessageProperty, value);         
            }
        }

        public static readonly DependencyProperty TextMessageProperty =
            DependencyProperty.Register(
                "TextMessage", typeof(string), typeof(CustomMessage),
                new PropertyMetadata("", new PropertyChangedCallback(TextMessageChanged)));

        private static void TextMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public string TextTime
        {
            get => (string)GetValue(TextTimeProperty);
            set => SetValue(TextTimeProperty, value);
        }

        public static readonly DependencyProperty TextTimeProperty =
            DependencyProperty.Register(
                "TextTime", typeof(string), typeof(CustomMessage),
                new PropertyMetadata("", new PropertyChangedCallback(TextTimeChanged)));

        private static void TextTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomMessage)d).TextTime = (string)e.NewValue;
        }

        public string TextSub
        {
            get => (string)GetValue(TextSubProperty);
            set => SetValue(TextSubProperty, value);
        }

        public static readonly DependencyProperty TextSubProperty =
            DependencyProperty.Register(
                "TextSub", typeof(string), typeof(CustomMessage),
                new PropertyMetadata("", new PropertyChangedCallback(TextSubChanged)));

        private static void TextSubChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomMessage)d).TextSub = (string)e.NewValue;
        }

        public SolidColorBrush ColorBackgroundCircle
        {
            get => (SolidColorBrush)GetValue(ColorBackgroundCircleProperty);
            set => SetValue(ColorBackgroundCircleProperty, value);
        }

        public static readonly DependencyProperty ColorBackgroundCircleProperty =
            DependencyProperty.Register(
                "ColorBackgroundCircle", typeof(SolidColorBrush), typeof(CustomMessage),
                new PropertyMetadata(new SolidColorBrush(), new PropertyChangedCallback(OnColorBackgroundCircleChanged)));

        private static void OnColorBackgroundCircleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomMessage)d).ColorBackgroundCircle = (SolidColorBrush)e.NewValue;
        }

        public SolidColorBrush ColorBackgroundMessage
        {
            get => (SolidColorBrush)GetValue(ColorBackgroundMessageProperty);
            set => SetValue(ColorBackgroundMessageProperty, value);
        }

        public static readonly DependencyProperty ColorBackgroundMessageProperty =
            DependencyProperty.Register(
                "ColorBackgroundMessage", typeof(SolidColorBrush), typeof(CustomMessage),
                new PropertyMetadata(new SolidColorBrush(), new PropertyChangedCallback(OnColorBackgroundMessageChanged)));

        private static void OnColorBackgroundMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomMessage)d).ColorBackgroundMessage = (SolidColorBrush)e.NewValue;
        }
      
        public ImageSource CheckImageSource
        {
            get => (ImageSource)GetValue(CheckImageSourceProperty);
            set => SetValue(CheckImageSourceProperty, value);
        }

        public static readonly DependencyProperty CheckImageSourceProperty =
            DependencyProperty.Register(
                "CheckImageSource", typeof(ImageSource), typeof(CustomMessage),
                new PropertyMetadata(new BitmapImage(), new PropertyChangedCallback(OnCheckImageSourceChanged)));

        private static void OnCheckImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomMessage)d).CheckImageSource = (ImageSource)e.NewValue;
        }
    }
}