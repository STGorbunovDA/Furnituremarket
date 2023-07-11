using Furnituremarket.Domain.Model;
using Xunit;

namespace Furnituremarket.Tests
{
    public class FurnitureTests
    {
        [Fact]
        public void IsPrice_WithNull_ReturnFalse()
        {
            bool actual = Furniture.IsPrice(null);
            Assert.False(actual);
        }

        [Fact]
        public void IsPrice_WithBlankString_ReturnFalse()
        {
            bool actual = Furniture.IsPrice("  ");
            Assert.False(actual);
        }

        [Fact]
        public void IsPrice_WithInvalidPriceTrue_ReturnFalse()
        {
            bool actual = Furniture.IsPrice(" 19,00 ");
            Assert.True(actual);
        }
        [Fact]
        public void IsPrice_WithInvalidPriceFalse_ReturnFalse()
        {
            bool actual = Furniture.IsPrice("q19,00q");
            Assert.False(actual);
        }
    }
}
