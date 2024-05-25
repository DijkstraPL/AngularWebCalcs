using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Build_IT_NCalcPlayground.ViewModels
{
    public abstract class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetProperty<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}