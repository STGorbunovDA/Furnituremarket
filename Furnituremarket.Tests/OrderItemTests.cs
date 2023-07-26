using Furnituremarket.Domain.Model;
using System;
using Xunit;

namespace Furnituremarket.Tests
{
    public class OrderItemTests
    {
        [Fact]
        public void OrderItem_WithZeroCount_ThrowsArgumentOutOfRangeException()
        {
            int count = 0;
            Assert.Throws<ArgumentOutOfRangeException>(() => { new OrderItem(1, "", "", "", "", "", count, 0m); });
        }
        [Fact]
        public void OrderItem_WithNegativeCount_ThrowsArgumentOutOfRangeException()
        {
            int count = -1;
            Assert.Throws<ArgumentOutOfRangeException>(() => { new OrderItem(1, "", "", "", "", "", count, 0m); });
        }
        [Fact]
        public void OrderItem_WithPositeve()
        {
            var orderItem = new OrderItem(1, "", "", "", "", "", 2, 3m);

            Assert.Equal(1, orderItem.FurnitureId);
            Assert.Equal("", orderItem.Name);
            Assert.Equal("", orderItem.Description);
            Assert.Equal("", orderItem.Color);
            Assert.Equal("", orderItem.Material);
            Assert.Equal("", orderItem.Image);
            Assert.Equal(2, orderItem.Count);
            Assert.Equal(3m, orderItem.Price);
        }
        [Fact]
        public void Count_WithNegativeValue_ThrowsArgumentOfRangeExctption()
        {
            var orderItem = new OrderItem(1, "", "", "", "", "", 2, 3m);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                orderItem.Count = -1;
            });
        }
        [Fact]
        public void Count_WithZeroValue_ThrowsArgumentOfRangeExctption()
        {
            var orderItem = new OrderItem(1, "", "", "", "", "", 2, 3m);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                orderItem.Count = 0;
            });
        }
        [Fact]
        public void Count_WithPositiveValue_SetsValue()
        {
            var orderItem = new OrderItem(1, "", "", "", "", "", 2, 3m);

            orderItem.Count = 10;

           Assert.Equal(10, orderItem.Count);
        }
    }
}
