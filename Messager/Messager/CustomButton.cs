using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Messager.Client;

namespace Messager
{
    public class CustomButton:Button
    {
        private readonly ServerWorks sw = new ServerWorks();
        private readonly Random _rnd = new Random();
        public int IdUser { get; set; }
        private readonly CustomMessage cm;
        private int statusMessage;

        public CustomButton()
        {
            DefaultStyleKey = typeof(CustomButton);
            ColorBackground= new SolidColorBrush(Color.FromRgb(255, 255, 255));
            RandomColor();
            MouseEnter += CustomButton_MouseEnter;
            MouseLeave += CustomButton_MouseLeave;
            Click += CustomButton_Click;
            cm = new CustomMessage();
        }

        public int StatusMessage
        {
            get => statusMessage;
            set
            {
                switch (value)
                {
                    case 0:
                        CheckImageSource = new BitmapImage(new Uri("/icons/statustwo.png", UriKind.Relative));
                        break;
                    case 1:
                        CheckImageSource = new BitmapImage(new Uri("/icons/statusone.png", UriKind.Relative));
                        break;
                    default:
                        CheckImageSource = new BitmapImage();
                        break;
                }
                statusMessage = value;
            }
        }

        public void RandomColor()
        {
            int k = _rnd.Next(0, 10);
            switch (k)
            {
                case 0:
                {
                    ColorCircleBackground = Brushes.BlueViolet;
                    break;
                }
                case 1:
                {
                    ColorCircleBackground = Brushes.Aquamarine;
                        break;
                }
                case 2:
                {
                    ColorCircleBackground = Brushes.Chocolate;
                       break;
                }
                case 3:
                {
                    ColorCircleBackground = Brushes.Chartreuse;
                        break;
                }
                case 4:
                {
                    ColorCircleBackground = Brushes.DeepPink;
                        break;
                }
                case 5:
                {
                    ColorCircleBackground = Brushes.Salmon;
                        break;
                }
                case 6:
                {
                    ColorCircleBackground = Brushes.YellowGreen;
                        break;
                }
                case 7:
                {
                    ColorCircleBackground = Brushes.DarkCyan;
                        break;
                }
                case 8:
                {
                    ColorCircleBackground = Brushes.OrangeRed;
                        break;
                }
                case 9:
                {
                    ColorCircleBackground = Brushes.IndianRed;
                        break;
                }
            }
        }

        public void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            Const.IdFocus = IdUser;
            Const.secondColor = ColorCircleBackground;
            UserC user = sw.GetUserById(Const.session, IdUser);
            if (Const.mess!=null)
            {
                Const.mess.SetData(user);
            }
            if (Const.cb != null)
            {
                Const.cb.MouseEnter += Const.cb.CustomButton_MouseEnter;
                Const.cb.MouseLeave += Const.cb.CustomButton_MouseLeave;
                Const.cb.Click += Const.cb.CustomButton_Click;
                Const.cb.ColorBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                Const.cb.ColorMessage = new SolidColorBrush(Color.FromRgb(168, 168, 168));
                Const.cb.ColorName = new SolidColorBrush(Color.FromRgb(34, 34, 34));
                Const.cb.ColorTime = new SolidColorBrush(Color.FromRgb(168, 168, 168));
            }
            cm.SetData();
            Message.GsViewer.ScrollToEnd();
            Click -= CustomButton_Click;
            MouseEnter -= CustomButton_MouseEnter;
            MouseLeave -= CustomButton_MouseLeave;
            ColorBackground = new SolidColorBrush(Color.FromRgb(65, 159, 217));
            ColorMessage = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ColorName = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ColorTime = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            Const.cb = this;
        }

        public void CustomButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ColorBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        public void CustomButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ColorBackground = new SolidColorBrush(Color.FromRgb(241, 241, 241));
        }

        public SolidColorBrush ColorBackground
        {
            get => (SolidColorBrush)GetValue(ColorBackgroundProperty);
            set => SetValue(ColorBackgroundProperty, value);
        }

        public static readonly DependencyProperty ColorBackgroundProperty =
            DependencyProperty.Register(
                "ColorBackground", typeof(SolidColorBrush), typeof(CustomButton),
                new PropertyMetadata(new SolidColorBrush(), new PropertyChangedCallback(OnColorChanged)));

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomButton)d).ColorBackground = (SolidColorBrush)e.NewValue;
        }

        public SolidColorBrush ColorName
        {
            get => (SolidColorBrush)GetValue(ColorNameProperty);
            set => SetValue(ColorNameProperty, value);
        }

        public static readonly DependencyProperty ColorNameProperty =
            DependencyProperty.Register(
                "ColorName", typeof(SolidColorBrush), typeof(CustomButton),
                new PropertyMetadata(new SolidColorBrush(), new PropertyChangedCallback(OnColorNameChanged)));

        private static void OnColorNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomButton)d).ColorName = (SolidColorBrush)e.NewValue;
        }

        public SolidColorBrush ColorTime
        {
            get => (SolidColorBrush)GetValue(ColorTimeProperty);
            set => SetValue(ColorTimeProperty, value);
        }

        public static readonly DependencyProperty ColorTimeProperty =
            DependencyProperty.Register(
                "ColorTime", typeof(SolidColorBrush), typeof(CustomButton),
                new PropertyMetadata(new SolidColorBrush(), new PropertyChangedCallback(OnColorTimeChanged)));

        private static void OnColorTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomButton)d).ColorTime = (SolidColorBrush)e.NewValue;
        }

        public SolidColorBrush ColorMessage
        {
            get => (SolidColorBrush)GetValue(ColorMessageProperty);
            set => SetValue(ColorMessageProperty, value);
        }

        public static readonly DependencyProperty ColorMessageProperty =
            DependencyProperty.Register(
                "ColorMessage", typeof(SolidColorBrush), typeof(CustomButton),
                new PropertyMetadata(new SolidColorBrush(), new PropertyChangedCallback(OnColorMessageChanged)));

        private static void OnColorMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomButton)d).ColorMessage = (SolidColorBrush)e.NewValue;
        }

        public SolidColorBrush ColorCircleBackground
        {
            get => (SolidColorBrush)GetValue(ColorCircleBackgroundProperty);
            set => SetValue(ColorCircleBackgroundProperty, value);
        }

        public static readonly DependencyProperty ColorCircleBackgroundProperty =
            DependencyProperty.Register(
                "ColorCircleBackground", typeof(SolidColorBrush), typeof(CustomButton),
                new PropertyMetadata(new SolidColorBrush(), new PropertyChangedCallback(OnColorCircleBackgroundChanged)));

        private static void OnColorCircleBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomButton)d).ColorCircleBackground = (SolidColorBrush)e.NewValue;
        }

        public string TextName
        {
            get => (string)GetValue(TextNameProperty);
            set => SetValue(TextNameProperty, value);
        }

        public static readonly DependencyProperty TextNameProperty =
            DependencyProperty.Register(
                "TextName", typeof(string), typeof(CustomButton),
                new PropertyMetadata("", new PropertyChangedCallback(OnTextNameChanged)));

        private static void OnTextNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomButton)d).TextName = (string)e.NewValue;
        }

        public string TextMessage
        {
            get => (string)GetValue(TextMessageProperty);
            set
            {
                if (value.Length > 35)
                {
                    string mysrting = value.Substring(0, 35) + "...";
                    value = mysrting;
                }

                SetValue(TextMessageProperty, value);
            }
        }

        public static readonly DependencyProperty TextMessageProperty =
            DependencyProperty.Register(
                "TextMessage", typeof(string), typeof(CustomButton),
                new PropertyMetadata("", new PropertyChangedCallback(OnTextMessageChanged)));

        private static void OnTextMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           ((CustomButton)d).TextMessage = (string)e.NewValue;
        }

        public string TextTime
        {
            get => (string)GetValue(TextTimeProperty);
            set => SetValue(TextTimeProperty, value);
        }

        public static readonly DependencyProperty TextTimeProperty =
            DependencyProperty.Register(
                "TextTime", typeof(string), typeof(CustomButton),
                new PropertyMetadata("", new PropertyChangedCallback(OnTextTimeChanged)));

        private static void OnTextTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomButton)d).TextTime = (string)e.NewValue;
        }

        public string CountNewMessage
        {
            get => (string)GetValue(CountNewMessageProperty);
            set
            {
                VisibleNewMessage = Convert.ToInt32(value) > 0 ? Visibility.Visible : Visibility.Hidden;
                SetValue(CountNewMessageProperty, value);
            }
        }

        public static readonly DependencyProperty CountNewMessageProperty =
            DependencyProperty.Register(
                "CountNewMessage", typeof(string), typeof(CustomButton),
                new PropertyMetadata("", new PropertyChangedCallback(OnCountNewMessageChanged)));

        private static void OnCountNewMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomButton)d).CountNewMessage = (string)e.NewValue;
        }


        public Visibility VisibleNewMessage
        {
            get => (Visibility)GetValue(VisibleNewMessageProperty);
            set => SetValue(VisibleNewMessageProperty, value);
        }

        public static readonly DependencyProperty VisibleNewMessageProperty =
            DependencyProperty.Register(
                "VisibleNewMessage", typeof(Visibility), typeof(CustomButton),
                new PropertyMetadata(Visibility.Hidden, new PropertyChangedCallback(OnVisibleNewMessageChanged)));

        private static void OnVisibleNewMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomButton)d).VisibleNewMessage = (Visibility)e.NewValue;
        }

        public ImageSource CheckImageSource
        {
            get => (ImageSource)GetValue(CheckImageSourceProperty);
            set => SetValue(CheckImageSourceProperty, value);
        }

        public static readonly DependencyProperty CheckImageSourceProperty =
            DependencyProperty.Register(
                "CheckImageSource", typeof(ImageSource), typeof(CustomButton),
                new PropertyMetadata(new BitmapImage(), new PropertyChangedCallback(OnCheckImageSourceChanged)));

        private static void OnCheckImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomButton)d).CheckImageSource = (ImageSource)e.NewValue;
        }

        public string TextSubname
        {
            get => (string)GetValue(TextSubnameProperty);
            set => SetValue(TextSubnameProperty, value);
        }

        public static readonly DependencyProperty TextSubnameProperty =
            DependencyProperty.Register(
                "TextSubname", typeof(string), typeof(CustomButton),
                new PropertyMetadata("", new PropertyChangedCallback(OnTextSubnameChanged)));
        private static void OnTextSubnameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomButton)d).TextSubname = (string)e.NewValue;
        }
    }
}