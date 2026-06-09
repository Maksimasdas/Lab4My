using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    public class Plane : INotifyPropertyChanged
    {
        double speedX; //скорость по оси икс
        double x = 0; // координата х
        double y = 0; // координата У
        bool isDropping = false;

        public Plane(double speedX)
        {
            SpeedX = speedX;
            X = 0;   // старт слева
            Y = 0;   // старт сверху
        }

        public double SpeedX
        {
            get => speedX;
            set
            {
                speedX = value;
                OnPropertyChanged("SpeedX");
            }
        }
        public double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }
        public double Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
        }
        public bool IsDropping
        {
            get => isDropping;
            set
            {
                isDropping = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
