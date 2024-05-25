using Build_IT_CommonTools.MatrixMath.Wrappers;
using Build_IT_FrameStatica.CalculationEngines.DirectStiffnessMethod.Frames.Interfaces;
using Build_IT_FrameStatica.CalculationEngines.DirectStiffnessMethod.Spans;
using Build_IT_FrameStatica.CalculationEngines.DirectStiffnessMethod.Spans.Interfaces;
using Build_IT_FrameStatica.CalculationEngines.Interfaces;
using Build_IT_FrameStatica.Frames;
using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Nodes;
using Build_IT_FrameStatica.Spans.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_FrameStatica.CalculationEngines.DirectStiffnessMethod.Frames
{
    internal class DirectStiffnessCalculationEngine : IFrameCalculationEngine
    {
        #region Fields

        private IFrame _frame;
        private IGlobalStiffnessMatrix _globalStiffnessMatrix;

        private IList<(ISpan span, ISpanCalculationEngine calculationEngine)> _spanCalculationEngines
            = new List<(ISpan, ISpanCalculationEngine)>();

        private VectorAdapter _jointLoadVector;
        private VectorAdapter _spanLoadVector;
        private VectorAdapter _deflectionVector;

        #endregion // Fields

        #region Constructors

        public DirectStiffnessCalculationEngine(IFrame frame, IGlobalStiffnessMatrix globalStiffnessMatrix = null)
        {
            _frame = frame;
            _globalStiffnessMatrix = globalStiffnessMatrix ?? new GlobalStiffnessMatrix(_frame, _spanCalculationEngines);
        }

        #endregion // Constructors

        #region Public_Methods

        public void Calculate()
        {
            SetSpanCalculationEngines();
            _frame.SetNumeration();
            CalculateStiffnessMatrixes();
            _globalStiffnessMatrix.Calculate();
            CaluclateJointLoadVector();
            CalculateSpanLoadVectors();
            CalculateSpanLoadVector();
            CalculateDeflectionVector();
            if (CheckDeflectionVector())
                throw new ArgumentException("Mechanism");
            CalculateDisplacements();
            CalculateForces();
            CalculateReactions();
            SetForces();
            SetDisplacements();
        }

        #endregion // Public_Methods

        #region Private_Methods

        private void SetSpanCalculationEngines()
        {
            foreach (var span in _frame.Spans)
                _spanCalculationEngines.Add((span, new SpanCalculationEngine(span)));
        }


        private void CalculateStiffnessMatrixes()
        {
            foreach (var spanEnginePair in _spanCalculationEngines)
                spanEnginePair.calculationEngine.StiffnessMatrix.Calculate();
        }

        private void CaluclateJointLoadVector()
        {
            if (_frame.NumberOfDegreesOfFreedom != 0)
                _jointLoadVector = VectorAdapter.Create(_frame.NumberOfDegreesOfFreedom);

            for (int i = 0; i < _frame.NumberOfDegreesOfFreedom; i++)
            {
                if (_frame.Nodes.Any(n => n.HorizontalMovementNumber == i))
                    _jointLoadVector[i] = _frame.Nodes.SingleOrDefault(n => n.HorizontalMovementNumber == i)?
                       .ConcentratedForces.Sum(cf => cf.CalculateJointLoadVectorNormalForceMember()) ?? 0;
                else if (_frame.Nodes.Any(n => n.VerticalMovementNumber == i))
                    _jointLoadVector[i] = _frame.Nodes.SingleOrDefault(n => n.VerticalMovementNumber == i)?
                        .ConcentratedForces.Sum(cf => cf.CalculateJointLoadVectorShearMember()) ?? 0;
                else
                    _jointLoadVector[i] = _frame.Nodes.SingleOrDefault(n => n.LeftRotationNumber == i || n.RightRotationNumber == i)?
                        .ConcentratedForces.Sum(cf => cf.CalculateJointLoadVectorBendingMomentMember()) ?? 0;
            }
        }

        private void CalculateSpanLoadVectors()
        {
            foreach (var spanEnginePair in _spanCalculationEngines)
                spanEnginePair.calculationEngine.CalculateSpanLoadVector();
        }

        private void CalculateSpanLoadVector()
        {
            if (_frame.NumberOfDegreesOfFreedom != 0)
                _spanLoadVector = VectorAdapter.Create(_frame.NumberOfDegreesOfFreedom);
            foreach (var spanEnginePair in _spanCalculationEngines)
                CalculateSpanLoadVectorForCurrentSpan(spanEnginePair);
        }

        private void CalculateDeflectionVector()
        {
            if (_frame.NumberOfDegreesOfFreedom != 0)
                _deflectionVector = _globalStiffnessMatrix.InversedMatrix * (_jointLoadVector - _spanLoadVector);
        }

        private bool CheckDeflectionVector()
            => _deflectionVector != null && _deflectionVector.Any(dv => double.IsNaN(dv));

        private void CalculateDisplacements()
        {
            foreach (var spanEnginePair in _spanCalculationEngines)
                spanEnginePair.calculationEngine.CalculateDisplacement(_deflectionVector, _frame.NumberOfDegreesOfFreedom);
        }

        private void CalculateForces()
        {
            foreach (var spanEnginePair in _spanCalculationEngines)
                spanEnginePair.calculationEngine.CalculateForce(
                    spanEnginePair.calculationEngine.LoadVector,
                    spanEnginePair.calculationEngine.Displacements);
        }

        private void CalculateReactions()
        {
            int numberOfReactions = _frame.Spans.Count * 3 + 3 - _frame.NumberOfDegreesOfFreedom;

            numberOfReactions += _frame.Nodes.Count(n => n is Hinge);

            for (int i = _frame.NumberOfDegreesOfFreedom; i < numberOfReactions + _frame.NumberOfDegreesOfFreedom; i++)
            {
                SetLeftNodeReactions(i);
                SetRightNodeReactions(i);
            }
        }

        private void SetLeftNodeReactions(int i)
        {
            if (_frame.Spans.FirstOrDefault(s => s.LeftNode.HorizontalMovementNumber == i)?.LeftNode.HorizontalForce != null)
                _frame.Spans.FirstOrDefault(s => s.LeftNode.HorizontalMovementNumber == i).LeftNode.HorizontalForce.Value
                    += _spanCalculationEngines.Where(sep => sep.span.LeftNode.HorizontalMovementNumber == i)
                    .Sum(sep => sep.calculationEngine.Forces[0]);

            if (_frame.Spans.FirstOrDefault(s => s.LeftNode.VerticalMovementNumber == i)?.LeftNode.VerticalForce != null)
                _frame.Spans.FirstOrDefault(s => s.LeftNode.VerticalMovementNumber == i).LeftNode.VerticalForce.Value
                    += _spanCalculationEngines.Where(sep => sep.span.LeftNode.VerticalMovementNumber == i)
                    .Sum(sep => sep.calculationEngine.Forces[1]);

            if (_frame.Spans.FirstOrDefault(s => s.LeftNode.RightRotationNumber == i)?.LeftNode.BendingMoment != null)
                _frame.Spans.FirstOrDefault(s => s.LeftNode.RightRotationNumber == i).LeftNode.BendingMoment.Value
                    -= _spanCalculationEngines.Where(sep => sep.span.LeftNode.RightRotationNumber == i)
                    .Sum(sep => sep.calculationEngine.Forces[2]);
        }

        private void SetRightNodeReactions(int i)
        {
            if (_frame.Spans.FirstOrDefault(s => s.RightNode.HorizontalMovementNumber == i)?.RightNode.HorizontalForce != null)
                _frame.Spans.FirstOrDefault(s => s.RightNode.HorizontalMovementNumber == i).RightNode.HorizontalForce.Value
                    += _spanCalculationEngines.Where(sep => sep.span.RightNode.HorizontalMovementNumber == i)
                    .Sum(sep => sep.calculationEngine.Forces[3]);

            if (_frame.Spans.FirstOrDefault(s => s.RightNode.VerticalMovementNumber == i)?.RightNode.VerticalForce != null)
                _frame.Spans.FirstOrDefault(s => s.RightNode.VerticalMovementNumber == i).RightNode.VerticalForce.Value
                    += _spanCalculationEngines.Where(sep => sep.span.RightNode.VerticalMovementNumber == i)
                    .Sum(sep => sep.calculationEngine.Forces[4]);

            if (_frame.Spans.FirstOrDefault(s => s.RightNode.LeftRotationNumber == i)?.RightNode.BendingMoment != null)
                _frame.Spans.FirstOrDefault(s => s.RightNode.LeftRotationNumber == i).RightNode.BendingMoment.Value
                    -= _spanCalculationEngines.Where(sep => sep.span.RightNode.LeftRotationNumber == i)
                    .Sum(sep => sep.calculationEngine.Forces[5]);
        }


        private void CalculateSpanLoadVectorForCurrentSpan
            ((ISpan span, ISpanCalculationEngine calculationEngine) spanEnginePair)
        {
            for (int i = 0; i < _frame.NumberOfDegreesOfFreedom; i++)
            {
                if (spanEnginePair.span.LeftNode.HorizontalMovementNumber == i)
                    _spanLoadVector[i] += spanEnginePair.calculationEngine.LoadVector[0];
                else if (spanEnginePair.span.LeftNode.VerticalMovementNumber == i)
                    _spanLoadVector[i] += spanEnginePair.calculationEngine.LoadVector[1];
                else if (spanEnginePair.span.LeftNode.RightRotationNumber == i)
                    _spanLoadVector[i] += spanEnginePair.calculationEngine.LoadVector[2];
                else if (spanEnginePair.span.RightNode.HorizontalMovementNumber == i)
                    _spanLoadVector[i] += spanEnginePair.calculationEngine.LoadVector[3];
                else if (spanEnginePair.span.RightNode.VerticalMovementNumber == i)
                    _spanLoadVector[i] += spanEnginePair.calculationEngine.LoadVector[4];
                else if (spanEnginePair.span.RightNode.LeftRotationNumber == i)
                    _spanLoadVector[i] += spanEnginePair.calculationEngine.LoadVector[5];
            }
        }

        private void SetForces()
        {
            foreach (var spanCalculationEngine in _spanCalculationEngines)
            {
                spanCalculationEngine.span.LeftForces.NormalForce += spanCalculationEngine.calculationEngine.Forces[0] * spanCalculationEngine.span.GetLambdaX();
                spanCalculationEngine.span.LeftForces.NormalForce += spanCalculationEngine.calculationEngine.Forces[1] * spanCalculationEngine.span.GetLambdaY();
                spanCalculationEngine.span.LeftForces.ShearForce += spanCalculationEngine.calculationEngine.Forces[1] * spanCalculationEngine.span.GetLambdaX();
                spanCalculationEngine.span.LeftForces.ShearForce += spanCalculationEngine.calculationEngine.Forces[0] * -spanCalculationEngine.span.GetLambdaY();
                spanCalculationEngine.span.LeftForces.BendingMoment -= spanCalculationEngine.calculationEngine.Forces[2];

                spanCalculationEngine.span.RightForces.NormalForce += spanCalculationEngine.calculationEngine.Forces[3] * spanCalculationEngine.span.GetLambdaX();
                spanCalculationEngine.span.RightForces.NormalForce += spanCalculationEngine.calculationEngine.Forces[4] * spanCalculationEngine.span.GetLambdaY();
                spanCalculationEngine.span.RightForces.ShearForce += spanCalculationEngine.calculationEngine.Forces[4] * spanCalculationEngine.span.GetLambdaX();
                spanCalculationEngine.span.RightForces.ShearForce += spanCalculationEngine.calculationEngine.Forces[3] * -spanCalculationEngine.span.GetLambdaY();
                spanCalculationEngine.span.RightForces.BendingMoment -= spanCalculationEngine.calculationEngine.Forces[5];
            }
        }
        
        private void SetDisplacements()
        {
            foreach (var spanCalculationEngine in _spanCalculationEngines)
            {
                spanCalculationEngine.span.LeftDisplacements.NormalDeflection += spanCalculationEngine.calculationEngine.Displacements[0] * spanCalculationEngine.span.GetLambdaX() * 1000;
                spanCalculationEngine.span.LeftDisplacements.NormalDeflection += spanCalculationEngine.calculationEngine.Displacements[1] * spanCalculationEngine.span.GetLambdaY() * 1000;
                spanCalculationEngine.span.LeftDisplacements.ShearDeflection += spanCalculationEngine.calculationEngine.Displacements[1] * spanCalculationEngine.span.GetLambdaX() * 1000;
                spanCalculationEngine.span.LeftDisplacements.ShearDeflection += spanCalculationEngine.calculationEngine.Displacements[0] * -spanCalculationEngine.span.GetLambdaY() * 1000;
                spanCalculationEngine.span.LeftDisplacements.Rotation -= spanCalculationEngine.calculationEngine.Displacements[2];

                spanCalculationEngine.span.RightDisplacements.NormalDeflection += spanCalculationEngine.calculationEngine.Displacements[3] * spanCalculationEngine.span.GetLambdaX() * 1000;
                spanCalculationEngine.span.RightDisplacements.NormalDeflection += spanCalculationEngine.calculationEngine.Displacements[4] * spanCalculationEngine.span.GetLambdaY() * 1000;
                spanCalculationEngine.span.RightDisplacements.ShearDeflection += spanCalculationEngine.calculationEngine.Displacements[4] * spanCalculationEngine.span.GetLambdaX() * 1000;
                spanCalculationEngine.span.RightDisplacements.ShearDeflection += spanCalculationEngine.calculationEngine.Displacements[3] * -spanCalculationEngine.span.GetLambdaY() * 1000;
                spanCalculationEngine.span.RightDisplacements.Rotation -= spanCalculationEngine.calculationEngine.Displacements[5];
            }
        }


        #endregion // Private_Methods
    }

}
