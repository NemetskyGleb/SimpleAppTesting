using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Testing.Controllers;
using Testing.Models;
using Xunit;
using Moq;

namespace Testing.Tests;

public class HomeControllerTests
{
    // class FakeDataSource : IDataSource
    // {
    //     public FakeDataSource(Product[] data) => Products = data;
    //     public IEnumerable<Product> Products { get; set; }
    // }
    [Fact]
    public void IndexActionModelIsComplete()
    {
        //Arrange
        Product[] testData = new Product[] {
            new Product { Name = "P1", Price = 75.10M },
            new Product { Name = "P2", Price = 120M },
            new Product { Name = "P3", Price = 110M }
        };
        // IDataSource data = new FakeDataSource(testData);
        // Mock object will fake IDataSource interface
        var mock = new Mock<IDataSource>();
        // SetupGet used to implement getter for a property
        // The Returns method is called on the result of the
        // SetupGet method to specify the result that will
        // be returned when the property value is read
        mock.SetupGet(m => m.Products).Returns(testData);
        var controller = new HomeController();
        // The Mock class defines an Object property, which returns the object
        // that implements the specified interface with the behaviors that have been defined.
        controller.dataSource = mock.Object;    
        // Act
        var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;
        // Assert
        Assert.Equal(testData, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
                                    && p1.Price == p2.Price));
        mock.VerifyGet(m => m.Products, Times.Once);
        // The VerifyGet method is one of the methods defined by the Mock class to inspect the state of the mock object when the test has
        // completed. In this case, the VerifyGet method allows me to check the number of times that the Products property method has been
        // read. The Times.Once value specifies that the VerifyGet method should throw an exception if the property has not been read exactly
        // once, which will cause the test to fail.
    }
}