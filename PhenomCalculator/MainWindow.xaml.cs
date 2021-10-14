using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhenomCalculator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private double getN1NoIceProt(double pressureAlt, double SAT)
        {
            double p00 = 84.41;
            double p10 = 0.001405;
            double p01 = 0.1779;
            double p20 = 7.638e-08;
            double p11 = 4.551e-06;
            double p02 = 0.0006793;
            double p30 = -3.814e-11;
            double p21 = -5.66e-09;
            double p12 = -1.017e-06;
            double p03 = -3.901e-05;
            double p40 = 3.188e-15;
            double p31 = 4.249e-13;
            double p22 = -7.356e-14;
            double p13 = -1.265e-08;
            double p04 = -1.204e-06;
            double p50 = -8.606e-20;
            double p41 = -1.248e-17;
            double p32 = 3.007e-15;
            double p23 = 1.716e-12;
            double p14 = 2.628e-10;
            return p00 + p10 * pressureAlt + p01 * SAT + p20 * pressureAlt * pressureAlt + p11 * pressureAlt * SAT + p02 * SAT * SAT + p30 * pressureAlt * pressureAlt * pressureAlt + p21 * pressureAlt * pressureAlt * SAT + p12 * pressureAlt * SAT * SAT + p03 * SAT * SAT * SAT + p40 * pressureAlt * pressureAlt * pressureAlt * pressureAlt + p31 * pressureAlt * pressureAlt * pressureAlt * SAT + p22 * pressureAlt * pressureAlt * SAT * SAT + p13 * pressureAlt * SAT * SAT * SAT + p04 * SAT * SAT * SAT * SAT + p50 * pressureAlt * pressureAlt * pressureAlt * pressureAlt * pressureAlt + p41 * pressureAlt * pressureAlt * pressureAlt * pressureAlt * SAT + p32 * pressureAlt * pressureAlt * pressureAlt * SAT * SAT + p23 * pressureAlt * pressureAlt * SAT * SAT * SAT + p14 * pressureAlt * SAT * SAT * SAT * SAT;
        }
        private double getN1IceProt(double pressureAlt, double SAT)
        {
            double p00 = 84.68;
            double p10 = 0.001217;
            double p01 = 0.1658;
            double p20 = 2.528e-08;
            double p11 = 3.893e-06;
            double p02 = -0.002034;
            double p30 = -2.2e-13;
            double p21 = -7.189e-09;
            double p12 = -7.151e-07;
            double p03 = -0.0002037;
            double p40 = -5.775e-15;
            double p31 = -1.022e-12;
            double p22 = -2.146e-10;
            double p13 = 1.599e-08;
            double p04 = -4.145e-06;
            double p50 = 4.476e-19;
            double p41 = 1.279e-16;
            double p32 = 3.455e-14;
            double p23 = 6.939e-12;
            double p14 = 1.396e-09;
            return p00 + p10 * pressureAlt + p01 * SAT + p20 * pressureAlt * pressureAlt + p11 * pressureAlt * SAT + p02 * SAT * SAT + p30 * pressureAlt * pressureAlt * pressureAlt + p21 * pressureAlt * pressureAlt * SAT + p12 * pressureAlt * SAT * SAT + p03 * SAT * SAT * SAT + p40 * pressureAlt * pressureAlt * pressureAlt * pressureAlt + p31 * pressureAlt * pressureAlt * pressureAlt * SAT + p22 * pressureAlt * pressureAlt * SAT * SAT + p13 * pressureAlt * SAT * SAT * SAT + p04 * SAT * SAT * SAT * SAT + p50 * pressureAlt * pressureAlt * pressureAlt * pressureAlt * pressureAlt + p41 * pressureAlt * pressureAlt * pressureAlt * pressureAlt * SAT + p32 * pressureAlt * pressureAlt * pressureAlt * SAT * SAT + p23 * pressureAlt * pressureAlt * SAT * SAT * SAT + p14 * pressureAlt * SAT * SAT * SAT * SAT;

        }

        private void N1_CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(N1_PressureAlt.Text, out double pressureAlt))
            {
                pressureAlt = 1000;
                N1_PressureAlt.Text = "1000";
                _ = MessageBox.Show("Invalid pressure altitude. Assuming 1000 ft.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (!double.TryParse(N1_SAT.Text, out double SAT))
            {
                SAT = 15;
                N1_SAT.Text = "15";
                _ = MessageBox.Show("Invalid SAT. Assuming ISA 15°C", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (SAT > 5 && N1_IceProt.IsChecked == true)
            {
                N1_IceProt.IsChecked = false;
                _ = MessageBox.Show("SAT above 5°C. Disable Ice Protection for T/O", "Ice Prot not required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (pressureAlt > 10000 && N1_IceProt.IsChecked == true)
            {
                pressureAlt = 10000;
                N1_PressureAlt.Text = "10000";
                _ = MessageBox.Show("Pressure altitude above maximum with ice protection. Limiting to 10000 ft.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (pressureAlt < -1000)
            {
                pressureAlt = -1000;
                N1_PressureAlt.Text = "-1000";
                _ = MessageBox.Show("Pressure altitude below minimum. Limiting to -1000 ft.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            } else if (pressureAlt > 14000)
            {
                pressureAlt = 14000;
                N1_PressureAlt.Text = "14000";
                _ = MessageBox.Show("Pressure altitude above maximum. Limiting to 14000 ft.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if ( SAT < -40)
            {
                SAT = -40;
                N1_SAT.Text = "-40";
                _ = MessageBox.Show("SAT below minimum. Limiting to -40°C.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else if (SAT > 45)
            {
                SAT = 45;
                N1_SAT.Text = "45";
                _ = MessageBox.Show("SAT above maximum. Limiting to 45°C.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if ((SAT > 20 && pressureAlt > 12000) || (SAT > 25 && pressureAlt > 10000) || (SAT > 30 && pressureAlt > 7000) || (SAT > 35 && pressureAlt > 5000) || (SAT > 40 && pressureAlt > 2000))
            {
                _ = MessageBox.Show("Parameters exceed aircraft performance. Take-off not recommended.", "Performance Error", MessageBoxButton.OK, MessageBoxImage.Error);
                N1_N1.Text = "NO TAKE-OFF";
                return;
            }
            double estimatedN1 = N1_IceProt.IsChecked == true ? getN1IceProt(pressureAlt, SAT) : getN1NoIceProt(pressureAlt, SAT);
            N1_N1.Text = estimatedN1.ToString("0." + new string('#', 2));
        }

        private void CalculateVSpeedsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
