using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Build_IT_FrameStaticaTests
{
    public static class DataSplitter
    {
        #region Public_Methods

        public static IEnumerable SplitData(short spanNumber, string fileName)
        {
            var valuesData = GetDataFromFile(fileName);
            var split = valuesData.Split(';');
            var organizedData = new List<Data>();

            for (int i = 0; i < split.Length; i += 8)
            {
                var data = new Data();

                data.Fx = Convert.ToDouble(split[i + 3].Replace(',', '.'), CultureInfo.InvariantCulture);
                data.Fz = Convert.ToDouble(split[i + 4].Replace(',', '.'), CultureInfo.InvariantCulture);
                data.My = Convert.ToDouble(split[i + 5].Replace(',', '.'), CultureInfo.InvariantCulture);
                data.Ux = Convert.ToDouble(split[i + 6].Replace(',', '.'), CultureInfo.InvariantCulture);
                data.Uz = Convert.ToDouble(split[i + 7].Replace(',', '.'), CultureInfo.InvariantCulture);

                if (split[i + 1].Contains("początek", StringComparison.InvariantCulture))
                    data.Position = 0;
                else if (split[i + 1].Contains("koniec", StringComparison.InvariantCulture))
                    data.LastOne = true;
                else
                    SetPosition(split, i, data);

                organizedData.Add(data);
            }

            return GroupSamePositions(spanNumber, organizedData);
        }

        #endregion // Public_Methods

        #region Private_Methods

        private static string GetDataFromFile(string fileName)
        {
            var path = @".\FrameData\" + fileName + ".csv";
            string[] fileData = File.ReadAllLines(path)
                .Skip(1)
                .TakeWhile(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();

            return string.Join(';', fileData);
        }


        private static void SetPosition(string[] split, int i, Data data)
        {
            var positionString = new String(split[i + 1].Where(s => Char.IsDigit(s) || s == ',').ToArray());
            data.Position = Convert.ToDouble(positionString.Replace(',', '.'), CultureInfo.InvariantCulture);

            //if (split[i + 1].Contains("(+)", StringComparison.InvariantCulture))
            //    data.Position += Data.Offset;
            //else if (split[i + 1].Contains("(-)", StringComparison.InvariantCulture))
            //    data.Position -= Data.Offset;
        }

        private static IEnumerable GroupSamePositions(short spanNumber, List<Data> organizedData)
        {
            var groupedData = organizedData.GroupBy(d => d.Position);

            foreach (var group in groupedData)
            {
                if (group.Count() == 1)
                    yield return new object[] { spanNumber, group.First() };
                else
                {
                    var data = new Data();
                    data.Position = group.First().Position;
                    data.MinMaxFx[0] = group.Min(d => d.Fx);
                    data.MinMaxFx[1] = group.Max(d => d.Fx);
                    data.MinMaxFz[0] = group.Min(d => d.Fz);
                    data.MinMaxFz[1] = group.Max(d => d.Fz);
                    data.MinMaxMy[0] = group.Min(d => d.My);
                    data.MinMaxMy[1] = group.Max(d => d.My);
                    data.MinMaxUx[0] = group.Min(d => d.Ux);
                    data.MinMaxUx[1] = group.Max(d => d.Ux);
                    data.MinMaxUz[0] = group.Min(d => d.Uz);
                    data.MinMaxUz[1] = group.Max(d => d.Uz);
                    data.HasTwoValues = true;
                    yield return new object[] { spanNumber, data };
                }
            }
        }
        #endregion // Private_Methods
    }
}