using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Results.Displacements;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Spans.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_FrameStatica.Results.OnSpan
{
    internal class ResultsContainer : IResultsContainer
    {
        #region Properties

        public IGetResult NormalForce { get; }
        public IGetResult Shear { get; }
        public IGetResult BendingMoment { get; }
        public IGetResult NormalDeflection { get; }
        public IGetResult ShearDeflection { get; }
        public IGetResult Rotation { get; }

        #endregion // Properties

        #region Fields

        private IFrame _frame;

        #endregion // Fields

        #region Constructors

        public ResultsContainer(IFrame frame)
        {
            _frame = frame;

            NormalForce = new NormalForceResult(_frame);
            Shear = new ShearResult(_frame);
            BendingMoment = new BendingMomentResult(_frame);
            NormalDeflection = new HorizontalDeflectionResult(_frame);
            ShearDeflection = new VerticalDeflectionResult(_frame);
            Rotation = new RotationResult(_frame);
        }

        #endregion // Constructors

        #region Public_Methods

        public IResultValue GetHorizontalDeflection(double position, short spanNumber)
        {
            ISpan span = _frame.Spans.First(s => s.Number == spanNumber);

            var normalDeflection = NormalDeflection.GetValue(position, spanNumber);
            var shearDeflection = ShearDeflection.GetValue(position, spanNumber);

            return new Displacement(span, position) 
            {
                Value = normalDeflection.Value * span.GetLambdaX() + shearDeflection.Value *- span.GetLambdaY() 
            };
        }

        public IResultValue GetVerticalDeflection(double position, short spanNumber)
        {
            ISpan span = _frame.Spans.First(s => s.Number == spanNumber);

            var normalDeflection = NormalDeflection.GetValue(position, spanNumber);
            var shearDeflection = ShearDeflection.GetValue(position, spanNumber);

            return new Displacement(span, position) 
            { 
                Value = shearDeflection.Value * span.GetLambdaX() + normalDeflection.Value * span.GetLambdaY() 
            };
        }
        #endregion // Public_Methods
    }
}
