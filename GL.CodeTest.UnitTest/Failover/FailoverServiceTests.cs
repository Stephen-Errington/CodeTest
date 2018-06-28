using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GL.CodeTest.Failover;
using GL.CodeTest.Config;
using Moq;

namespace GL.CodeTest.UnitTest.Failover {
    [TestClass]
    public class FailoverServiceTests {
        [TestMethod]
        public void WhenFailoverModeEnabledIsFalseThenDoNotCallCountFailedEntries() {
            var failoverRepository = new Mock<IFailoverRepository>();
            var config = new Mock<IConfigManager>();
            config.Setup(x => x.IsFailoverModeEnabled).Returns(false);

            var failoverService = new FailoverService(failoverRepository.Object, config.Object);

            var isFailoverMode = failoverService.IsFailoverMode();

            failoverRepository.Verify(x => x.CountFailedEntries(It.IsAny<int>()), Times.Never());
        }

        [TestMethod]
        public void WhenFailoverModeEnabledIsTrueAndFailedCountIsOverLimitReturnTrue() {
            var failoverRepository = new Mock<IFailoverRepository>();
            var config = new Mock<IConfigManager>();

            config.Setup(x => x.IsFailoverModeEnabled).Returns(true);
            config.Setup(x => x.FailoverMinutes).Returns(It.IsAny<int>());
            config.Setup(x => x.FailedRequestLimit).Returns(100);

            failoverRepository.Setup(x => x.CountFailedEntries(It.IsAny<int>())).Returns(105);

            var failoverService = new FailoverService(failoverRepository.Object, config.Object);

            var isFailoverMode = failoverService.IsFailoverMode();

            Assert.AreEqual(true, isFailoverMode);
        }

        [TestMethod]
        public void WhenFailoverModeEnabledIsTrueAndFailedCountIsUnderLimitReturnFalse() {
            var failoverRepository = new Mock<IFailoverRepository>();
            var config = new Mock<IConfigManager>();

            config.Setup(x => x.IsFailoverModeEnabled).Returns(true);
            config.Setup(x => x.FailoverMinutes).Returns(It.IsAny<int>());
            config.Setup(x => x.FailedRequestLimit).Returns(100);

            failoverRepository.Setup(x => x.CountFailedEntries(It.IsAny<int>())).Returns(5);

            var failoverService = new FailoverService(failoverRepository.Object, config.Object);

            var isFailoverMode = failoverService.IsFailoverMode();

            Assert.AreEqual(false, isFailoverMode);
        }
    }
}
