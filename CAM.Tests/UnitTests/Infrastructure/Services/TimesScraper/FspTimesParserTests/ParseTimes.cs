using System.Collections.Generic;
using CAM.Core.Entities;
using CAM.Infrastructure.Services.TimesScraper;
using Xunit;
using CAM.Tests.Builders;
using KellermanSoftware.CompareNetObjects;

namespace CAM.Tests.UnitTests.Infrastructure.Services.TimesScraper.FspTimesParserTests
{
    public class ParseTimes
    {
        /// <summary>
        /// Ensures that, when given a number with trailing text, the parser creates a substring
        /// and converts it to a decimal.
        /// </summary>
        [Theory]
        [InlineData("Aircraft Total Time", "4026.27 (follows Engine 1 Tach)", 4026.27)]
        [InlineData("Aircraft Total Time", "99999.99 (follows Engine 1 Tach)", 99999.99)]
        public void Trims_Trailing_Text_If_Necessary(string label, string value, decimal expectedNum)
        {
            // Create the lists to pass in, the labels and values must be at index 1
            var tuple = TimesBuilders.ReturnMockLists();
            var labels = tuple.Item1;
            labels.Add(label);
            var values = tuple.Item2;
            values.Add(value);

            Times timeValues = FspTimesParser.ParseTimes(labels, values);

            Assert.Equal((decimal)expectedNum, timeValues.AircraftTotal);
        }

        /// <summary>
        /// Ensures that if some parameter is not tracked(e.g. an aircraft does not have a Hobbs meter,
        /// or only has one engine), the value is set to zero.
        /// </summary>
        [Fact]
        public void Sets_Zero_Values_If_Parameter_Not_Tracked()
        {
            var tuple = TimesBuilders.ReturnMockLists();
            var labels = tuple.Item1;
            var values = tuple.Item2;

            Times outputTimes = FspTimesParser.ParseTimes(labels, values);
            Times expectedTimes = Builders.TimesBuilders.ReturnTimesAllZeroButAircraftId();
            
            // Using CompareNetObjects to compare the two objects
            var compareLogic = new CompareLogic();
            Assert.True(compareLogic.Compare(expectedTimes, outputTimes).AreEqual);
        }
    }
}