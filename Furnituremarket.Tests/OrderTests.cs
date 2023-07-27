using Furnituremarket.Domain.Model;
using System;
using Xunit;

namespace Furnituremarket.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Order_WithNullItems_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Order(1, null));
        }
        [Fact]
        public void TotalCount_WithEmptyItems_ReturnZero()
        {
            var order = new Order(1, new OrderItem[0]);

            Assert.Equal(0, order.TotalCount);
        }
        [Fact]
        public void TotalPrice_WithEmptyItems_ReturnZero()
        {
            var order = new Order(1, new OrderItem[0]);

            Assert.Equal(0m, order.TotalPrice);
        }

        [Fact]
        public void TotalCount_WithNonEmptyItems_CalcualtesTotalCount()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,"","","","","",3,10m),
                new OrderItem(2,"","","","","",5,100m)
            });

            Assert.Equal(3 + 5, order.TotalCount);
        }

        [Fact]
        public void TotalPrice_WithNonEmptyItems_CalcualtesTotalPrice()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,"","","","","", 3, 10m),
                new OrderItem(2,"","","","","", 5, 100m)
            });

            Assert.Equal(3 * 10m + 5 * 100m, order.TotalPrice);
        }
        [Fact]
        public void GetItemFurniture_WithExistingItem_ReturnsItem()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,"","","","","", 3, 10m),
                new OrderItem(2,"","","","","", 5, 100m)
            });

            var orderItem = order.GetItemFurniture(1);

            Assert.Equal(3, orderItem.Count);
        }
        [Fact]
        public void GetItemFurniture_WithNonExistingItem_ThrowsInvalidOperationExeption()
        {
            var order = new Order(1, new[]
           {
                new OrderItem(1,"","","","","", 3, 10m),
                new OrderItem(2,"","","","","", 5, 100m)
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                order.GetItemFurniture(100);
            });
        }
    }
}
