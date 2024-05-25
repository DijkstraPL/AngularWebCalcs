using Build_IT_NCalcPlayground.Events.AppEvents;
using Build_IT_NCalcPlayground.Events.Interfaces;
using Build_IT_NCalcPlayground.WpfExtensions;
using System;
using System.Windows.Input;

namespace Build_IT_NCalcPlayground.ViewModels
{
    public class InputParameterViewModel : Notifier
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                _inputDataChanged.Publish(this);
            }
        }

        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                SetProperty(ref _value, value);
                _inputDataChanged.Publish(this);
            }
        }

        private string _unit;
        public string Unit
        {
            get => _unit;
            set
            {
                SetProperty(ref _unit, value);
                _inputDataChanged.Publish(this);
            }
        }

        private ValueType _valueType = ValueType.ValueType;
        public ValueType ValueType
        {
            get => _valueType;
            set
            {
                SetProperty(ref _valueType, value);
                _inputDataChanged.Publish(this);
                OnPropertyChanged(nameof(HasUnit));
            }
        }

        public bool HasUnit => ValueType == ValueType.ValueType;

        public ICommand RemoveInputParameterCommand { get; }

        private readonly InputDataChangedEvent _inputDataChanged;
        private readonly IEventAggregator _eventAggregator;

        public InputParameterViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            RemoveInputParameterCommand = new RelayCommand(OnRemovingInputParameter);
            _inputDataChanged = _eventAggregator.GetEvent<InputDataChangedEvent>();
            _inputDataChanged.Publish(this);
        }

        private void OnRemovingInputParameter()
        {
            var inputParameterViewModelRemovedClickedEvent = _eventAggregator.GetEvent<InputParameterViewModelRemoveClickedEvent>();
            inputParameterViewModelRemovedClickedEvent.Publish(this);
            _inputDataChanged.Publish(this);
        }
    }
}
