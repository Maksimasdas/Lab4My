using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    public class Bomb: INotifyPropertyChanged
    {
        private double x;
        private double y;
        public double Vx { get; set; } //скорость по Х
        public double Vy { get; set; }// скорость по У
        public double Time { get; set; } //время полета
        public const double Gravity = 9.8; //ускорение свободного падения

        public event PropertyChangedEventHandler? PropertyChanged;

        public double X
        {
            get => x;
            set { x = value; OnPropertyChanged("X"); }
        }

        public double Y
        {
            get => y;
            set { y = value; OnPropertyChanged("Y"); }
        }
        public Bomb(double startX, double startY, double vx)
        {
            X = startX;
            Y = startY;
            Vx = vx;
            Vy = 0;
            Time = 0;
        }

        public void Update(double dt)
        {
            Time += dt;

            X += Vx * dt;
            Y += Vy * dt + 0.5 * Gravity * dt * dt;

            Vy += Gravity * dt;

            OnPropertyChanged(nameof(X)); //использовал nameof для безопасной привязки без использования "X"
            OnPropertyChanged(nameof(Y));
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
