using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Varldsklass.Web;
using Varldsklass.Web.Controllers;
using Moq;
using Varldsklass.Domain.Repositories;
using Varldsklass.Web.ViewModels;
using Varldsklass.Domain.Entities;


namespace Varldsklass.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        public AccountControllerTest()
        {
            
        }

        [TestMethod]
        public void LogOn()
        {
            
            // Arrange - database
            /*Mock<AccountRepository> mock = new Mock<AccountRepository>();
            mock.Setup(u => u.Save(new Account {
                ID = 1,
                Email = "admin@varldsklass.com",
                Password = "dNZcqnm6dgynDMJpZcZlfw==",
                FirstName = "Mr.",
                LastName = "Whatever",
                Role = 2,
                CreatedDate = DateTime.Now
            }));*/

            IAccountRepository accountRepository = new AccountRepository();
            accountRepository.Save(new Account { ID = 1, Email = "admin@varldsklass.com", FirstName = "Admin", LastName = "von Världsklass", Password = "dNZcqnm6dgynDMJpZcZlfw==", Role = 1, CreatedDate = DateTime.Now });
            //accountRepository.Save(new Account { ID = 2, Email = "bokare@varldsklass.com", FirstName = "Bokare", LastName = "von Världsklass", Password = "dNZcqnm6dgynDMJpZcZlfw==", Role = 2, CreatedDate = DateTime.Now });
            
            // Arrange - viewmodel
            LogOnViewModel model = new LogOnViewModel
            {
                Email = "admin@varldsklass.com",
                Password = "password"
            };

            // Arrange - controller
            AccountController controller = new AccountController(accountRepository);

            // Act
            ActionResult result = controller.LogOn(model, "/") as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/", ((RedirectResult)result).Url);
            
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
