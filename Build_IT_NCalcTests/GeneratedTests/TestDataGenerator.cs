using Build_IT_NCalc.Units;
using System.Collections.Generic;

namespace Build_IT_NCalcTests.GeneratedTests
{
    using Par = TestDataInputParameter;
    public class TestDataGenerator
    {
        public static IEnumerable<object[]> GetDataFromDataGenerator()
        {
            /* 1 */
            yield return new object[] {  "a+b", "55", new Par("a", 22), new Par("b", 33) };
            /* 2 */
            yield return new object[] {  "a+b", "3.22m", new Par("a", 22, "cm"), new Par("b", 3, "m") };
            /* 3 */
            yield return new object[] {  "a*(b+c)", "6.4m", new Par("a", 2), new Par("b", 2, "m"), new Par("c", 120, "cm"), };
            /* 4 */
            yield return new object[] {  "a/b/c", "11kPa", new Par("a", 44, "kN"), new Par("b", 2, "m"), new Par("c", 2, "m"), };
            /* 5 */
            yield return new object[] {  "a+b+c", "5.32m", new Par("a", 222, "cm"), new Par("b", 2, "m"), new Par("c", 1100, "mm"), };
            /* 6 */
            yield return new object[] {  "b/a", "10kPa/m^2", new Par("a", 2, "m^2"), new Par("b", 20, "kPa"), };
            /* 7 */
            yield return new object[] {  "a*(b+c/d)", "40kN", new Par("a", 2), new Par("b", 10, "kN"), new Par("c", 2, "kN,m"), new Par("d", 20, "cm"), };
            /* 8 */
            yield return new object[] {  "a+b", "4m", new Par("a", 2, "m"), new Par("b", 2, "m"), };
            /* 9 */ 
            yield return new object[] {  "a+b", "5.001kN", new Par("a", 5, "kN"), new Par("b", 1,  "N"), };
//EndLine - do not remove
        }
    }
}
