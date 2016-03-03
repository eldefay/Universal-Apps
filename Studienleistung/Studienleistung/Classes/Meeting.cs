using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Studienleistung
{
    public class Meeting : INotifyPropertyChanged
    {
        public int _participants;
        public TimeSpan _elapsedTime;
        public double _cost;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int participants
        {
            get
            {
                return this._participants;
            }

            set
            {
                if (value != this._participants)
                {
                    this._participants = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double cost
        {
            get
            {
                return this._cost;
            }

            set
            {
                if (value != this._cost)
                {
                    this._cost = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TimeSpan elapsedTime
        {
            get
            {
                return this._elapsedTime;
            }

            set
            {
                if (value != this._elapsedTime)
                {
                    this._elapsedTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Meeting()
        {
            participants = 1;
        }
    }
}
