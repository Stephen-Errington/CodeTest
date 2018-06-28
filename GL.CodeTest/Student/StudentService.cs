using GL.CodeTest.DataAccess;
using GL.CodeTest.Failover;

namespace GL.CodeTest.StudentServices {
    public class StudentService : IStudentService {
        private readonly IFactoryStudentDataAccess factoryStudentDataAccess;
        private readonly IFailoverService failoverService;
        private readonly IArchivedDataService archivedDataService;

        public StudentService(IFactoryStudentDataAccess factoryStudentDataAccess, IFailoverService failoverService, IArchivedDataService archivedDataService) {
            this.factoryStudentDataAccess = factoryStudentDataAccess;
            this.failoverService = failoverService;
            this.archivedDataService = archivedDataService;
        }

        public Student GetStudent(StudentRequest studentRequest) {
            if(studentRequest.IsArchived)
                return GetArchivedStudent(studentRequest.StudentId);

            var isFailoverMode = failoverService.IsFailoverMode();
            var dataAccess = this.factoryStudentDataAccess.GetStudentDataAccess(isFailoverMode);
            var studentResponse = dataAccess.LoadStudent(studentRequest.StudentId);

            return studentResponse.IsArchived ? GetArchivedStudent(studentResponse.Student.Id) : studentResponse.Student;
        }

        private Student GetArchivedStudent(int studentId) {
            return archivedDataService.GetArchivedStudent(studentId);
        }
    }
}
