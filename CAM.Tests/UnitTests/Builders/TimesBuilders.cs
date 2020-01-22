using System;
using System.Collections.Generic;
using CAM.Core.Entities;

namespace CAM.Tests.Builders
{
    public class TimesBuilders
    {
        const string _mockAircraftLabel = "Aircraft";
        const string _mockAircraftValuePreTrim = "N9999 MakeModel";
        const string _mockAircraftValuePostTrim = "N9999";

        /// <summary>
        /// Returns a mock pair of lists which simulate no hobbs, cycles, or airtime, and only 1
        /// engine.
        /// </summary>
        /// <returns>List one: labels, List two: values.</returns>
        public static Tuple<List<string>, List<string>> ReturnMockLists()
        {
            var labelList = new List<string>
            {
                _mockAircraftLabel,
            };

            var valueList = new List<string>
            {
                _mockAircraftValuePreTrim,
            };

            return new Tuple<List<string>, List<string>>(labelList, valueList);
        }

        public static Times ReturnTimesAllZeroButAircraftId()
        {
            return new Times
            {
                AircraftId = _mockAircraftValuePostTrim,
                Hobbs = 0m,
                AirTime = 0,
                Tach1 = 0m,
                Tach2 = 0m,
                Prop1 = 0m,
                Prop2 = 0m,
                AircraftTotal = 0m,
                Engine1Total = 0m,
                Engine2Total = 0m,
                Cycles = 0,
            };
        }
    }
}