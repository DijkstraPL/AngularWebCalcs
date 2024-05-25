using Build_IT_FrameStatica.CalculationEngines.DirectStiffnessMethod;
using Build_IT_FrameStatica.CalculationEngines.DirectStiffnessMethod.Frames;
using Build_IT_FrameStatica.CalculationEngines.Interfaces;
using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Nodes;
using Build_IT_FrameStatica.Nodes.Interfaces;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Results.OnSpan;
using Build_IT_FrameStatica.Spans;
using Build_IT_FrameStatica.Spans.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Build_IT_FrameStatica.Frames.Interfaces.IFrame;

[assembly: InternalsVisibleTo("Build_IT_FrameStaticaTests")]
[assembly: InternalsVisibleTo("Build_IT_FrameStaticaAcceptanceTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Build_IT_FrameStatica.Frames
{
    public class Frame : IFrame
    {
        #region Properties

        public short NumberOfDegreesOfFreedom { get; private set; }

        public IList<ISpan> Spans { get; }
        public ICollection<INode> Nodes { get; }

        public IFrameCalculationEngine CalculationEngine { get; private set; }

        public bool IncludeSelfWeight => throw new NotImplementedException();
        public IResultsContainer Results { get;  }

        #endregion // Properties

        #region Constructors

        public Frame(IList<ISpan> spans, ICollection<INode> nodes)
        {
            Spans = spans ?? throw new ArgumentNullException(nameof(spans));
            Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));

            CalculationEngine = new DirectStiffnessCalculationEngine(this);
            Results = new ResultsContainer(this);
        }

        #endregion // Constructors

        #region Public_Methods

        public void SetNumeration()
        {
            short spanCounter = 0;
            short nodeCounter = 0;

            spanCounter = SetSpanNumeration(spanCounter);
            nodeCounter = SetNodeNumeration(nodeCounter);

            SetNumberOfDegreesOfFreedom();
        }

        public void AddHingeToSpan(ISpan span, Side side)
        {
            int index = Spans.IndexOf(span);
            if (!Spans.Remove(span))
                throw new ArgumentException("Can't find span to remove.");
            INode hingeNode = null;
            switch (side)
            {
                case Side.Left:
                    var pointLeft = span.LeftNode.Position.GetPointAtDistance(span.RightNode.Position, 0.002);
                    hingeNode = new Hinge(pointLeft);
                    break;
                case Side.Right:
                    var pointRight = span.RightNode.Position.GetPointAtDistance(span.LeftNode.Position, 0.002);
                    hingeNode = new Hinge(pointRight);
                    break;
                default:
                    throw new NotImplementedException();
            }

            Nodes.Add(hingeNode);

            var span1 = new Span(span.LeftNode, hingeNode, span.Material, span.Section, span.IncludeSelfWeight);
            var span2 = new Span(hingeNode, span.RightNode, span.Material, span.Section, span.IncludeSelfWeight);
            var longerSpan = span1.Length > span2.Length ? span1 : span2;
            var shorterSpan = span1.Length > span2.Length ? span2 : span1;

            foreach (var pointLoad in span.PointLoads)
                longerSpan.PointLoads.Add(pointLoad);
            foreach (var continousLoad in span.ContinousLoads)
                longerSpan.ContinousLoads.Add(continousLoad);

            Spans.Insert(index, longerSpan);
            Spans.Add(shorterSpan);
        }

        #endregion // Public_Methods

        #region Private_Methods

        private void SetNumberOfDegreesOfFreedom()
        {
            foreach (var node in Nodes)
                NumberOfDegreesOfFreedom += node.DegreesOfFreedom;
        }

        private short SetSpanNumeration(short spanCounter)
        {
            foreach (var span in Spans)
                span.Number = spanCounter++;
            return spanCounter;
        }

        private short SetNodeNumeration(short nodeCounter)
        {
            foreach (var node in Nodes)
                node.SetDisplacementNumeration(ref nodeCounter);
            foreach (var node in Nodes)
                node.SetReactionNumeration(ref nodeCounter);
            return nodeCounter;
        }

        #endregion // Private_Methods
    }
}
