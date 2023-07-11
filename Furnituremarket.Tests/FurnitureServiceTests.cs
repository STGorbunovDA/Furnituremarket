using Furnituremarket.DAL.Interfaces;
using Furnituremarket.Domain.Model;
using Furnituremarket.Service.Implementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Furnituremarket.Tests
{
    public class FurnitureServiceTests
    {
    //    [Fact]
    //    public void GetFurniture_WithName_CallsGetByQuery()
    //    {
    //        var furnitureRepositoryStub = new Mock<IFurnitureRepository>();

    //        furnitureRepositoryStub.Setup(x => x.GetByQuery(It.IsAny<string>()))
    //            .Returns(() => new Task<List<Furniture>>(() =>
    //                    new List<Furniture>() { new Furniture(1, "Стол", "", "", "", 0.00m, DateTime.Now) }));
           
    //        var furnitureService = new FurnitureService(furnitureRepositoryStub.Object);

    //        var validName = "Стол";

    //        var actual = furnitureService.GetFurniture(validName);

    //        Assert.Collection ((IEnumerable<Furniture>) actual, furniture => Assert.Equal("Стол", furniture.Name));
    //    }
    }
}
