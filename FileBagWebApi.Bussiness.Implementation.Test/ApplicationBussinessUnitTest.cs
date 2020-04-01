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
        private const string cypherKey = "acf7ef943fdeb3cbfed8dd0d8f584731";


        #region ctor tests
        [TestMethod]
        public void ApplicationBussiness_ConstructorShouldSetDataAccess()
        {
            Mock<IApplicationDataAccess> dataAccessMock = new Mock<IApplicationDataAccess>();
            IApplicationDataAccess dataAccess = dataAccessMock.Object;

            IApplicationBussiness applicationBussiness = new ApplicationBussiness(dataAccess,cypherKey);
            Assert.AreSame(dataAccess, applicationBussiness._dataAccess);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplicationBussiness_ConstructorShouldThrowExceptionWhenDataAccessIsNull()
        {
            IApplicationBussiness applicationBussiness = new ApplicationBussiness(null, cypherKey);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplicationBussiness_ConstructorShouldThrowExceptionWhenKeyIsNull()
        {
            Mock<IApplicationDataAccess> dataAccessMock = new Mock<IApplicationDataAccess>();
            IApplicationDataAccess dataAccess = dataAccessMock.Object;

            IApplicationBussiness applicationBussiness = new ApplicationBussiness(dataAccess, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplicationBussiness_ConstructorShouldThrowExceptionWhenKeyIsEmpty()
        {
            Mock<IApplicationDataAccess> dataAccessMock = new Mock<IApplicationDataAccess>();
            IApplicationDataAccess dataAccess = dataAccessMock.Object;

            IApplicationBussiness applicationBussiness = new ApplicationBussiness(dataAccess, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplicationBussiness_ConstructorShouldThrowExceptionWhenKeyIsWhitespace()
        {
            Mock<IApplicationDataAccess> dataAccessMock = new Mock<IApplicationDataAccess>();
            IApplicationDataAccess dataAccess = dataAccessMock.Object;

            IApplicationBussiness applicationBussiness = new ApplicationBussiness(dataAccess, "".PadLeft(32, ' '));
        }
        #endregion

        #region Register tests
        [TestMethod]
        public async Task ApplicationBussiness_RegisterShouldThrowExceptionWhenNameIsEmpty()
        {
            Mock<IApplicationDataAccess> dataAccessMock = new Mock<IApplicationDataAccess>();
            IApplicationDataAccess dataAccess = dataAccessMock.Object;

            IApplicationBussiness applicationBussiness = new ApplicationBussiness(dataAccess, cypherKey);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => applicationBussiness.Register(string.Empty, "randomSecret", "randomURI")); ;
        }

        [TestMethod]
        public async Task ApplicationBussiness_RegisterShouldThrowExceptionWhenSecretIsEmpty()
        {
            Mock<IApplicationDataAccess> dataAccessMock = new Mock<IApplicationDataAccess>();
            IApplicationDataAccess dataAccess = dataAccessMock.Object;

            IApplicationBussiness applicationBussiness = new ApplicationBussiness(dataAccess, cypherKey);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => applicationBussiness.Register("randomName", string.Empty, "randomURI")); ;
        }


        [TestMethod]
        public async Task ApplicationBussiness_RegisterShouldThrowExceptionWhenURIIsEmpty()
        {
            Mock<IApplicationDataAccess> dataAccessMock = new Mock<IApplicationDataAccess>();
            IApplicationDataAccess dataAccess = dataAccessMock.Object;

            IApplicationBussiness applicationBussiness = new ApplicationBussiness(dataAccess, cypherKey);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => applicationBussiness.Register("randomName", "randomSecret", string.Empty));
        }

        [TestMethod]
        public async Task ApplicationBussiness_RegisterShouldCallDataAccessRegisterMethod()
        {
            Application application = new Application() { Id = Guid.NewGuid(), Name = "TestName", URI = "uri.test.com" };
            Mock<IApplicationDataAccess> dataAccessMock = new Mock<IApplicationDataAccess>();
            dataAccessMock.Setup(d => d.Register("randomName", It.IsAny<string>(), "randomURI")).Returns(Task.FromResult(application));
            IApplicationDataAccess dataAccess = dataAccessMock.Object;

            IApplicationBussiness applicationBussiness = new ApplicationBussiness(dataAccess, cypherKey);
            var result = await applicationBussiness.Register("randomName", "randomSecret", "randomURI");
            Assert.IsNotNull(result);
            Assert.AreEqual("TestName", result.Name);
            Assert.AreEqual("uri.test.com", result.URI);
        }
        #endregion
    }
}
