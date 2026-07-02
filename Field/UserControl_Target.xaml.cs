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

namespace Airport
{
    /// <summary>
    /// Логика взаимодействия для UserControl_Target.xaml
    /// </summary>
    public partial class UserControl_Target : UserControl
    {
        Target target;
        public UserControl_Target()
        {
            InitializeComponent();
            target = new Target();
            this.DataContext = target;
        }

        public void SetXTarget(double x)
        {
            Canvas.SetLeft(this, x);
            target.X = x;
            target.LX = x + 10;
            target.RX = x + 10;
        }

        public void SetYTarget(double y)
        {
            Canvas.SetTop(this, y);
            target.Y = y;
            target.UY = y + 10;
        }

        public double GetXTarget()
        {
            return target.X;
        }

        public double GetYTarget()
        {
            return target.Y;
        }
    }
}
