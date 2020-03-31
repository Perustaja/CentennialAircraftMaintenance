using System;
using CAM.Core.Entities;
using CAM.Tests.Builders;
using Xunit;

namespace CAM.Tests.UnitTests.Entities.PartTests
{
    public class CurrentStock
    {
        [Fact]
        public void AddStock_Throws_On_Negative_Value()
        {
            var testPart = PartBuilder.ReturnDefaultPart();
            Assert.Throws<ArgumentException>(() => testPart.AddStock(-12));
        }
        [Fact]
        public void RemoveStock_Throws_On_Negative_Value()
        {
            var testPart = PartBuilder.ReturnDefaultPart();
            Assert.Throws<ArgumentException>(() => testPart.RemoveStock(-12));
        }
        [Fact]
        public void CurrentStock_Zero_After_Removal_Greater_Than_CurrentStock()
        {
            var testPart = PartBuilder.ReturnDefaultPart();
            testPart.RemoveStock(1);
            Assert.Equal(0, testPart.CurrentStock);
        }
    }
}