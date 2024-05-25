using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Spans.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_FrameStatica.Results.OnSpan
{
    internal abstract class Result : IGetResult
    {
        #region Properties

        public ICollection<IResultValue> Values { get; private set; }
        protected IList<ISpan> Spans { get; }
        protected IFrame Frame { get; }

        #endregion // Properties

        #region Fields

        protected const double tick = 100;

        #endregion // Fields

        #region Constructors

        protected Result(IFrame frame)
        {
            Frame = frame ?? throw new ArgumentNullException(nameof(frame));
            Spans = frame.Spans;
        }

        #endregion // Constructors

        #region Public_Methods

        public void SetValues()
        {
            Values = new List<IResultValue>();

            foreach (var span in Spans)
                for (int i = 0; i <= span.Length * tick; i++)
                    Values.Add(CalculateAtPosition(span, i / tick));
        }
               
        public IResultValue GetMaxValue()
        {
            CheckValuesIfNeeded();

            return Values.Aggregate((v1, v2) => v1.Value > v2.Value ? v1 : v2);
        }

        public IResultValue GetMaxValue(short[] spanNumbers, double startPosition = 0, double? endPosition = null)
        {
            CheckValuesIfNeeded();
            var filteredValues = SetFilteredValues(spanNumbers, startPosition, endPosition);

            return filteredValues.Aggregate((v1, v2) => v1.Value > v2.Value ? v1 : v2);
        }

        public IResultValue GetMinValue()
        {
            CheckValuesIfNeeded();

            return Values.Aggregate((v1, v2) => v1.Value < v2.Value ? v1 : v2);
        }

        public IResultValue GetMinValue(short[] spanNumbers, double startPosition = 0, double? endPosition = null)
        {
            CheckValuesIfNeeded();
            var filteredValues = SetFilteredValues(spanNumbers, startPosition, endPosition);

            return filteredValues.Aggregate((v1, v2) => v1.Value < v2.Value ? v1 : v2);
        }

        public IResultValue GetValue(double distanceFromLeftSide, short spanNumber)
        {
            if (Spans.Count <= spanNumber)
                throw new IndexOutOfRangeException(nameof(spanNumber));
            var span = Spans.First(s => s.Number == spanNumber);
            if (span.Length < distanceFromLeftSide)
                throw new ArgumentOutOfRangeException(nameof(distanceFromLeftSide));
            return CalculateAtPosition(span, distanceFromLeftSide);
        }

        #endregion // Public_Methods

        #region Protected_Methods

        protected abstract IResultValue CalculateAtPosition(ISpan span, double distanceFromLeftSide);

        #endregion // Protected_Methods

        #region Private_Methods

        private IEnumerable<IResultValue> SetFilteredValues(short[] spanNumbers, double startPosition, double? endPosition)
        {
            var values = Values.Where(v => spanNumbers.Contains(v.Span.Number));
            if (startPosition == 0 && endPosition == null)
                return values;
            return values.Where(r => r.Position >= startPosition && r.Position <= endPosition);
        }

        private void CheckValuesIfNeeded()
        {
            if (Values == null)
                SetValues();
        }

        #endregion // Private_Methods
    }
}
