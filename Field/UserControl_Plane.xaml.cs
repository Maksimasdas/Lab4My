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
    /// Логика взаимодействия для UserControl_Plane.xaml
    /// </summary>
    public partial class UserControl_Plane : UserControl
    {
        Plane plane;
        public UserControl_Plane(double speed)
        {
            InitializeComponent();
            plane = new Plane(speed);
            this.DataContext = plane;

            //привязка скорости
            Binding bindingSpeed = new Binding("SpeedX");
            bindingSpeed.Converter = new SpeedToString();
            textblockSpeed.SetBinding(TextBlock.TextProperty, bindingSpeed);
            
            //привязка координат Х и У
            Binding bindingX = new Binding("X");
            this.SetBinding(Canvas.LeftProperty, bindingX);

            Binding bindY = new Binding("Y");
            this.SetBinding(Canvas.TopProperty, bindY);

        }

        public void UpdateSpeed(double speed)
        {
            plane.SpeedX = speed;
        }

        public double GetSpeed()
        {
            return plane.SpeedX;
        }

        public void SetXplane(double x)
        {
            plane.X = x;
        }

        public void SetYplane(double y)
        {
            plane.Y = y;
        }

        public double GetXplane()
        {
            return plane.X;
        }

        public double GetYplane()
        {
            return plane.Y;
        }




    }
}
