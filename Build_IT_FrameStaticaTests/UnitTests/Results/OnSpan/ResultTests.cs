using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Results.Interfaces;
using Build_IT_FrameStatica.Results.OnSpan;
using Build_IT_FrameStatica.Spans.Interfaces;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_FrameStaticaTests.UnitTests.Results.OnSpan
{
    [TestFixture]
    public class ResultTests
    {
        public const string MethodName = "CalculateAtPosition";

        [Test]
        public void GetMaxValueTest()
        {
            var value1 = Mock.Of<IResultValue>(v => v.Value == 1);
            var value2 = Mock.Of<IResultValue>(v => v.Value == 2);
            var value3 = Mock.Of<IResultValue>(v => v.Value == 3);

            var span = Mock.Of<ISpan>(s => s.Length == 0.02);
            var frame = Mock.Of<IFrame>(f => f.Spans == new List<ISpan> { span });

            var result = new Mock<Result>(frame);
            result.Protected().Setup<IResultValue>(MethodName, span, 0.0).Returns(value1);
            result.Protected().Setup<IResultValue>(MethodName, span, 0.01).Returns(value2);
            result.Protected().Setup<IResultValue>(MethodName, span, 0.02).Returns(value3);
        
            Assert.AreSame(value3, result.Object.GetMaxValue());
        }

        [Test]
        public void GetMinValueTest()
        {
            var value1 = Mock.Of<IResultValue>(v => v.Value == 1);
            var value2 = Mock.Of<IResultValue>(v => v.Value == 2);
            var value3 = Mock.Of<IResultValue>(v => v.Value == 3);

            var span = Mock.Of<ISpan>(s => s.Length == 0.02);
            var frame = Mock.Of<IFrame>(f => f.Spans == new List<ISpan> { span });

            var result = new Mock<Result>(frame);
            result.Protected().Setup<IResultValue>(MethodName, span, 0.0).Returns(value1);
            result.Protected().Setup<IResultValue>(MethodName, span, 0.01).Returns(value2);
            result.Protected().Setup<IResultValue>(MethodName, span, 0.02).Returns(value3);

            Assert.AreSame(value1, result.Object.GetMinValue());
        }
    }
}
