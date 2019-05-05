using System.ComponentModel;

namespace WpfBinding
{
    class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    class Model : NotificationObject
    {
        private string _MyProperty = "Hello!";

        public string Myproperty
        {
            get { return _MyProperty; }
            set
            {
                _MyProperty = value;
                this.RaisePropertyChanged("Myproperty");
            }
        }

        public void Copy(object obj)
        {
            this.Myproperty += " en,Hello!";
        }
    }
}