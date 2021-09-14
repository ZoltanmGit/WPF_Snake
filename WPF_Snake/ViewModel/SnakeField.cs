using System;

namespace WPF_Snake.ViewModel
{
    public class SnakeField : ViewModelBase
    {
        private Int32 _number;
        public Int32 Number 
        {
            get
            {
                return _number;
            }
            set
            {
                if(_number != value)
                {
                    _number = value;
                    OnPropertyChanged("Number");
                }
            }
        }
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
    }
}
