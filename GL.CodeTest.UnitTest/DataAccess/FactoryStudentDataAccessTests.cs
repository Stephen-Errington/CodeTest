using GL.CodeTest.DataAccess;
using GL.CodeTest.Failover;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GL.CodeTest.UnitTest.DataAccess {
    [TestClass]
    public class FactoryStudentDataAccessTests {
        [TestMethod]
        public void WhenIsFailoverModeIsTrueThenReturnFailoverStudentDataAccess() {

            var factoryStudentDataAccess = new FactoryStudentDataAccess();

            var dataAccess = factoryStudentDataAccess.GetStudentDataAccess(true);

            Assert.AreEqual(typeof(FailoverStudentDataAccess), dataAccess.GetType());
        }

        [TestMethod]
        public void WhenIsFailoverModeIsFalseThenReturnStudentDataAccess() {

            var factoryStudentDataAccess = new FactoryStudentDataAccess();

            var dataAccess = factoryStudentDataAccess.GetStudentDataAccess(false);

            Assert.AreEqual(typeof(StudentDataAccess), dataAccess.GetType());
        }
    }
}
