using NUnit.Framework;
using Moq;
using JenkinProjectRazor.Pages;
using Microsoft.Extensions.Logging;

namespace JenkinProjectRazor.Tests
{
    [TestFixture]
    public class PageModelTests
    {
        private Mock<ILogger<IndexModel>> _indexLoggerMock;
        private Mock<ILogger<PrivacyModel>> _privacyLoggerMock;
        private Mock<ILogger<ErrorModel>> _errorLoggerMock;

        [SetUp]
        public void Setup()
        {
            _indexLoggerMock = new Mock<ILogger<IndexModel>>();
            _privacyLoggerMock = new Mock<ILogger<PrivacyModel>>();
            _errorLoggerMock = new Mock<ILogger<ErrorModel>>();
        }

        [Test]
        public void IndexModel_OnGet_DoesNotThrow()
        {
            var model = new IndexModel(_indexLoggerMock.Object);
            Assert.DoesNotThrow(() => model.OnGet());
        }

        [Test]
        public void PrivacyModel_OnGet_DoesNotThrow()
        {
            var model = new PrivacyModel(_privacyLoggerMock.Object);
            Assert.DoesNotThrow(() => model.OnGet());
        }

        [Test]
        public void ErrorModel_ShowRequestId_ReturnsTrue_WhenRequestIdIsSet()
        {
            var model = new ErrorModel(_errorLoggerMock.Object)
            {
                RequestId = "ABC123"
            };
            Assert.IsTrue(model.ShowRequestId);
        }

        [Test]
        public void ErrorModel_ShowRequestId_ReturnsFalse_WhenRequestIdIsNullOrEmpty()
        {
            var model = new ErrorModel(_errorLoggerMock.Object)
            {
                RequestId = null
            };
            Assert.IsFalse(model.ShowRequestId);

            model.RequestId = "";
            Assert.IsFalse(model.ShowRequestId);
        }
    }
}
