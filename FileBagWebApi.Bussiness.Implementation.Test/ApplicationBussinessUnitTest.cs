using FileBagWebApi.Bussiness.Interfaces;
using FileBagWebApi.DataAccess.Interfaces;
using FileBagWebApi.Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace FileBagWebApi.Bussiness.Implementation.Test
{
    [TestClass]
    public class ApplicationBussinessUnitTest
    {
        [TestMethod]
        public void ApplicationBussiness_ConstructorShouldSetDataAccess()
        {
            Mock<IApplicationDataAccess> dataAccessMock = new Mock<IApplicationDataAccess>();
            IApplicationDataAccess dataAccess = dataAccessMock.Object;

            IApplicationBussiness applicationBussiness = new ApplicationBussiness(dataAccess);
            Assert.AreSame(dataAccess, applicationBussiness._dataAccess);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplicationBussiness_ConstructorShouldThrowExceptionWhenDataAccessIsNull()
        {
            IApplicationBussiness applicationBussiness = new ApplicationBussiness(null);
        }

        [TestMethod]
        public async Task ApplicationBussiness_RegisterShouldThrowExceptionWhenNameIsEmpty()
        {
            Mock<IApplicationDataAccess> dataAccessMock = new Mock<IApplicationDataAccess>();
            IApplicationDataAccess dataAccess = dataAccessMock.Object;

            IApplicationBussiness applicationBussiness = new ApplicationBussiness(dataAccess);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => applicationBussiness.Register(string.Empty, "randomURI"));
        }

        [TestMethod]
        public async Task ApplicationBussiness_RegisterShouldThrowExceptionWhenURIIsEmpty()
        {
            Mock<IApplicationDataAccess> dataAccessMock = new Mock<IApplicationDataAccess>();
            IApplicationDataAccess dataAccess = dataAccessMock.Object;

            IApplicationBussiness applicationBussiness = new ApplicationBussiness(dataAccess);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => applicationBussiness.Register("randomName", string.Empty));
        }

        [TestMethod]
        public async Task ApplicationBussiness_RegisterShouldCallDataAccessRegisterMethod()
        {
            Application application = new Application() { Id = Guid.NewGuid(), Name = "TestName", URI = "uri.test.com" };
            Mock<IApplicationDataAccess> dataAccessMock = new Mock<IApplicationDataAccess>();
            dataAccessMock.Setup(d => d.Register("randomName", "randomURI")).Returns(Task.FromResult(application));
            IApplicationDataAccess dataAccess = dataAccessMock.Object;

            IApplicationBussiness applicationBussiness = new ApplicationBussiness(dataAccess);
            var result = await applicationBussiness.Register("randomName", "randomURI");
            Assert.IsNotNull(result);
            Assert.AreEqual("TestName", result.Name);
            Assert.AreEqual("uri.test.com", result.URI);
        }
    }
}
