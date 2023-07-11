using Furnituremarket.DAL.Interfaces;
using Furnituremarket.Domain.Model;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Furnituremarket.Tests
{
    public class FurnitureServiceTests
    {
        [Fact]
        public void GetFurniture_WithName_CallsGetByQuery()
        {
            //List<Furniture> listFurniture = new List<Furniture>();
            //var furnitureRepositoryStub = new Mock<IFurnitureRepository>();
            //furnitureRepositoryStub.Setup(x => x.GetByQuery(It.IsAny<string>()))
            //    .Returns(
            //         listFurniture.Add(new Furniture())
            //    );
        }
    }
}
