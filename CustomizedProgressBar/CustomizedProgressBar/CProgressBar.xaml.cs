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
using System.Windows.Media.Animation;

namespace CustomizedProgressBar
{
    /// <summary>
    /// Interaction logic for CProgressBar.xaml
    /// </summary>
    public partial class CProgressBar : UserControl
    { 
        //properties for fully customization 
        private double width;
        private double height;

        private int totalTime;
     
        public CProgressBar(double w, double h, int sec)
        {
            width = w;
            height = h;
            totalTime = sec;
            InitializeComponent();
        }

        private void startProgress(object sender, RoutedEventArgs e)
        {
            progressBar1.Width = this.width;
            progressBar1.Height = this.height;
            progressBar1.Margin = new Thickness(3, 3, 0, 0);

            border1.Margin = new Thickness(0);
            border1.Width = this.width + 6;
            border1.Height = this.height + 6;

            SolidColorBrush scb = new SolidColorBrush(Colors.PaleGreen);
            LinearGradientBrush lgb = new LinearGradientBrush();
            GradientStop stop1 = new GradientStop(Colors.GreenYellow, 0.0);
            GradientStop stop2 = new GradientStop(Colors.LightBlue, 0.5);
            GradientStop stop3 = new GradientStop(Colors.Violet, 1.0);
            lgb.GradientStops.Add(stop1);
            lgb.GradientStops.Add(stop2);
            lgb.GradientStops.Add(stop3);
            Rectangle r = (Rectangle)progressBar1.Template.FindName("bar", progressBar1);
            r.Fill = lgb;

            progressBar1.ValueChanged += new RoutedPropertyChangedEventHandler<double>(progressBar1_ValueChanged);
           

            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 100;
            da.Duration = new Duration(TimeSpan.FromSeconds(totalTime));
            progressBar1.BeginAnimation(ProgressBar.ValueProperty, da);

            ColorAnimation ca1 = new ColorAnimation();
            ca1.To = Colors.Yellow;
            ca1.Duration = new Duration(TimeSpan.FromSeconds(totalTime));
            stop1.BeginAnimation(GradientStop.ColorProperty, ca1);

            ColorAnimation ca2 = new ColorAnimation();
            ca2.To = Colors.Orange;
            ca2.Duration = new Duration(TimeSpan.FromSeconds(totalTime));
            stop2.BeginAnimation(GradientStop.ColorProperty, ca2);

            ColorAnimation ca3 = new ColorAnimation();
            ca3.To = Colors.Tomato;
            ca3.Duration = new Duration(TimeSpan.FromSeconds(totalTime));
            stop3.BeginAnimation(GradientStop.ColorProperty, ca3);
           
        }

        void progressBar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (progressBar1.Value == 100)
            {
                progressBar1.ValueChanged -= new RoutedPropertyChangedEventHandler<double>(progressBar1_ValueChanged);
                MessageBox.Show("Complete!");
            }

            Rectangle r = (Rectangle)progressBar1.Template.FindName("bar", progressBar1);
            r.Width = progressBar1.Value * progressBar1.Width / 100;
             
        }
    }
}
