using Build_IT_NCalc.Units;
using Build_IT_NCalcPlayground.Events.AppEvents;
using Build_IT_NCalcPlayground.Events.Interfaces;
using Build_IT_NCalcPlayground.ViewModels.Interfaces;
using Build_IT_NCalcPlayground.WpfExtensions;
using NCalc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Build_IT_NCalcPlayground.ViewModels
{
    public class MainWindowViewModel : Notifier, IMainWindowViewModel
    {
        private readonly ObservableCollection<InputParameterViewModel> _inputParameterViewModels = new ObservableCollection<InputParameterViewModel>();
        public IEnumerable<InputParameterViewModel> InputParameterViewModels => _inputParameterViewModels;

        public ICommand AddInputParameterCommand { get; }
        public ICommand CopyTestDataCommand { get; }
        public ICommand AddTestDataCommand { get; }

        private readonly IEventAggregator _eventAggregator;

        private string _equation;
        public string Equation
        {
            get => _equation;
            set
            {
                SetProperty(ref _equation, value);
                Calculate();
            }
        }

        private string _result;
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        private string _remarks;
        public string Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }

        private long _calculationTime;
        public long CalculationTime
        { 
            get => _calculationTime; 
            private set => SetProperty(ref _calculationTime, value);
        }

        private readonly Stopwatch _stopwatch = new Stopwatch();

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            AddInputParameterCommand = new RelayCommand(AddNewInputParameterViewModel);
            CopyTestDataCommand = new RelayCommand(CopyTestData);
            AddTestDataCommand = new RelayCommand(AddTestData);

            var inputParameterViewModelRemoveClickedEvent = _eventAggregator.GetEvent<InputParameterViewModelRemoveClickedEvent>();
            inputParameterViewModelRemoveClickedEvent.Subscribe(RemoveInputParameterViewModel);
            var inputDataChanged = _eventAggregator.GetEvent<InputDataChangedEvent>();
            inputDataChanged.Subscribe(OnInputDataChanged);
        }

        private void OnInputDataChanged(InputParameterViewModel obj)
        {
            Calculate();
        }

        private void RemoveInputParameterViewModel(InputParameterViewModel inputParameterViewModel)
        {
            _inputParameterViewModels.Remove(inputParameterViewModel);
        }

        private void AddNewInputParameterViewModel()
        {
            _inputParameterViewModels.Add(new InputParameterViewModel(_eventAggregator));
        }

        private void Calculate()
        {
            _stopwatch.Start();
            try
            {
                    var expr = new NCalc.Expression(Equation, EvaluateOptions.AllowUnitCalculations);

                    foreach (var ipvm in _inputParameterViewModels)
                    {
                        switch (ipvm.ValueType)
                        {
                            case ValueType.Unknown:
                                throw new NotSupportedException();
                            case ValueType.Double:
                                double val = Convert.ToDouble(ipvm.Value);
                                expr.AddParameter(ipvm.Name, val);
                                break;
                            case ValueType.ValueType:
                                double val2 = Convert.ToDouble(ipvm.Value);
                                expr.AddParameter(ipvm.Name, new ValueUnit(val2, ipvm.Unit.Split(',', StringSplitOptions.RemoveEmptyEntries)));
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }

                    var sut = expr.ToLambda<object>();
                    var result = sut();

                    if (result is ValueUnit valueUnitResult)
                        valueUnitResult.OrganizeUnits();

                    Result = result.ToString();
            }
            catch
            {
                Result = "Wrong data!";
            }
            finally
            {
                _stopwatch.Stop();
                CalculationTime = _stopwatch.ElapsedMilliseconds;
                _stopwatch.Reset();
            }
        }

        private void CopyTestData()
        {
            var generatedCode = GenerateCode();

            Clipboard.SetText(generatedCode);
        }

        private void AddTestData()
        {
            var generatedCode = GenerateCode();
            const string endLineCode = "//EndLine - do not remove";

            string text = File.ReadAllText(@"D:\Projects\KPK_Calcs\Build_IT_NCalcTests\GeneratedTests\TestDataGenerator.cs");
            MatchCollection matches = Regex.Matches(text, "yield return");
            int count = matches.Count;

            text = text.Replace(endLineCode, $"/* {count + 1} {Remarks}*/ " + generatedCode + "\n" + endLineCode);
            File.WriteAllText(@"D:\Projects\KPK_Calcs\Build_IT_NCalcTests\GeneratedTests\TestDataGenerator.cs", text);
        }

        private string GenerateCode()
        {
            StringBuilder dataSet = new StringBuilder();

            dataSet.Append("yield return new object[] { ").Append("unitRegistry, ")
                .Append($"\"{Equation}\"").Append(", ")
                .Append($"\"{Result}\"").Append(", ");

            foreach (InputParameterViewModel inputParameterViewModel in InputParameterViewModels)
            {
                dataSet.Append("new TestDataInputParameter(");
                switch (inputParameterViewModel.ValueType)
                {
                    case ValueType.Unknown:
                        throw new NotSupportedException();
                    case ValueType.Double:
                        dataSet.Append($"\"{inputParameterViewModel.Name}\", ")
                            .Append(inputParameterViewModel.Value).Append("), ");
                        break;
                    case ValueType.ValueType:
                        dataSet.Append($"\"{inputParameterViewModel.Name}\", ")
                            .Append(inputParameterViewModel.Value).Append(", ")
                            .Append($"\"{inputParameterViewModel.Unit}\"").Append(", ")
                            .Append("unitRegistry").Append("), ");
                        break;
                    default: 
                        throw new NotImplementedException();
                }
            }
            dataSet.Append("};");

            return dataSet.ToString();
        }
    }
}
