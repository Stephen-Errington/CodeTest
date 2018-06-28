using GL.CodeTest.DataAccess;
using GL.CodeTest.Failover;
using GL.CodeTest.StudentServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GL.CodeTest.UnitTest.StudentServices {
    [TestClass]
    public class StudentServiceTests {
        private Mock<IFactoryStudentDataAccess> factoryStudentDataAccess;
        private Mock<IFailoverService> failoverService;
        private Mock<IArchivedDataService> archivedDataService;
        private Mock<IStudentDataAccess> studentDataAccess;
        private StudentService studentService;

        [TestInitialize]
        public void Init() {
            factoryStudentDataAccess = new Mock<IFactoryStudentDataAccess>();
            failoverService = new Mock<IFailoverService>();
            archivedDataService = new Mock<IArchivedDataService>();
            studentDataAccess = new Mock<IStudentDataAccess>();

            factoryStudentDataAccess.Setup(x => x.GetStudentDataAccess(It.IsAny<bool>())).Returns(studentDataAccess.Object);

            studentService = new StudentService(factoryStudentDataAccess.Object, failoverService.Object, archivedDataService.Object);
        }

        [TestMethod]
        public void GetStudentWhenStudentRequestIsArchivedIsTrueThenOnlyCallArchivedDataService() {
            var studentRequest = GetStudentRequest(true);

            var student = studentService.GetStudent(studentRequest);

            archivedDataService.Verify(x => x.GetArchivedStudent(It.IsAny<int>()), Times.Once());
            failoverService.Verify(x => x.IsFailoverMode(), Times.Never());
            factoryStudentDataAccess.Verify(x => x.GetStudentDataAccess(It.IsAny<bool>()), Times.Never());
        }

        [TestMethod]
        public void GetStudentWhenStudentRequestIsArchivedIsFalseAndStudentResponseIsArchivedIsTrueThenCallAllMethods() {
            var studentRequest = GetStudentRequest(false);
            var studentResponse = GetStudentResponse(true);
            Setup(studentResponse, false);

            var student = studentService.GetStudent(studentRequest);

            archivedDataService.Verify(x => x.GetArchivedStudent(It.IsAny<int>()), Times.Once());
            failoverService.Verify(x => x.IsFailoverMode(), Times.Once());
            factoryStudentDataAccess.Verify(x => x.GetStudentDataAccess(It.IsAny<bool>()), Times.Once());
        }

        [TestMethod]
        public void GetStudentWhenStudentRequestIsArchivedIsFalseAndStudentResponseIsArchivedIsFalseThenDoNotCallArchive() {
            var studentRequest = GetStudentRequest(false);
            var studentResponse = GetStudentResponse(false);
            Setup(studentResponse, false);

            var student = studentService.GetStudent(studentRequest);

            archivedDataService.Verify(x => x.GetArchivedStudent(It.IsAny<int>()), Times.Never());
            failoverService.Verify(x => x.IsFailoverMode(), Times.Once());
            factoryStudentDataAccess.Verify(x => x.GetStudentDataAccess(It.IsAny<bool>()), Times.Once());
        }

        private StudentRequest GetStudentRequest(bool IsArchived) {
            return new StudentRequest() { IsArchived = IsArchived };
        }

        private StudentResponse GetStudentResponse(bool IsArchived) {
            return new StudentResponse() { IsArchived = IsArchived, Student = new Student() };
        }

        private void Setup(StudentResponse response, bool isFailoverMode) {
            studentDataAccess.Setup(x => x.LoadStudent(It.IsAny<int>())).Returns(response);
            failoverService.Setup(x => x.IsFailoverMode()).Returns(isFailoverMode);
        }

    }
}
