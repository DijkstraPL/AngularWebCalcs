using Build_IT_ScriptInterpreter.Formatters;
using Build_IT_ScriptInterpreter.Formatters.Marks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreterTests
{
    [TestFixture]
    public class MathMLFormatterTests
    {
        [Test]
        [TestCase("50*2+2.2", "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mn>50</mn><mo>&#xB7;</mo><mn>2</mn><mo>+</mo><mn>2.2</mn></math>")]
        [TestCase("(15+2)*Pow(7,2)", "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mo>(</mo><mn>15</mn><mo>+</mo><mn>2</mn><mo>)</mo><mo>&#xB7;</mo><msup><mrow><mn>7</mn></mrow><mrow><mn>2</mn></mrow></msup></math>")]
        [TestCase("[b]*Pow([h],3)/12", "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mi>b</mi><mo>&#xB7;</mo><msup><mrow><mi>h</mi></mrow><mrow><mn>3</mn></mrow></msup><mo>/</mo><mn>12</mn></math>")]
        [TestCase("Pow([f_cm_(t)]/[f_cm_],0.3)*[E_cm_]", "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><msup><mrow><mfenced><mrow><mrow><msub><mrow><mi>f</mi></mrow><mrow><mi>cm</mi></mrow></msub><mi>(t)</mi></mrow><mo>/</mo><mrow><msub><mrow><mi>f</mi></mrow><mrow><mi>cm</mi></mrow></msub></mrow></mrow></mfenced></mrow><mrow><mn>0.3</mn></mrow></msup><mo>&#xB7;</mo><mrow><msub><mrow><mi>E</mi></mrow><mrow><mi>cm</mi></mrow></msub></mrow></math>")]
        [TestCase("sqrt(235/[f_y_])", "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><msqrt><mrow><mn>235</mn><mo>/</mo><mrow><msub><mrow><mi>f</mi></mrow><mrow><mi>y</mi></mrow></msub></mrow></mrow></msqrt></math>")]
        [TestCase("if([t]<28,1,2/3)", "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mfenced open=\"{\" close=\"\"><mtable columnspacing=\"1.4ex\" columnalign=\"left\"><mtr><mtd><mi>if&#xA0;</mi><mrow><mi>t</mi><mo>&lt;</mo><mn>28</mn></mrow></mtd><mtd><mrow><mn>1</mn></mrow></mtd></mtr><mtr><mtd><mi>else&#xA0;</mi></mtd><mtd><mrow><mn>2</mn><mo>/</mo><mn>3</mn></mrow></mtd></mtr></mtable></mfenced></math>")]
        [TestCase("Round(22*Pow(0.1*[f_cm_],0.3),0)", "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mn>22</mn><mo>&#xB7;</mo><msup><mrow><mfenced><mrow><mn>0.1</mn><mo>&#xB7;</mo><mrow><msub><mrow><mi>f</mi></mrow><mrow><mi>cm</mi></mrow></msub></mrow></mrow></mfenced></mrow><mrow><mn>0.3</mn></mrow></msup></math>")]
        [TestCase("if([Type] = 'Rolled', [h] - 2 * [t_f_] - 2 * [r], [h] - 2 * [t_f_] - 2 * [a] * sqrt(2))", "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mfenced open=\"{\" close=\"\"><mtable columnspacing=\"1.4ex\" columnalign=\"left\"><mtr><mtd><mi>if&#xA0;</mi><mrow><mi>Type</mi><mo>=</mo><mtext>Rolled</mtext></mrow></mtd><mtd><mrow><mi>h</mi><mo>-</mo><mn>2</mn><mo>&#xB7;</mo><mrow><msub><mrow><mi>t</mi></mrow><mrow><mi>f</mi></mrow></msub></mrow><mo>-</mo><mn>2</mn><mo>&#xB7;</mo><mi>r</mi></mrow></mtd></mtr><mtr><mtd><mi>else&#xA0;</mi></mtd><mtd><mrow><mi>h</mi><mo>-</mo><mn>2</mn><mo>&#xB7;</mo><mrow><msub><mrow><mi>t</mi></mrow><mrow><mi>f</mi></mrow></msub></mrow><mo>-</mo><mn>2</mn><mo>&#xB7;</mo><mi>a</mi><mo>&#xB7;</mo><msqrt><mrow><mn>2</mn></mrow></msqrt></mrow></mtd></mtr></mtable></mfenced></math>")]
        [TestCase("if(in([cement_type_],'CEM 42,5R','CEM 52,5N', 'CEM 52,5R') == true,0.2,if(in([cement_type_],'CEM 32,5R','CEM 42,5') == true,0.25,if(in([cement_type_],'CEM 32,5N') == true,0.38, ERROR('Invalid cement type.'))))", "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mfenced open=\"{\" close=\"\"><mtable columnspacing=\"1.4ex\" columnalign=\"left\"><mtr><mtd><mi>if&#xA0;</mi><mrow><mi>in</mi><mfenced><mrow><mrow><msub><mrow><mi>cement</mi></mrow><mrow><mi>type</mi></mrow></msub></mrow></mrow><mrow><mtext>CEM 42,5R</mtext></mrow><mrow><mtext>CEM 52,5N</mtext></mrow><mrow><mtext>CEM 52,5R</mtext></mrow></mfenced><mo>=</mo><mtext>true</mtext></mrow></mtd><mtd><mrow><mn>0.2</mn></mrow></mtd></mtr><mtr><mtd><mi>else&#xA0;if&#xA0;</mi><mrow><mi>in</mi><mfenced><mrow><mrow><msub><mrow><mi>cement</mi></mrow><mrow><mi>type</mi></mrow></msub></mrow></mrow><mrow><mtext>CEM 32,5R</mtext></mrow><mrow><mtext>CEM 42,5</mtext></mrow></mfenced><mo>=</mo><mtext>true</mtext></mrow></mtd><mtd><mrow><mn>0.25</mn></mrow></mtd></mtr><mtr><mtd><mi>else&#xA0;if&#xA0;</mi><mrow><mi>in</mi><mfenced><mrow><mrow><msub><mrow><mi>cement</mi></mrow><mrow><mi>type</mi></mrow></msub></mrow></mrow><mrow><mtext>CEM 32,5N</mtext></mrow></mfenced><mo>=</mo><mtext>true</mtext></mrow></mtd><mtd><mrow><mn>0.38</mn></mrow></mtd></mtr><mtr><mtd><mi>else&#xA0;</mi></mtd><mtd><mrow><mi>error</mi><mfenced><mrow><mtext>Invalid cement type.</mtext></mrow></mfenced></mrow></mtd></mtr></mtable></mfenced></math>")]
        public void FormatEquation(string equation, string expectedMathML)
        {
            string value = equation;

            var mathMLFormatter = new MathMLFormatter();

            var result = mathMLFormatter.GetMathML(value);
            var formattedResult = result.GetMarks().First().ToString()
                .Replace("\n", string.Empty)
                .Replace("  ", string.Empty);

            Assert.That(formattedResult, Is.EqualTo(expectedMathML));
        }

        [Test]
        public void EquationWithSeveralLinesTest()
        {
            string value = @"if([f_cm_]<=35,
1+(1-[RH]/100)/(0.1*Pow([h_0_],1/3)),
(1+(1-[RH]/100)/(0.1*Pow([h_0_],1/3))*[α_1_])*[α_2_])";

            var mathMLFormatter = new MathMLFormatter();

            var result = mathMLFormatter.GetMathML(value);
            var formattedResult = result.GetMarks().First().ToString()
                .Replace("\n", string.Empty)
                .Replace("  ", string.Empty);

            Assert.That(formattedResult, Is.EqualTo("<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mfenced open=\"{\" close=\"\"><mtable columnspacing=\"1.4ex\" columnalign=\"left\"><mtr><mtd><mi>if&#xA0;</mi><mrow><mrow><msub><mrow><mi>f</mi></mrow><mrow><mi>cm</mi></mrow></msub></mrow><mo>&#x2264;</mo><mn>35</mn></mrow></mtd><mtd><mrow><mn>1</mn><mo>+</mo><mo>(</mo><mn>1</mn><mo>-</mo><mi>RH</mi><mo>/</mo><mn>100</mn><mo>)</mo><mo>/</mo><mo>(</mo><mn>0.1</mn><mo>&#xB7;</mo><msup><mrow><mrow><msub><mrow><mi>h</mi></mrow><mrow><mi>0</mi></mrow></msub></mrow></mrow><mrow><mn>1</mn><mo>/</mo><mn>3</mn></mrow></msup><mo>)</mo></mrow></mtd></mtr><mtr><mtd><mi>else&#xA0;</mi></mtd><mtd><mrow><mo>(</mo><mn>1</mn><mo>+</mo><mo>(</mo><mn>1</mn><mo>-</mo><mi>RH</mi><mo>/</mo><mn>100</mn><mo>)</mo><mo>/</mo><mo>(</mo><mn>0.1</mn><mo>&#xB7;</mo><msup><mrow><mrow><msub><mrow><mi>h</mi></mrow><mrow><mi>0</mi></mrow></msub></mrow></mrow><mrow><mn>1</mn><mo>/</mo><mn>3</mn></mrow></msup><mo>)</mo><mo>&#xB7;</mo><mrow><msub><mrow><mi>α</mi></mrow><mrow><mi>1</mi></mrow></msub></mrow><mo>)</mo><mo>&#xB7;</mo><mrow><msub><mrow><mi>α</mi></mrow><mrow><mi>2</mi></mrow></msub></mrow></mrow></mtd></mtr></mtable></mfenced></math>"));
        }

        //[Test]
        //public void Equation()
        //{
        //    string value = "if(in([cement_type_],'CEM 42,5R','CEM 52,5N', 'CEM 52,5R') == true,0.2,if(in([cement_type_],'CEM 32,5R','CEM 42,5') == true,0.25,if(in([cement_type_],'CEM 32,5N') == true,0.38, ERROR('Invalid cement type.'))))";

        //    var mathMLFormatter = new MathMLFormatter();

        //    var result = mathMLFormatter.GetMathML(value);
        //    var formattedResult = result.ToString().Replace("\n", string.Empty);

        //    Assert.That(formattedResult, Is.EqualTo("<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mfenced open=\"{\" close=\"\"><mtable columnspacing=\"1.4ex\" columnalign=\"left\"><mtr><mtd><mi>if&#xA0;</mi><mrow><mi>in</mi><mfenced><mrow><mrow><msub><mrow><mi>cement</mi></mrow><mrow><mi>type</mi></mrow></msub></mrow></mrow><mrow><mtext>CEM42,5R</mtext></mrow><mrow><mtext>CEM52,5N</mtext></mrow><mrow><mtext>CEM52,5R</mtext></mrow></mfenced><mo>=</mo><mtext>true</mtext></mrow></mtd><mtd><mrow><mn>0.2</mn></mrow></mtd></mtr><mtr><mtd><mi>else&#xA0;</mi></mtd><mtd><mrow><mfenced open=\"{\" close=\"\"><mtable columnspacing=\"1.4ex\" columnalign=\"left\"><mtr><mtd><mi>if&#xA0;</mi><mrow><mi>in</mi><mfenced><mrow><mrow><msub><mrow><mi>cement</mi></mrow><mrow><mi>type</mi></mrow></msub></mrow></mrow><mrow><mtext>CEM32,5R</mtext></mrow><mrow><mtext>CEM42,5</mtext></mrow></mfenced><mo>=</mo><mtext>true</mtext></mrow></mtd><mtd><mrow><mn>0.25</mn></mrow></mtd></mtr><mtr><mtd><mi>else&#xA0;</mi></mtd><mtd><mrow><mfenced open=\"{\" close=\"\"><mtable columnspacing=\"1.4ex\" columnalign=\"left\"><mtr><mtd><mi>if&#xA0;</mi><mrow><mi>in</mi><mfenced><mrow><mrow><msub><mrow><mi>cement</mi></mrow><mrow><mi>type</mi></mrow></msub></mrow></mrow><mrow><mtext>CEM32,5N</mtext></mrow></mfenced><mo>=</mo><mtext>true</mtext></mrow></mtd><mtd><mrow><mn>0.38</mn></mrow></mtd></mtr><mtr><mtd><mi>else&#xA0;</mi></mtd><mtd><mrow><mi>error</mi><mfenced><mrow><mtext>Invalidcementtype.</mtext></mrow></mfenced></mrow></mtd></mtr></mtable></mfenced></mrow></mtd></mtr></mtable></mfenced></mrow></mtd></mtr></mtable></mfenced></math>"));
        //}
    }
}
