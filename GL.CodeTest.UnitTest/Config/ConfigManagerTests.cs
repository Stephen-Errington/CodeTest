using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GL.CodeTest.Config;

namespace GL.CodeTest.UnitTest.Config {
    [TestClass]
    public class ConfigManagerTests {
        [TestMethod]
        public void WhenIsFailoverModeEnabledButNoMinutesOrLimitThenReturnFalse() {
            var configReader = new Mock<IConfigReader>();
            configReader.Setup(x => x.Read<bool>(It.IsAny<string>(), It.IsAny<bool>())).Returns(true);

            var config = new ConfigManager(configReader.Object);

            var isFailoverMode = config.IsFailoverModeEnabled;

            Assert.AreEqual(false, isFailoverMode);
        }

        [TestMethod]
        public void WhenIsFailoverModeEnabledCalledThenConstraintIsFAILOVER_MODE_ENABLED() {
            var configReader = new Mock<IConfigReader>();
            configReader.Setup(x => x.Read<bool>(It.IsAny<string>(), It.IsAny<bool>())).Returns(true);

            var config = new ConfigManager(configReader.Object);

            var isFailoverMode = config.IsFailoverModeEnabled;
            configReader.Verify(x => x.Read<bool>(Constraints.FAILOVER_MODE_ENABLED, It.IsAny<bool>()), Times.Once());
        }

        [TestMethod]
        public void WhenFailedRequestLimitCalledThenConstraintIsFAILED_REQUEST_LIMIT() {
            var configReader = new Mock<IConfigReader>();
            configReader.Setup(x => x.Read<int?>(It.IsAny<string>(), null)).Returns(0);

            var config = new ConfigManager(configReader.Object);

            var failedRequestLimit = config.FailedRequestLimit;
            configReader.Verify(x => x.Read<int?>(Constraints.FAILED_REQUEST_LIMIT, It.IsAny<int?>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public void WhenFailoverMinutesCalledThenConstraintIsFAILOVER_MINUTES() {
            var configReader = new Mock<IConfigReader>();
            configReader.Setup(x => x.Read<int?>(It.IsAny<string>(), null)).Returns(0);

            var config = new ConfigManager(configReader.Object);

            var failoverMinutes = config.FailoverMinutes;
            configReader.Verify(x => x.Read<int?>(Constraints.FAILOVER_MINUTES, It.IsAny<int?>()), Times.AtLeastOnce());
        }
    }
}
