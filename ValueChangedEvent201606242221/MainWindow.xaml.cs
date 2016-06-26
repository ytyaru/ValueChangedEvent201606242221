using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ValueChangedEvent201606242221
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.RoutedPropertyChangedEventHandler<double> slider1ValueChanged;
        System.Windows.RoutedPropertyChangedEventHandler<double> slider2ValueChanged;
        System.Windows.RoutedPropertyChangedEventHandler<double> slider3ValueChanged;
        System.Windows.RoutedPropertyChangedEventHandler<double> slider4ValueChanged;
        System.Windows.RoutedPropertyChangedEventHandler<double> slider5ValueChanged;
        System.Windows.RoutedPropertyChangedEventHandler<double> slider6ValueChanged;

        public MainWindow()
        {
            InitializeComponent();
            InitializeEvent();
            SetColor((byte)slider4.Value, (byte)slider5.Value, (byte)slider6.Value);
        }

        private void InitializeEvent()
        {
            slider1ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.slider1_ValueChanged);
            slider2ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.slider2_ValueChanged);
            slider3ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.slider3_ValueChanged);
            slider4ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.slider4_ValueChanged);
            slider5ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.slider5_ValueChanged);
            slider6ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.slider6_ValueChanged);

            slider1.ValueChanged += slider1ValueChanged;
            slider2.ValueChanged += slider2ValueChanged;
            slider3.ValueChanged += slider3ValueChanged;
            slider4.ValueChanged += slider4ValueChanged;
            slider5.ValueChanged += slider5ValueChanged;
            slider6.ValueChanged += slider6ValueChanged;
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetRgb((float)slider1.Value, (float)slider2.Value, (float)slider3.Value);
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetRgb((float)slider1.Value, (float)slider2.Value, (float)slider3.Value);
        }

        private void slider3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetRgb((float)slider1.Value, (float)slider2.Value, (float)slider3.Value);
        }

        private void slider4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetHsv((int)slider4.Value, (int)slider5.Value, (int)slider6.Value);
        }

        private void slider5_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetHsv((int)slider4.Value, (int)slider5.Value, (int)slider6.Value);
        }

        private void slider6_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetHsv((int)slider4.Value, (int)slider5.Value, (int)slider6.Value);
        }

        private void SetRgb(float h, float s, float v)
        {
            slider4.ValueChanged -= slider4ValueChanged;
            slider5.ValueChanged -= slider5ValueChanged;
            slider6.ValueChanged -= slider6ValueChanged;

            var rgb = HsvRgbConverter.ToRgb(h, s, v);
            slider4.Value = (byte)rgb.R;
            slider5.Value = (byte)rgb.G;
            slider6.Value = (byte)rgb.B;
            SetColor((byte)slider4.Value, (byte)slider5.Value, (byte)slider6.Value);

            slider4.ValueChanged += slider4ValueChanged;
            slider5.ValueChanged += slider5ValueChanged;
            slider6.ValueChanged += slider6ValueChanged;
        }
        private void SetHsv(int r, int g, int b)
        {
            slider1.ValueChanged -= slider1ValueChanged;
            slider2.ValueChanged -= slider2ValueChanged;
            slider3.ValueChanged -= slider3ValueChanged;

            var hsv = HsvRgbConverter.ToHsv(r, g, b);
            slider1.Value = (double)hsv.H;
            slider2.Value = (double)hsv.S;
            slider3.Value = (double)hsv.V;
            SetColor((byte)slider4.Value, (byte)slider5.Value, (byte)slider6.Value);

            slider1.ValueChanged += slider1ValueChanged;
            slider2.ValueChanged += slider2ValueChanged;
            slider3.ValueChanged += slider3ValueChanged;
        }

        private void SetColor(byte R, byte G, byte B)
        {
            Background = new SolidColorBrush(Color.FromRgb((byte)slider4.Value, (byte)slider5.Value, (byte)slider6.Value));

            // 反対色
            Color oppColor = GetOppositeColor(R, G, B);

            // 補色
            Color comColor = GetComplementaryColor(R, G, B);

            SolidColorBrush oppBrush = new SolidColorBrush(oppColor);
            SolidColorBrush comBrush = new SolidColorBrush(comColor);
            label1.Foreground = oppBrush;
            label2.Foreground = oppBrush;
            label3.Foreground = oppBrush;
            label4.Foreground = comBrush;
            label5.Foreground = comBrush;
            label6.Foreground = comBrush;
        }

        /// <summary>
        /// 補色を返す。
        /// </summary>
        /// <param name="R">Red</param>
        /// <param name="G">Green</param>
        /// <param name="B">Blue</param>
        /// <returns>補色</returns>
        private Color GetComplementaryColor(byte R, byte G, byte B)
        {
            byte min = Math.Min(Math.Min(R, G), B);
            byte max = Math.Max(Math.Max(R, G), B);
            return Color.FromRgb(
                (byte)((max + min) - R),
                (byte)((max + min) - G),
                (byte)((max + min) - B));
        }

        /// <summary>
        /// 反対色を返す
        /// </summary>
        /// <param name="R">Red</param>
        /// <param name="G">Green</param>
        /// <param name="B">Blue</param>
        /// <returns>反対色</returns>
        private Color GetOppositeColor(byte R, byte G, byte B)
        {
            return Color.FromRgb(
                (byte)(R ^ 0xff),
                (byte)(G ^ 0xff),
                (byte)(B ^ 0xff));
        }
    }
}
