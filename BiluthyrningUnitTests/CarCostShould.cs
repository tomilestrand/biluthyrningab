using System;
using Xunit;
using BiluthyrningABdel1;

namespace BiluthyrningUnitTests
{
    public class CarCostShould
    {
        [Fact]
        public void ReturnNegativeForInvalidTypes()
        {
            var result = Biluthyrning.CarCost(5, 5, 0);
            Assert.InRange<decimal>(result, decimal.MinValue, -1);
        }

        [Fact]
        public void ReturnDecimal()
        {
            var result = Biluthyrning.CarCost(5, 5, 1);
            Assert.IsType<decimal>(result);
        }
    }
}
