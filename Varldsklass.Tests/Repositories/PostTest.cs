using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Varldsklass.Web.Controllers;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Entities.Abstract;
using Moq;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories;

namespace Varldsklass.Tests.Repositories
{
    [TestClass]
    public class PostTest
    {
        [TestMethod]
        public void Can_Save_Valid_Post()
        {
            // Arrange - create mock repository
            Mock<PostRepository> mock = new Mock<PostRepository>();
            // Arrange - create the controller

            AdminController target = new AdminController(mock.Object, null);
            // Arrange - create a product
            Post post = new Post { Title = "Test" };
            // Act - try to save the product
            ActionResult result = target.SavePost(post);
            // Assert - check that the repository was called
            mock.Verify(m => m.SavePost(post));
            // Assert - check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }
    }
}
